using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DanceHighlight : MonoBehaviour
{

    bool isFirstMove = true;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!isFirstMove)
        {
            //Debug.Log(" STOP - " + GameManager.Instance.MiniGameManager.currMiniGame.currentGesture.ToString());
            GameManager.Instance.GestureManager.StopDetecting();

            //default result if no guesture recognized
            if (!GameManager.Instance.GestureManager.hasDetected)
            {
                GestureEvaluationResult result = new GestureEvaluationResult(GameManager.Instance.MiniGameManager.currMiniGame.currentGesture, GestureEvaluationResult.GESTURE_PERFORMANCE.OK);
                result.score = (int)GestureEvaluationResult.GESTURE_PERFORMANCE.BAD;
                GameManager.Instance.MiniGameManager.TriggerGestureResult(result);
            }
            GameManager.Instance.MiniGameManager.currMiniGame.OnGameNextStep();
        }

        //Debug.Log("START - " + GameManager.Instance.MiniGameManager.currMiniGame.currentGesture.ToString());
        GameManager.Instance.GestureManager.StartDetecting();
        isFirstMove = false;
    }

    //load next guesture
    private void OnTriggerExit2D(Collider2D collider)
    {
        /*
        Debug.Log(" STOP - " + GameManager.Instance.MiniGameManager.currMiniGame.currentGesture.ToString());
        GameManager.Instance.GestureManager.StopDetecting();
        
        if (isFirstMove)
            isFirstMove = false;
        */
        if (collider.gameObject.name.Equals("MoveImage_last"))
        {
            GameManager.Instance.MiniGameManager.currMiniGame.OnGameNextStep();
        }
    }
}
