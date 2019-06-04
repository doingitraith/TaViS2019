using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    private MiniGame currMiniGame;

    public Dictionary<GameID.GAME_ID, MiniGame> miniGames;
    
    // Start is called before the first frame update
    void Start()
    {
        gestureManager = GameObject.Find("GameManager").GetComponent<GestureManager>();
        gestureManager.LoadGameGestures(GameManager.currentGame);
        InitMiniGames();
        currMiniGame = miniGames[GameManager.currentGame];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitMiniGames()
    {
        miniGames = new Dictionary<GameID.GAME_ID, MiniGame>();
        // TODO: Add Mini Game classes
        // miniGames.Add(GameID.GAME_ID.DANCE, FindObjectOfType<DanceMiniGame>());
    }

    public void TriggerGestureResult(GestureEvaluationResult gestureEvaluationResult)
    {
        //handle score, trigger event with name...
        int score = gestureEvaluationResult.score;
    }

    public void SetupMiniGame(GameID.GAME_ID minigame)
    {
        //do stuff
        gestureManager.LoadGameGestures(GameManager.currentGame);
        currMiniGame = GameManager.currentGame;
    }
        // do stuff
    }
}
