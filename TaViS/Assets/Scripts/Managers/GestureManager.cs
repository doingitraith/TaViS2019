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
    public float confidenceThresholdBad = .05f;
    public float confidenceThresholdOk = .1f;
    public float confidenceThresholdGood = .12f;
    public float confidenceThresholdVeryGood = .16f;
    public float confidenceThresholdPerfect = .2f;

    public float gestureSegmentTolerance = 2.5f;

    private GestureController gc;
    //private MiniGameManager miniGameManager;
    private GameID.GAME_ID currentGame;
    private Dictionary<GameID.GAME_ID, List<Gesture>> gesturesByGame;
    private Dictionary<GameID.GAME_ID, List<GESTURENAME>> namesByGame;
    private List<GESTURENAME> currentGestureNames = new List<GESTURENAME>();

    private int gesturePauseCount = 0;

    //available gesture names for gamemanager,please keep updated
    public enum GESTURENAME
    {
        /*SWIPE_LEFT,
        WAVE_LEFT,
        PULL_LEFT,
        SWIPE_LEG,
        */
        RAISE_ARMS,
        DRINK,
        TIP_HAT
    }

    public GESTURENAME ConvertStringToGestureName(string gestureID)
    {
        gestureID.Replace(" ", "");
        gestureID.Replace("_", "");
        gestureID.ToLower();
        switch (gestureID)
        {
            case "drink": return GESTURENAME.DRINK;
            case "tiphat": return GESTURENAME.TIP_HAT;
            default: return GESTURENAME.RAISE_ARMS;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.Find("GestureController").GetComponent<GestureController>();
        //miniGameManager = GameObject.Find("MiniGameManager").GetComponent<MiniGameManager>();
        currentGame = GameManager.Instance.CurrentGame;
        gesturesByGame = new Dictionary<GameID.GAME_ID, List<Gesture>>();
        InitGameDictionary();
        InitNameDictionary();
        LoadGameGestures(currentGame);
        //currentGame = GameID.GAME_ID.START;
    }

    public void LoadGameGestures(GameID.GAME_ID newGameID)
    {
        currentGame = newGameID;
        gc.GetGestures().Clear();
        currentGestureNames.Clear();
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
        if (namesByGame.ContainsKey(currentGame))
        {
            List<GESTURENAME> currentName = null;
            if (namesByGame.TryGetValue(currentGame, out currentName))
            {
                currentGestureNames = currentName;
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
            // game 1
            case GESTURENAME.DRINK: output = GESTURENAME.DRINK.ToString(); break;
            case GESTURENAME.TIP_HAT: output = GESTURENAME.TIP_HAT.ToString(); break;
          
            default: output = "UnknownGesture"; break;
        }
        Debug.Log(output + "Recognized");
        //the gamelogic to perform --> minigame manager
        TriggerGestureResult(e.GestureName, false, null);
    }

    //declare all your gestures here and assign the correct GameID.GAME_ID 
    void InitGameDictionary()
    {
        List<Gesture> gestureList = new List<Gesture>();
        gc.GestureRecognizedInController += OnGestureRecognized;

        //GAME 1
        IRelativeGestureSegment[] drink = { new DrinkSegment1(), new DrinkSegment2(), new DrinkSegment3()};
        gestureList.Add(new Gesture(GESTURENAME.DRINK, drink));

        IRelativeGestureSegment[] tipHat = { new TipHatSegment1(), new TipHatSegment2(), new TipHatSegment1() };
        gestureList.Add(new Gesture(GESTURENAME.TIP_HAT, tipHat));

        gesturesByGame.Add(GameID.GAME_ID.TIP_HAT_DRINK, gestureList);
        //gestureList.Clear();
    }

    void InitNameDictionary()
    {
        List<GESTURENAME> nameList = new List<GESTURENAME>();
        gc.GestureRecognizedInController += OnGestureRecognized;

        //GAME 1
        nameList.Add(GESTURENAME.DRINK);
        nameList.Add(GESTURENAME.TIP_HAT);
        namesByGame.Add(GameID.GAME_ID.TIP_HAT_DRINK, nameList);
    }

    void TriggerGestureResult(GESTURENAME recognizedGesture, bool resultFromConfidence, Nullable<float> confidence)
    {
        GestureEvaluationResult result;
        if (!resultFromConfidence)
        {
            result = GetPointsFromEvaluation(recognizedGesture, false, null);
        }
        else
        {
            result = GetPointsFromEvaluation(recognizedGesture, true, confidence);
        }
        result.score = GetFinalGestureEvaluationResult(result);
        GameManager.Instance.MiniGameManager.TriggerGestureResult(result);
    }

    GestureEvaluationResult GetPointsFromEvaluation(GESTURENAME recognizedGesture, bool useConfidence, Nullable<float> confidence)
    {
        GestureEvaluationResult.GESTURE_PERFORMANCE performance = GestureEvaluationResult.GESTURE_PERFORMANCE.INVALID;
        if (!useConfidence)
        {
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
            Debug.Log("Performance from Pause Events: " + performance.ToString() + " Score: " + gesturePauseCount);
            gesturePauseCount = 1;
        }
        else
        {
            if (confidence <= confidenceThresholdPerfect)
            {
                performance = GestureEvaluationResult.GESTURE_PERFORMANCE.PERFECT;
            }
            else if (confidence <= confidenceThresholdVeryGood)
            {
                performance = GestureEvaluationResult.GESTURE_PERFORMANCE.VERY_GOOD;
            }
            else if (confidence <= confidenceThresholdGood)
            {
                performance = GestureEvaluationResult.GESTURE_PERFORMANCE.GOOD;
            }
            else if (confidence <= confidenceThresholdOk)
            {
                performance = GestureEvaluationResult.GESTURE_PERFORMANCE.OK;
            }
            else if (confidence <= confidenceThresholdBad)
            {
                performance = GestureEvaluationResult.GESTURE_PERFORMANCE.BAD;
            }
            Debug.Log("Performance from Confidence: " + performance.ToString());
        }
        return new GestureEvaluationResult(recognizedGesture, performance);
    }

    public void VisualBuilderGestureRecognized(GESTURENAME name, float confidence)
    {
        if (!currentGestureNames.Contains(name)){
            return;
        }
        Debug.Log(name.ToString() + " RECOGNIZED");
        TriggerGestureResult(name, true, confidence);
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
            case GESTURENAME.DRINK: difficultyMod = 2; break;
            default: break;
        }

        return difficultyMod * (int)result.performance;
    }
}
