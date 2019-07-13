﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureManager : MonoBehaviour
{
    public int scoreThresholdBad = 2150;
    public int scoreThresholdOk = 1950;
    public int scoreThresholdGood = 1850;
    public int scoreThresholdVeryGood = 1650;
    public int scoreThresholdPerfect = 1550;
    public float confidenceThresholdBad = .05f;
    public float confidenceThresholdOk = .1f;
    public float confidenceThresholdGood = .12f;
    public float confidenceThresholdVeryGood = .16f;
    public float confidenceThresholdPerfect = .2f;

    public float gestureSegmentTolerance = 2.5f;

    public List<GESTURENAME> currentGestureNames = new List<GESTURENAME>();
    public GameObject checkPoints;

    private GestureController gc;
    //private MiniGameManager miniGameManager;
    private GameID.GAME_ID currentGame;
    private Dictionary<GameID.GAME_ID, List<Gesture>> gesturesByGame;
    private Dictionary<GameID.GAME_ID, List<GESTURENAME>> namesByGame;

    private int gesturePauseCount = 0;

    public bool isDetecting;

    //available gesture names for gamemanager,please keep updated
    public enum GESTURENAME
    {
        /*SWIPE_LEFT,
        WAVE_LEFT,
        PULL_LEFT,
        SWIPE_LEG,
        */
        TAKE_PHOTO,
        RAISE_ARMS,
        DRINK,
        TIP_HAT,
        DANCE_RAISE_ARMS,
        DANCE_WAVE_RIGHT_LEG,
        DANCE_WAVE_LEFT_LEG,
        DANCE3,
        BALANCE,
        DANCE_DISCO_ARM_LEFT,
        DANCE_DISCO_ARM_RIGHT,
        DANCE_HAM_LEFT,
        DANCE_HAM_RIGHT,
        DANCE_LEFT_LEG_Z,
        DANCE_RIGHT_LEG_Z,
        END
    }

    public GESTURENAME ConvertStringToGestureName(string gestureID)
    {
        gestureID = gestureID.Replace(" ", "");
        gestureID = gestureID.Replace("_", "");
        gestureID = gestureID.ToLower();
        switch (gestureID)
        {
            case "tiphat": return GESTURENAME.TIP_HAT;
            case "drink": return GESTURENAME.DRINK;
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
        namesByGame = new Dictionary<GameID.GAME_ID, List<GESTURENAME>>();
        InitGameDictionary();
        InitNameDictionary();
        //LoadGameGestures(currentGame);
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
            if (gesturesByGame.TryGetValue(currentGame, out currentGesture))
            {
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
            // TAKE PHOTO
            case GESTURENAME.TAKE_PHOTO: output = GESTURENAME.TAKE_PHOTO.ToString(); break;

            // MEET GUESTS
            case GESTURENAME.DRINK: output = GESTURENAME.DRINK.ToString(); break;
            case GESTURENAME.TIP_HAT: output = GESTURENAME.TIP_HAT.ToString(); break;

            // DANCE
            case GESTURENAME.DANCE_RAISE_ARMS: output = GESTURENAME.DANCE_RAISE_ARMS.ToString(); break;
            case GESTURENAME.DANCE_WAVE_LEFT_LEG: output = GESTURENAME.DANCE_WAVE_LEFT_LEG.ToString(); break;
            case GESTURENAME.DANCE_WAVE_RIGHT_LEG: output = GESTURENAME.DANCE_WAVE_RIGHT_LEG.ToString(); break;
            case GESTURENAME.DANCE_DISCO_ARM_LEFT: output = GESTURENAME.DANCE_DISCO_ARM_LEFT.ToString(); break;
            case GESTURENAME.DANCE_DISCO_ARM_RIGHT: output = GESTURENAME.DANCE_DISCO_ARM_RIGHT.ToString(); break;
            case GESTURENAME.DANCE_HAM_LEFT: output = GESTURENAME.DANCE_HAM_LEFT.ToString(); break;
            case GESTURENAME.DANCE_HAM_RIGHT: output = GESTURENAME.DANCE_HAM_RIGHT.ToString(); break;
            case GESTURENAME.DANCE_LEFT_LEG_Z: output = GESTURENAME.DANCE_LEFT_LEG_Z.ToString(); break;
            case GESTURENAME.DANCE_RIGHT_LEG_Z: output = GESTURENAME.DANCE_RIGHT_LEG_Z.ToString(); break;

            // END
            case GESTURENAME.END: output = GESTURENAME.END.ToString(); break;

            default: output = "UnknownGesture"; break;
        }
        Debug.Log(output);
        //the gamelogic to perform --> minigame manager
        if (e.GestureName.Equals(GameManager.Instance.MiniGameManager.currMiniGame.currentGesture))
        {
            //Debug.Log(output + " Recognized");
            if(GameManager.Instance.MiniGameManager.currMiniGame.Id.Equals(GameID.GAME_ID.DANCE))
                StopDetecting();

            TriggerGestureResult(e.GestureName, false, null);
        }
    }

    //declare all your gestures here and assign the correct GameID.GAME_ID 
    void InitGameDictionary()
    {
        //gc.GestureRecognizedInController += OnGestureRecognized;

        //MEET GUESTS
        IRelativeGestureSegment[] tipHat = { new TipHatSegment1(), new TipHatSegment2(), new TipHatSegment1() };

        IRelativeGestureSegment[] drink = { new DrinkSegment1(), new DrinkSegment2(), new DrinkSegment3() };

        gesturesByGame.Add(GameID.GAME_ID.TIP_HAT_DRINK, new List<Gesture> { new Gesture(GESTURENAME.TIP_HAT, tipHat), new Gesture(GESTURENAME.DRINK, drink) });

        //DANCE
        IRelativeGestureSegment[] raiseArms = { new RaiseArmsSegment1(), new RaiseArmsSegment2() };
        IRelativeGestureSegment[] waveRightLeg = { new SwipeRightLegSegment1(), new SwipeRightLegSegment2() };
        IRelativeGestureSegment[] waveLefttLeg = { new SwipeLeftLegSegment1(), new SwipeLeftLegSegment2() };

        IRelativeGestureSegment[] discoArmLeft = { new DiscoArmLeftSegment1(), new DiscoArmLeftSegment2()};
        IRelativeGestureSegment[] discoArmRight = { new DiscoArmRightSegment1(), new DiscoArmRightSegment2()};
        IRelativeGestureSegment[] hamLeft = { new HamLeftSegment1(), new HamLeftSegment2()};
        IRelativeGestureSegment[] hamRight = { new HamRightSegment1(), new HamRightSegment2()};
        IRelativeGestureSegment[] LeftLegZ = { new LeftLegZSegment1(), new LeftLegZSegment2() };
        IRelativeGestureSegment[] RightLegZ = { new RightLegZSegment1(), new RightLegZSegment2() };

        gesturesByGame.Add(GameID.GAME_ID.DANCE, new List<Gesture> { new Gesture(GESTURENAME.DANCE_RAISE_ARMS, raiseArms),
                                                                     new Gesture(GESTURENAME.DANCE_WAVE_RIGHT_LEG, waveRightLeg),
                                                                     new Gesture(GESTURENAME.DANCE_WAVE_LEFT_LEG, waveLefttLeg),
                                                                     new Gesture(GESTURENAME.DANCE_DISCO_ARM_LEFT, discoArmLeft),
                                                                     new Gesture(GESTURENAME.DANCE_DISCO_ARM_RIGHT, discoArmRight),
                                                                     new Gesture(GESTURENAME.DANCE_HAM_LEFT, hamLeft),
                                                                     new Gesture(GESTURENAME.DANCE_HAM_RIGHT, hamRight),
                                                                     new Gesture(GESTURENAME.DANCE_LEFT_LEG_Z, LeftLegZ),
                                                                     new Gesture(GESTURENAME.DANCE_RIGHT_LEG_Z, RightLegZ)
                                                                    });

    }

    void InitNameDictionary()
    {
        //gc.GestureRecognizedInController += OnGestureRecognized;

        //TAKE PHOT
        namesByGame.Add(GameID.GAME_ID.START, new List<GESTURENAME> {GESTURENAME.TAKE_PHOTO});

        //MEET GUESTS
        namesByGame.Add(GameID.GAME_ID.TIP_HAT_DRINK, new List<GESTURENAME> {GESTURENAME.TIP_HAT,
                                                                             GESTURENAME.DRINK});

        //DANCE
        // TODO: Make dance moves random
        namesByGame.Add(GameID.GAME_ID.DANCE, new List<GESTURENAME> {GESTURENAME.DANCE_RAISE_ARMS,
                                                                     GESTURENAME.DANCE_WAVE_RIGHT_LEG,
                                                                     GESTURENAME.DANCE_WAVE_LEFT_LEG,

                                                                     GESTURENAME.DANCE_DISCO_ARM_LEFT,
                                                                     GESTURENAME.DANCE_DISCO_ARM_RIGHT,
                                                                     GESTURENAME.DANCE_HAM_LEFT,
                                                                     GESTURENAME.DANCE_HAM_RIGHT,
                                                                     GESTURENAME.DANCE_LEFT_LEG_Z,
                                                                     GESTURENAME.DANCE_RIGHT_LEG_Z
                                                                     });
        //BALANCE
        namesByGame.Add(GameID.GAME_ID.BALANCE_TABLET, new List<GESTURENAME> {GESTURENAME.BALANCE});

        namesByGame.Add(GameID.GAME_ID.END, new List<GESTURENAME> { GESTURENAME.END });
    }

    void TriggerGestureResult(GESTURENAME recognizedGesture, bool resultFromConfidence, Nullable<float> confidence)
    {
        if (recognizedGesture == GameManager.Instance.MiniGameManager.currMiniGame.currentGesture)
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
    }

    GestureEvaluationResult GetPointsFromEvaluation(GESTURENAME recognizedGesture, bool useConfidence, Nullable<float> confidence)
    {
        GestureEvaluationResult.GESTURE_PERFORMANCE performance = GestureEvaluationResult.GESTURE_PERFORMANCE.INVALID;
        if (!useConfidence)
        {
            if (gesturePauseCount <= scoreThresholdBad)
            {
                performance = GestureEvaluationResult.GESTURE_PERFORMANCE.BAD;
            }
            if (gesturePauseCount <= scoreThresholdOk)
            {
                performance = GestureEvaluationResult.GESTURE_PERFORMANCE.OK;
            }
            if (gesturePauseCount <= scoreThresholdGood)
            {
                performance = GestureEvaluationResult.GESTURE_PERFORMANCE.GOOD;
            }
            if (gesturePauseCount <= scoreThresholdVeryGood)
            {
                performance = GestureEvaluationResult.GESTURE_PERFORMANCE.VERY_GOOD;
            }
            if (gesturePauseCount <= scoreThresholdPerfect)
            {
                performance = GestureEvaluationResult.GESTURE_PERFORMANCE.PERFECT;
            }

            //Debug.Log("Performance from Pause Events: " + performance.ToString() + " Pause: " + gesturePauseCount);
            gesturePauseCount = 0;
        }
        else
        {
            if (confidence <= confidenceThresholdPerfect)
            {
                performance = GestureEvaluationResult.GESTURE_PERFORMANCE.PERFECT;
            }
            if (confidence <= confidenceThresholdVeryGood)
            {
                performance = GestureEvaluationResult.GESTURE_PERFORMANCE.VERY_GOOD;
            }
            if (confidence <= confidenceThresholdGood)
            {
                performance = GestureEvaluationResult.GESTURE_PERFORMANCE.GOOD;
            }
            if (confidence <= confidenceThresholdOk)
            {
                performance = GestureEvaluationResult.GESTURE_PERFORMANCE.OK;
            }
            if (confidence <= confidenceThresholdBad)
            {
                performance = GestureEvaluationResult.GESTURE_PERFORMANCE.BAD;
            }
            Debug.Log("Performance from Confidence: " + performance.ToString());
        }
        return new GestureEvaluationResult(recognizedGesture, performance);
    }

    public void VisualBuilderGestureRecognized(GESTURENAME name, float confidence)
    {
        if (!currentGestureNames.Contains(name) || !isDetecting)
        {
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
        // TODO: set difficulties
        switch (result.gestureName)
        {
            case GESTURENAME.DRINK: difficultyMod = 2; break;
            default: break;
        }

        return difficultyMod * (int)result.performance;
    }

    public void StartDetecting()
    {
        gc.GestureRecognizedInController += OnGestureRecognized;
        isDetecting = true;
    }

    public void StopDetecting()
    {
        gc.GestureRecognizedInController -= OnGestureRecognized;
        isDetecting = false;
    }
}
