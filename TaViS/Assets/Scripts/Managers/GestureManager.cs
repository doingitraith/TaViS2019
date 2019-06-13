using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureManager : MonoBehaviour
{
    public int scoreThresholdBad = 1150;
    public int scoreThresholdOk = 950;
    public int scoreThresholdGood = 850;
    public int scoreThresholdVeryGood = 650;
    public int scoreThresholdPerfect = 550;

    private GestureController gc;
    //private MiniGameManager miniGameManager;
    private GameID.GAME_ID currentGame;
    private Dictionary<GameID.GAME_ID, List<Gesture>> gesturesByGame;

    private int gesturePauseCount = 0;

    //available gesture names for gamemanager,please keep updated
    public enum GESTURENAME
    {
        SWIPE_LEFT,
        WAVE_LEFT,
        PULL_LEFT,
        RAISE_ARMS,
        SWIPE_LEG
    }
    
    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.Find("GestureController").GetComponent<GestureController>();
        //miniGameManager = GameObject.Find("MiniGameManager").GetComponent<MiniGameManager>();
        currentGame = GameManager.Instance.CurrentGame;
        gesturesByGame = new Dictionary<GameID.GAME_ID, List<Gesture>>();
        InitDictionary();
        LoadGameGestures(currentGame);
        currentGame = GameID.GAME_ID.START;
    }

    public void LoadGameGestures(GameID.GAME_ID newGameID)
    {
        currentGame = newGameID;
        gc.GetGestures().Clear();
        //load currently possible gestures
        if (gesturesByGame.ContainsKey(currentGame))
        {
            List<Gesture> currentGesture = null;
            if(gesturesByGame.TryGetValue(currentGame, out currentGesture)){
                gc.AddExistingGestures(currentGesture);
            }
            else
            {
                Debug.Log("Unable to get value from key " + currentGame);
            }
        }
        else
        {
            Debug.Log("No Gestures Found, please add to the dictionary");
        }
    }
    void OnGestureRecognized(object sender, GestureEventArgs e)
    {
        string output = "";
        switch (e.GestureName)
        {
            case GESTURENAME.SWIPE_LEFT: output = GESTURENAME.SWIPE_LEFT.ToString(); break;
            case GESTURENAME.WAVE_LEFT: output = GESTURENAME.WAVE_LEFT.ToString(); break;
            case GESTURENAME.RAISE_ARMS: output = GESTURENAME.RAISE_ARMS.ToString(); break;
            case GESTURENAME.SWIPE_LEG: output = GESTURENAME.SWIPE_LEG.ToString(); break;
            case GESTURENAME.PULL_LEFT: output = GESTURENAME.PULL_LEFT.ToString(); break;
          
            default: output = "UnknownGesture"; break;
        }
        Debug.Log(output + "Recognized");
        //the gamelogic to perform --> minigame manager
        TriggerGestureResult(e.GestureName);
    }

    //declare all your gestures here and assign the correct GameID.GAME_ID 
    void InitDictionary()
    {
        List<Gesture> gestureList = new List<Gesture>();
        gc.GestureRecognizedInController += OnGestureRecognized;

        IRelativeGestureSegment[] swipeLeft = { new SwipeToLeftSegment1(), new SwipeToLeftSegment2(), new SwipeToLeftSegment3() };
        gestureList.Add(new Gesture(GESTURENAME.SWIPE_LEFT, swipeLeft));

        IRelativeGestureSegment[] raiseArms = { new RaiseArmsSegment1(), new RaiseArmsSegment2() };
        gestureList.Add(new Gesture(GESTURENAME.RAISE_ARMS, raiseArms));

        IRelativeGestureSegment[] swipeLeg = { new SwipeRightLegSegment1(), new SwipeRightLegSegment2() };
        gestureList.Add(new Gesture(GESTURENAME.SWIPE_LEG, swipeLeg));

        IRelativeGestureSegment[] waveLeft = { new WaveLeftSegment1(), new WaveLeftSegment2(),
                                             new WaveLeftSegment1(),new WaveLeftSegment2(),
                                             new WaveLeftSegment1(),new WaveLeftSegment2() };
        gestureList.Add(new Gesture(GESTURENAME.WAVE_LEFT, waveLeft));

        IRelativeGestureSegment[] pullLeft = { new PullToLeftSegment1(), new PullToLeftSegment2(), new PullToLeftSegment3(), };
        gestureList.Add(new Gesture(GESTURENAME.PULL_LEFT, pullLeft));

        gesturesByGame.Add(GameID.GAME_ID.START, gestureList);
    }

    void TriggerGestureResult(GESTURENAME recognizedGesture)
    {
        GestureEvaluationResult result = GetPointsFromEvaluation(recognizedGesture);
        result.score = GetFinalGestureEvaluationResult(GetPointsFromEvaluation(recognizedGesture));
        GameManager.Instance.MiniGameManager.TriggerGestureResult(result);
    }

    GestureEvaluationResult GetPointsFromEvaluation(GESTURENAME recognizedGesture)
    {
        GestureEvaluationResult.GESTURE_PERFORMANCE performance = GestureEvaluationResult.GESTURE_PERFORMANCE.INVALID;
        if (gesturePauseCount <= scoreThresholdPerfect)
        {
            performance = GestureEvaluationResult.GESTURE_PERFORMANCE.PERFECT;
        }
        else if (gesturePauseCount <= scoreThresholdVeryGood)
        {
            performance = GestureEvaluationResult.GESTURE_PERFORMANCE.VERY_GOOD;
        }
        else if (gesturePauseCount <= scoreThresholdGood)
        {
            performance = GestureEvaluationResult.GESTURE_PERFORMANCE.GOOD;
        }
        else if (gesturePauseCount <= scoreThresholdOk)
        {
            performance = GestureEvaluationResult.GESTURE_PERFORMANCE.OK;
        }
        else if (gesturePauseCount <= scoreThresholdBad)
        {
            performance = GestureEvaluationResult.GESTURE_PERFORMANCE.BAD;
        }
        
        Debug.Log(performance.ToString() + " Score: " + gesturePauseCount);
        gesturePauseCount = 1;
        return new GestureEvaluationResult(recognizedGesture, performance);
    }

    public void IncreasePauseCount()
    {
        gesturePauseCount++;
    }

    int GetFinalGestureEvaluationResult(GestureEvaluationResult result)
    {
        int difficultyMod = 1;
        //set the difficulty of your gestures here!
        switch (result.gestureName)
        {
            case GESTURENAME.RAISE_ARMS: difficultyMod = 2; break;
            default: break;
        }

        return difficultyMod * (int)result.performance;

    }
}
