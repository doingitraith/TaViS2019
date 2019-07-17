using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

//controls minigame progression
public class MiniGameManager : MonoBehaviour
{
    private int currMiniGameIdx = 0;

    public MiniGame currMiniGame;
    public List<GameID.GAME_ID> miniGames;
    public List<GameObject> miniGameObjects;

    private bool isTransitioning;
    public Disguise disguise;
    public TakePhoto takePhoto;
    public RetakePhoto retakePhoto;
    private PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        //InitMiniGames();
        isTransitioning = false;
        playerMovement = GameManager.Instance.eric.GetComponent<PlayerMovement>(); 
        SetupMiniGame(GameManager.Instance.CurrentGame);
    }

    // Update is called once per frame
    void Update()
    {
        if (isTransitioning)
        {
            if (!playerMovement.GetWalking())
            {
                isTransitioning = false;
                StartNextGame();
            }
        }
    }

    /*
    private void InitMiniGames()
    {
        // Add Mini Game classes
        //miniGames.Add(GameID.GAME_ID.START, miniGameObjects[0].GetComponent<MiniGame>());
        miniGames.Add(GameID.GAME_ID.TIP_HAT_DRINK, miniGameObjects[1].GetComponent<MiniGame>());
        // miniGames.Add(GameID.GAME_ID.DANCE, FindObjectOfType<DanceMiniGame>());
    }
    */

    //reacts to the results received from the games
    public void TriggerGestureResult(GestureEvaluationResult result)
    {
        //handle score, trigger event with name...
        Debug.Log(currMiniGame.Id + ", " + currMiniGame.currentGesture.ToString() +
          " done: " + result.performance + ", " + result.score);

        GameManager.Instance.GestureManager.StopDetecting();

        GameManager.Instance.ChangeScore(result.score);

        //Give Feedback between guestures (with instruction for next guesture)...
        if(currMiniGame.Id.Equals(GameID.GAME_ID.TIP_HAT_DRINK) && (currMiniGame.currentGesture.Equals(GestureManager.GESTURENAME.TIP_HAT) || currMiniGame.currentGesture.Equals(GestureManager.GESTURENAME.DRINK)))
        {
            GameManager.Instance.Ui.UpdateFeedbackTextGesture(result.performance, currMiniGame.currentGesture, null, false);
        }
        //...or just the feedback
        else
        { 
            GameManager.Instance.Ui.UpdateFeedbackTextGesture(result.performance, null, null, false);
        }

        //Update Suspiciousness, might need some tweaking
        if (result.performance == GestureEvaluationResult.GESTURE_PERFORMANCE.BAD ||
            result.performance == GestureEvaluationResult.GESTURE_PERFORMANCE.INVALID)
            GameManager.Instance.ChangeSuspicousness(5.0f);
        if (result.performance == GestureEvaluationResult.GESTURE_PERFORMANCE.OK)
            GameManager.Instance.ChangeSuspicousness(3.0f);
        if (result.performance == GestureEvaluationResult.GESTURE_PERFORMANCE.VERY_GOOD)
            GameManager.Instance.ChangeSuspicousness(-3.0f);
        if (result.performance == GestureEvaluationResult.GESTURE_PERFORMANCE.PERFECT)
            GameManager.Instance.ChangeSuspicousness(-5.0f);

        if (!currMiniGame.Id.Equals(GameID.GAME_ID.DANCE))
            currMiniGame.OnGameNextStep();

        EndMiniGame();
    }

    //handles disguise result
    public void EndDisguise(bool isDisguised)
    {
        //player has avoided the guards, game continues
        if (isDisguised)
        {
            Debug.Log("NOT DETECTED");
            currMiniGameIdx++;
            GameManager.Instance.ChangeSuspicousness(-10);
            if (currMiniGameIdx < miniGames.Count)
            {
                playerMovement.SetWalking(true, playerMovement.transform);
                isTransitioning = true;
            }
        }
        //guards caught player, game over
        else
        {
            currMiniGame.isFailed = true;
            GameManager.Instance.Ui.EndGame();
            Debug.Log("DETECTED");
        }
    }

    public void EndMiniGame()
    {
        if (currMiniGame.isFinished || currMiniGame.isFailed)
        {
            //Check for bad result
            //GameManager.Instance.suspicousnessLevel = 50.0f;
            if (GameManager.Instance.suspicousnessLevel >= GameManager.Instance.scoreDangerous)
                disguise.StartDisguise();
            else
            {
                currMiniGameIdx++;
                playerMovement.SetWalking(true, playerMovement.transform);
                isTransitioning = true;
                //EndDisguise(false);
            }
        }
    }

    private void StartNextGame()
    {
        if (currMiniGameIdx < miniGames.Count)
        {
            SetupMiniGame(miniGames[currMiniGameIdx]);
        }
        else
        {
            Debug.Log("End of game");
            //TODO: End Game
        }
    }

    public void SetupMiniGame(GameID.GAME_ID minigame)
    {
        GameManager.Instance.CurrentGame = minigame;
        GameManager.Instance.GestureManager.LoadGameGestures(minigame);
        currMiniGame = miniGameObjects[currMiniGameIdx].GetComponent<MiniGame>();
        if (!minigame.Equals(GameID.GAME_ID.BALANCE_TABLET))
        {
            GameManager.Instance.Ui.UpdateFeedbackTextGesture(null, null, minigame, false);
        }
        GameManager.Instance.GestureManager.StartDetecting();
        currMiniGame.OnGameStarted();
    }

    //updates detection results for glasses
    public void GlassesDetected(DetectionResult glasses)
    {
        if (disguise.isDisguising)
        {
            disguise.AddResult(glasses);
        }
        if(takePhoto.timerStarted)
        {
            takePhoto.AddResult(glasses);
        }
        if (retakePhoto.timerStarted)
        {
            retakePhoto.AddResult(glasses);
        }
    }
}
