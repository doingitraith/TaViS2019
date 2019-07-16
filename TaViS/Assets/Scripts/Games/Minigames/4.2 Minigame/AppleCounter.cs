using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleCounter : MonoBehaviour
{
    float result;
    int counter = 4;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "apple")
        {
            counter--;
            GetComponent<AudioSource>().Play();
            Debug.Log("apple detected");
        }
    }

    private void Update()
    {
        /*
        if(counter <= 0)
        {
            GestureEvaluationResult result = new GestureEvaluationResult(GestureManager.GESTURENAME.BALANCE, GestureEvaluationResult.GESTURE_PERFORMANCE.BAD);
            result.score = 0;
            GameManager.Instance.MiniGameManager.TriggerGestureResult(result);
        }
        if(counter == 1)
        {
            GestureEvaluationResult result = new GestureEvaluationResult(GestureManager.GESTURENAME.BALANCE, GestureEvaluationResult.GESTURE_PERFORMANCE.OK);
            result.score = 100;
            GameManager.Instance.MiniGameManager.TriggerGestureResult(result);
        }
        */
    }

    public int getAppleCount()
    {
        return counter;
    }
}
