using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MiniGame : MonoBehaviour
{
    public GameID.GAME_ID Id;
    public string GameName;
    public Transform StartPosition;
    public Quaternion CameraStartRotation;
    public List<GestureManager.GESTURENAME> GestureNames;
    public bool isFinished;
    public bool isFailed;
    public GestureManager.GESTURENAME currentGesture;

    protected int currentStep;

    public virtual void OnGameStarted()
    {
        isFinished = false;
        isFailed = false;
    }

    public virtual void OnGameNextStep()
    {
        ++currentStep;
        if (currentStep < GestureNames.Count)
            currentGesture = GestureNames[currentStep];
        else
            OnGameFinished();
    }

    public virtual void OnGameFinished()
    {
        isFinished = true;
    }

    public virtual void OnGameFailed()
    {
        isFailed = true;
    }
    public abstract void ResetGame();
}
