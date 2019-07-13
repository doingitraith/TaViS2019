﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public UnityEricController eric;
    public GameObject suspiciousnessParent;
    public GameObject photoParent;
    public Text suspicousness;
    public Text score;
    Text feedBack;
    public Image EyeFill;
    string feedBackText;
    Image FeedbackBackground;
    float fade = .7f;
    float fadeOut = 2f;
    float pause = 5f;
    float timePerLetter = 0.012f;
    bool textBuild;
    public Color colorPerfect = Color.green;
    public Color colorVeryGood = Color.blue;
    public Color colorGood = Color.cyan;
    public Color colorOk;
    public Color colorBad = Color.red;

    private void Start()
    {
        feedBack = GameObject.Find("Feedback").GetComponent<Text>();
        FeedbackBackground = GameObject.Find("FeedbackBackground").GetComponent<Image>();
        feedBackText = "";
        textBuild = true;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            GameManager.Instance.ChangeSuspicousness(5);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameManager.Instance.ChangeSuspicousness(-5);
        }
        UpdateScore();
        UpdateSuspiciousness();
    }
    public void UpdateSuspiciousness()
    {
        float suspiciousnessValue = GameManager.Instance.suspicousnessLevel;

        if(suspiciousnessValue >= GameManager.Instance.scoreDangerous)
            suspicousness.text = "The Guards are onto you!";
        else if (suspiciousnessValue >= GameManager.Instance.scoreSuspicous)
            suspicousness.text = "The Guards are suspicous";
        else
            suspicousness.text = "The Guards suspect nothing";

        EyeFill.fillAmount = suspiciousnessValue / GameManager.Instance.scoreDangerous;
    }

    public void UpdateScore()
    {
        float scoreValue = GameManager.Instance.score;
        score.text = "Score: " + scoreValue;
    }

    //Call on GestureFinish or GameFinish : set performance and gesturename null for flavor text, or set gameId null and the others not for gesture feedback
    public void UpdateFeedbackTextGesture(Nullable<GestureEvaluationResult.GESTURE_PERFORMANCE> performance, Nullable<GestureManager.GESTURENAME> gestureName, Nullable<GameID.GAME_ID> gameId)
    {
        //StopCoroutine("LetterByLetter");
        if(performance.Equals(GestureEvaluationResult.GESTURE_PERFORMANCE.NONE))
        {
            return;
        }

        feedBack.text = "";
        feedBackText = "";
        feedBack.color = Color.white;
        Color background = colorOk;
        feedBack.alignment = TextAnchor.UpperLeft;
        if (gameId != null)
        {
            eric.textPlaying = true;
            feedBack.text = "";
            switch (gameId)
            {
                case GameID.GAME_ID.START: feedBackText = "Welcome to PLACE agent, melting pot of dealers and gangsters of all kind.\nLogic, instinct and discretion are your most reknown qualities as we have been informed.\nYour target of this mission is on the stage.\nFollow your intuition and don't draw the guard's attention.\nJust blend in with the guests and follow my instructions. First you need to pass an ID check.\nGood luck in there agent."; break;
                case GameID.GAME_ID.TIP_HAT_DRINK: feedBackText = "We see you have taken the place of the bartender. Outstanding move!\nIt is customary to greet your guests before serving them.\nBut of course you know that.\nJust tip your hat."; break;
                case GameID.GAME_ID.END: feedBackText = "This is it agent. We never doubted your skills. Bravo!\nNow you can safely secure the target. Good job agent and thanks for your time."; break;
                default: eric.textPlaying = false; return;
            }
        }
        if (performance != null)
        {
            feedBackText = "";
            switch (performance)
            {
                //case GestureEvaluationResult.GESTURE_PERFORMANCE.INVALID: feedBackText = "*HQ are you sure this is the famous agent?*"; background = colorBad; break;
                case GestureEvaluationResult.GESTURE_PERFORMANCE.BAD: feedBackText = "*HQ are you sure this is the famous agent?*"; background = colorBad; break;
                case GestureEvaluationResult.GESTURE_PERFORMANCE.OK: feedBackText = "Is something wrong agent? That was close."; background = colorOk; break;
                case GestureEvaluationResult.GESTURE_PERFORMANCE.GOOD: feedBackText = "So far so good!"; background = colorGood; break;
                case GestureEvaluationResult.GESTURE_PERFORMANCE.VERY_GOOD: feedBackText = "Very Convincing!"; background = colorVeryGood; break;
                case GestureEvaluationResult.GESTURE_PERFORMANCE.PERFECT: feedBackText = "Excellent, you are truly inconspicuous!"; background = colorPerfect; break;
                default: break;
            }
            if (gestureName != null)
            {
                switch (gestureName)
                {
                    case GestureManager.GESTURENAME.TIP_HAT: feedBackText += "\nNow you need to have a drink with the guests"; break;
                    case GestureManager.GESTURENAME.DRINK: feedBackText += "\nNow you need to perform for the guests."; break;
                    default: break;
                }
            }
        }
        feedBack.text = "";

        ComputePauseDuration();
        StartCoroutine(LetterByLetter(background));

    }

    IEnumerator FadeIn(Color color)
    {
        for (float i = 0.01f; i < fade; i += Time.deltaTime)
        {
            FeedbackBackground.color = Color.Lerp(Color.clear, color, i / fade);
            yield return null;
        }
    }

    IEnumerator LetterByLetter(Color color)
    {
        StartCoroutine(FadeIn(color));
        foreach (char letter in feedBackText)
        {
            feedBack.text += "" + letter;
            yield return new WaitForSeconds(0.035f);
        }
        for (float i = 0; i < pause; i += Time.deltaTime)
        {
            yield return null;
        }
        Color colorText = feedBack.color;
        Color colorBackground = FeedbackBackground.color;
        for (float i = 0.01f; i < fadeOut; i += Time.deltaTime)
        {
            feedBack.color = Color.Lerp(colorText, Color.clear, i / fadeOut);
            FeedbackBackground.color = Color.Lerp(colorBackground, Color.clear, i / fadeOut);
            yield return null;
        }

        if (GameManager.Instance.CurrentGame.Equals(GameID.GAME_ID.START))
        {
            photoParent.SetActive(true);
        }
        if (GameManager.Instance.CurrentGame.Equals(GameID.GAME_ID.TIP_HAT_DRINK))
        {
            suspiciousnessParent.SetActive(true);
        }

        eric.textPlaying = false;
    }

    IEnumerator WaitBeforeFadeOut()
    {
        for (float i = 0; i < pause; i += Time.deltaTime)
        {
            yield return null;
        }
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        Color colorText = feedBack.color;
        Color colorBackground = FeedbackBackground.color;
        for (float i = 0.01f; i < fadeOut; i += Time.deltaTime)
        {
            feedBack.color = Color.Lerp(colorText, Color.clear, i / fadeOut);
            FeedbackBackground.color = Color.Lerp(colorBackground, Color.clear, i / fadeOut);
            yield return null;
        }

        if (GameManager.Instance.CurrentGame.Equals(GameID.GAME_ID.START))
        {
            photoParent.SetActive(true);
        }
        if (GameManager.Instance.CurrentGame.Equals(GameID.GAME_ID.TIP_HAT_DRINK))
        {
            suspiciousnessParent.SetActive(true);
        }
        eric.textPlaying = false;
    }

    void ComputePauseDuration()
    {
        pause = 0.0f;
        pause = timePerLetter * feedBackText.Length;
        //Debug.Log("pause: " + pause);
    }
}
