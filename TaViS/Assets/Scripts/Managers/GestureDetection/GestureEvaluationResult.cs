using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//used to send a gesturename and its score to minigame or gamemanager in gesturemanager
public class GestureEvaluationResult
{
<<<<<<< HEAD
    public GestureManager.GESTURENAME gestureName;
    public GESTURE_PERFORMANCE performance;
    public int score = 0;
=======
    private GestureManager.GESTURENAME gestureName;
    private GESTURE_PERFORMANCE performance;
>>>>>>> parent of 50858ad... the real push from 31.5
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
