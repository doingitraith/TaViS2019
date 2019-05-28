using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//used to send a gesturename and its score to minigame or gamemanager in gesturemanager
public class GestureEvaluationResult
{
    private GestureManager.GESTURENAME gestureName;
    private GESTURE_PERFORMANCE performance;
    public enum GESTURE_PERFORMANCE
    {
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
