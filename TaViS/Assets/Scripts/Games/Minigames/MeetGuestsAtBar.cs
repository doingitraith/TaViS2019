using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeetGuestsAtBar : MiniGame
{
    public override void OnGameStarted()
    {
        base.OnGameStarted();
        Id = GameID.GAME_ID.TIP_HAT_DRINK;
        GameName = Id.ToString();
        GestureNames = GameManager.Instance.GestureManager.currentGestureNames;
        currentStep = 0;
        if (GestureNames.Count == 0)
            throw new System.Exception("Gestures for " + GameName + " empty");

        currentGesture = GestureNames[currentStep];
        GameManager.Instance.GestureManager.checkPoints = GameObject.Find(GestureNames[currentStep].ToString());
        ToggleChildrenActivation(GameManager.Instance.GestureManager.checkPoints, true);

        Debug.Log("Game started: " + GameName);
        Debug.Log("Current gesture: " + currentGesture);
    }

    public override void OnGameNextStep()
    {
        base.OnGameNextStep();
        Debug.Log("Current gesture: " + currentGesture);
    }

    public override void OnGameFailed()
    {
        base.OnGameFailed();
        Debug.Log("Game failed: " + GameName);
        ToggleChildrenActivation(GameManager.Instance.GestureManager.checkPoints, false);
        throw new System.NotImplementedException();
    }

    public override void OnGameFinished()
    {
        base.OnGameFinished();
        Debug.Log("Game finished: " + GameName);
        ToggleChildrenActivation(GameManager.Instance.GestureManager.checkPoints, false);
        throw new System.NotImplementedException();
    }

    public override void ResetGame()
    {
        Debug.Log("Game reset: " + GameName);
        throw new System.NotImplementedException();
    }
}
