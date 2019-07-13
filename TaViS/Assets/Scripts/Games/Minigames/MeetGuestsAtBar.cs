using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeetGuestsAtBar : MiniGame
{
    public GameObject Hat;
    public GameObject Glass;

    private void Start()
    {
        Id = GameID.GAME_ID.TIP_HAT_DRINK;
    }
    public override void OnGameStarted()
    {
        base.OnGameStarted();
        //activate
        GameManager.Instance.GestureManager.StopDetecting();
        Glass.GetComponentInChildren<Collider>().enabled = false;
        Hat.GetComponentInChildren<Collider>().enabled = true;

        Debug.Log("Game started: " + GameName);
        Debug.Log("Current gesture: " + currentGesture);
    }

    public override void OnGameNextStep()
    {
        base.OnGameNextStep();
        if(currentGesture == GestureManager.GESTURENAME.DRINK)
        {
            Glass.GetComponentInChildren<Collider>().enabled = true;
            Hat.GetComponentInChildren<Collider>().enabled = false;
            Hat.GetComponent<Hat>().ReleaseObject();
        }
        Debug.Log("Current gesture: " + currentGesture);
    }

    public override void OnGameFailed()
    {
        base.OnGameFailed();
        CleanUp();
        Debug.Log("Game failed: " + GameName);
        throw new System.NotImplementedException();
    }

    public override void OnGameFinished()
    {
        base.OnGameFinished();
        CleanUp();
        Debug.Log("Game finished: " + GameName);
    }

    public override void ResetGame()
    {
        Debug.Log("Game reset: " + GameName);
        throw new System.NotImplementedException();
    }

    private void CleanUp()
    {
        foreach(Checkpoint checkpoint in GameManager.Instance.CheckpointManager.checkpoints)
        {
            checkpoint.gameObject.SetActive(false);
        }
        Glass.GetComponent<MartiniGlass>().ReleaseObject();
        Glass.GetComponentInChildren<Collider>().enabled = false;
        Glass.transform.SetParent(null);
        Glass.transform.position = Glass.GetComponent<PickableObject>().targetOnObjectToMoveTo.position;
        Hat.GetComponentInChildren<Collider>().enabled = false;
    }
}
