﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureManager : MonoBehaviour
{
    private GestureController gc;
    private MiniGameManager miniGameManager;
    private GameID.GAME_ID currentGame;
    private Dictionary<GameID.GAME_ID, Gesture> gesturesByGame;

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
        miniGameManager = GameObject.Find("MiniGameManager").GetComponent<MiniGameManager>();
        currentGame = GameID.GAME_ID.START;

        initDictionary();
        UpdateGestures(currentGame);
    }

    public void UpdateGestures(GameID.GAME_ID newGameID)
    {
        currentGame = newGameID;
        gc.GetGestures().Clear();
        //load currently possible gestures
        if (gesturesByGame.ContainsKey(currentGame))
        {
            Gesture currentGesture = null;
            if(gesturesByGame.TryGetValue(currentGame, out currentGesture)){
                gc.AddExistingGesture(currentGesture);
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
    void initDictionary()
    {
        gc.GestureRecognizedInController += OnGestureRecognized;

        IRelativeGestureSegment[] swipeLeft = { new SwipeToLeftSegment1(), new SwipeToLeftSegment2(), new SwipeToLeftSegment3() };
        gesturesByGame.Add(GameID.GAME_ID.MISCELLANEOUS, new Gesture(GESTURENAME.SWIPE_LEFT, swipeLeft));

        IRelativeGestureSegment[] raiseArms = { new RaiseArmsSegment1(), new RaiseArmsSegment2() };
        gesturesByGame.Add(GameID.GAME_ID.MISCELLANEOUS, new Gesture(GESTURENAME.RAISE_ARMS, raiseArms));

        IRelativeGestureSegment[] swipeLeg = { new SwipeRightLegSegment1(), new SwipeRightLegSegment2() };
        gesturesByGame.Add(GameID.GAME_ID.MISCELLANEOUS, new Gesture(GESTURENAME.SWIPE_LEG, swipeLeg));

        IRelativeGestureSegment[] waveLeft = { new WaveLeftSegment1(), new WaveLeftSegment2(),
                                             new WaveLeftSegment1(),new WaveLeftSegment2(),
                                             new WaveLeftSegment1(),new WaveLeftSegment2() };
        gesturesByGame.Add(GameID.GAME_ID.MISCELLANEOUS, new Gesture(GESTURENAME.WAVE_LEFT, waveLeft));

        IRelativeGestureSegment[] pullLeft = { new PullToLeftSegment1(), new PullToLeftSegment2(), new PullToLeftSegment3(), };
        gesturesByGame.Add(GameID.GAME_ID.MISCELLANEOUS, new Gesture(GESTURENAME.PULL_LEFT, pullLeft));
    }

    void TriggerGestureResult(GESTURENAME recognizedGesture)
    {
        ReturnPointsFromEvaluation(recognizedGesture);
        miniGameManager.TriggerGestureResult(new GestureEvaluationResult(recognizedGesture, ReturnPointsFromEvaluation(recognizedGesture)));
    }

    GestureEvaluationResult.GESTURE_PERFORMANCE ReturnPointsFromEvaluation(GESTURENAME recognizedGesture)
    {
        //todo evaluation...
        return GestureEvaluationResult.GESTURE_PERFORMANCE.PERFECT;
    }
}
