using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DanceHighlight : MonoBehaviour
{

    bool isFirstMove = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!isFirstMove)
        {
            Debug.Log(" STOP - " + GameManager.Instance.MiniGameManager.currMiniGame.currentGesture.ToString());
            GameManager.Instance.GestureManager.StopDetecting();
            GameManager.Instance.MiniGameManager.currMiniGame.OnGameNextStep();
        }

        Debug.Log("START - " + GameManager.Instance.MiniGameManager.currMiniGame.currentGesture.ToString());
        GameManager.Instance.GestureManager.StartDetecting();
        isFirstMove = false;
    }

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
