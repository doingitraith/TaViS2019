using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//used to send a gesturename and its score to minigame or gamemanager in gesturemanager
public class GestureEvaluationResult
{

    public GestureManager.GESTURENAME gestureName;
    public GESTURE_PERFORMANCE performance;
    public int score = 0;

    //performance values and basic score corresponding to it
    public enum GESTURE_PERFORMANCE
    {
        NONE = 0,
        INVALID = -300,
        BAD = -300,
        OK = 100,
        GOOD = 300,
        VERY_GOOD = 600,
        PERFECT = 900
    }

    public GestureEvaluationResult(GestureManager.GESTURENAME gestureName, GESTURE_PERFORMANCE performance)
    {
        this.gestureName = gestureName;
        this.performance = performance;
    }
}
