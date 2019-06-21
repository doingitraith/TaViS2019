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
        GameManager.Instance.GestureManager.LoadGameGestures(GameManager.Instance.CurrentGame);
        InitMiniGames();
        currMiniGame = miniGames[GameManager.Instance.CurrentGame];
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InitMiniGames()
    {
        miniGames = new Dictionary<GameID.GAME_ID, MiniGame>();
        // TODO: Add Mini Game classes
        miniGames.Add(GameID.GAME_ID.START, new StartGame());
        miniGames.Add(GameID.GAME_ID.TIP_HAT_DRINK, new MeetGuestsAtBar());
        // miniGames.Add(GameID.GAME_ID.DANCE, FindObjectOfType<DanceMiniGame>());
    }

    public void TriggerGestureResult(GestureEvaluationResult result)
    {
        //handle score, trigger event with name...
        int score = result.score;
    }

    public void SetupMiniGame(GameID.GAME_ID minigame)
    {
        //do stuff
        GameManager.Instance.GestureManager.LoadGameGestures(GameManager.Instance.CurrentGame);
        currMiniGame = miniGames[GameManager.Instance.CurrentGame];
    }
    // do stuff
}
