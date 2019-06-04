using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//used to send a gesturename and its score to minigame or gamemanager in gesturemanager
public class GestureEvaluationResult
{

    public GestureManager.GESTURENAME gestureName;
    public GESTURE_PERFORMANCE performance;
    public int score = 0;

    public enum GESTURE_PERFORMANCE
    {
        INVALID,
        BAD,
        OK,
        GOOD,
        VERY_GOOD,
        PERFECT
    }

    public GestureEvaluationResult(GestureManager.GESTURENAME gestureName, GESTURE_PERFORMANCE performance)
    {
        this.gestureName = gestureName;
        this.performance = performance;
    }
}
