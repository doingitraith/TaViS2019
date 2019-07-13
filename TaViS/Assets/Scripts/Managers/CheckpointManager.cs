using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckpointManager : MonoBehaviour
{
    float startTimeValue = 10f;
    float time;

    bool noList;
    bool first;
    bool middle;
    bool last;
    bool timeout;
    bool invalid;
    bool startTimer;

    int score = 0;
    int timeoutPenalty = 100;
    int invalidPenalty = 200;
    int scoreIncrement = 300;

    Transform checkpointParent;
    public List<Checkpoint> checkpoints = new List<Checkpoint>();

    GameObject actor;
    Text countdown;

    // Start is called before the first frame update
    void Start()
    {
        countdown = GameObject.Find("Countdown").GetComponent<Text>();
        //LoadCheckPoints();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!noList)
        {
            if (startTimer)
            {
                InvokeRepeating("Countdown", 1.0f, 1.0f);
                startTimer = false;
            }
            if (last)
            {
                CancelInvoke("Countdown");
                countdown.text = "";
                GestureEvaluationResult result = ComputeResult();
                last = false;
                GameManager.Instance.MiniGameManager.TriggerGestureResult(result);
            }
        }
    }

    void Countdown()
    {
        if (--time < 0)
        {
            countdown.text = "";
            CancelInvoke("Countdown");
            timeout = true;
            score -= timeoutPenalty;
            GestureEvaluationResult result = ComputeResult();
            GameManager.Instance.MiniGameManager.TriggerGestureResult(result);
        }
        else
        {
            countdown.text = "" + time;
        }
    }

    public void RestartTimer()
    {
        startTimer = true;
        time = startTimeValue;
        countdown.text = "" + time;
    }

    //Call on MiniGame Start
    public void LoadCheckPoints()
    {
        noList = first = middle = last = invalid = timeout = false;
        score = 0;
        //RestartTimer();
        if(checkpointParent != null)
        {
            foreach(Transform child in checkpointParent)
            {
                child.gameObject.SetActive(false);
            }
        }
        checkpointParent = null;
        checkpointParent = transform.Find(GameManager.Instance.MiniGameManager.currMiniGame.currentGesture.ToString());

        if (checkpointParent == null)
        {
            noList = true;
            return;
        }

        foreach (Transform child in checkpointParent)
        {
            checkpoints.Add(child.gameObject.GetComponent<Checkpoint>());
        }

    }

    public void SetFirst(GameObject actor)
    {
        first = true;
        this.actor = actor;
    }
    public void SetMiddle()
    {
        middle = true;
    }
    public void SetLast()
    {
        last = true;
    }

    public void SetInvalid()
    {
        invalid = true;
    }

    GestureEvaluationResult ComputeResult()
    {
        GestureEvaluationResult.GESTURE_PERFORMANCE performance = GestureEvaluationResult.GESTURE_PERFORMANCE.BAD;
        if (invalid)
        {
            score -= invalidPenalty;
        }

        if (first && middle && timeout) {
            performance = GestureEvaluationResult.GESTURE_PERFORMANCE.GOOD;
        }
        else if (first && timeout) {
            performance = GestureEvaluationResult.GESTURE_PERFORMANCE.OK;
        }
        else if(first && middle && last)
        {
            performance = GestureEvaluationResult.GESTURE_PERFORMANCE.PERFECT;
        }
        else if (first && last)
        {
            performance = GestureEvaluationResult.GESTURE_PERFORMANCE.VERY_GOOD;
        }
        GestureEvaluationResult result = new GestureEvaluationResult(GameManager.Instance.MiniGameManager.currMiniGame.currentGesture, performance);
        result.score = score;
        return result;
    }

    public void IncreaseScore()
    {
        score += scoreIncrement;
    }

    public GameObject GetActor()
    {
        return actor;
    }

    public bool GetFirst()
    {
        return first;
    }
}