using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MiniGame : MonoBehaviour
{
    public GameID.GAME_ID Id;
    public BasicAvatarController eric;
    public string GameName;
    public Transform StartPosition;
    public Quaternion CameraStartRotation;
    public List<GestureManager.GESTURENAME> GestureNames;
    public bool isFinished;
    public bool isFailed;
    public bool hasCheckpoints;
    public GestureManager.GESTURENAME currentGesture;

    protected int currentStep;

    private void Start()
    {
        eric = GameObject.Find("Eric").GetComponent<BasicAvatarController>();
    }

    public virtual void OnGameStarted()
    {
        isFinished = false;
        isFailed = false;
        GameName = Id.ToString();
        GestureNames = GameManager.Instance.GestureManager.currentGestureNames;
        //currentStep = 0;
        if (GestureNames.Count == 0)
            throw new System.Exception("Gestures for " + GameName + " empty");

        currentGesture = GestureNames[currentStep];
        if (hasCheckpoints)
        {
            GameManager.Instance.CheckpointManager.LoadCheckPoints();
            GameManager.Instance.GestureManager.checkPoints = GameObject.Find(GestureNames[currentStep].ToString());
            ToggleChildrenActivation(GameManager.Instance.GestureManager.checkPoints, true);
        }
    }

    public virtual void OnGameNextStep()
    {
        ++currentStep;
        if (currentStep < GestureNames.Count)
        {
            currentGesture = GestureNames[currentStep];
            if (hasCheckpoints)
            {
                GameManager.Instance.CheckpointManager.LoadCheckPoints();
                GameManager.Instance.GestureManager.checkPoints = GameObject.Find(GestureNames[currentStep].ToString());
                ToggleChildrenActivation(GameManager.Instance.GestureManager.checkPoints, true);
            }
        }
        else
            OnGameFinished();
    }

    public virtual void OnGameFinished()
    {
        isFinished = true;
        if (hasCheckpoints)
            ToggleChildrenActivation(GameManager.Instance.GestureManager.checkPoints, false);
    }

    public virtual void OnGameFailed()
    {
        isFailed = true;
        if(hasCheckpoints)
            ToggleChildrenActivation(GameManager.Instance.GestureManager.checkPoints, false);
    }
    public abstract void ResetGame();

    protected void ToggleChildrenActivation(GameObject checkpoints, bool enabled)
    {
        checkpoints.SetActive(enabled);

        foreach (Transform child in checkpoints.transform)
        {
            ToggleChildrenActivation(child.gameObject, enabled);
        }
    }
}
