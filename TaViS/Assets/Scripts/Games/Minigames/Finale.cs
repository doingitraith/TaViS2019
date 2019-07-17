using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Finale : MiniGame
{
    public GameObject trapdoorButtonText;
    public RawImage photo;
    public Text score;

    public override void OnGameFailed()
    {
        base.OnGameFailed();
    }

    public override void OnGameFinished()
    {
        base.OnGameFinished();
        //present photo
        photo.transform.SetParent(GameObject.Find("MainCanvas").transform);
        photo.gameObject.SetActive(true);
        photo.gameObject.transform.position = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);
        photo.texture = GameManager.Instance.takenPicure;
        score.gameObject.SetActive(true);
    }

    public override void OnGameNextStep()
    {
        base.OnGameNextStep();
    }

    public override void OnGameStarted()
    {
        base.OnGameStarted();
        trapdoorButtonText.SetActive(true);
    }

    public override void ResetGame()
    {
        throw new System.NotImplementedException();
    }
    private void Start()
    {
        hasCheckpoints = false;
        Id = GameID.GAME_ID.END;
    }
}
