using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    private int currMiniGameIdx=1;

    public MiniGame currMiniGame;
    public List<GameID.GAME_ID> miniGames;
    public List<GameObject> miniGameObjects;

    // Start is called before the first frame update
    void Start()
    {
        //InitMiniGames();
        SetupMiniGame(GameManager.Instance.CurrentGame);
    }

    // Update is called once per frame
    void Update()
    {

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

    public void TriggerGestureResult(GestureEvaluationResult result)
    {
        //handle score, trigger event with name...
        GameManager.Instance.GestureManager.StopDetecting();
        GameManager.Instance.ChangeScore(result.score);

        if (result.performance == GestureEvaluationResult.GESTURE_PERFORMANCE.BAD ||
            result.performance == GestureEvaluationResult.GESTURE_PERFORMANCE.INVALID)
            GameManager.Instance.ChangeSuspicousness(2.0f);

        currMiniGame.OnGameNextStep();

        if (currMiniGame.isFinished || currMiniGame.isFailed)
        {
            currMiniGameIdx++;
            if (currMiniGameIdx < miniGames.Count)
            {
                // TODO: Do game transition
                SetupMiniGame(miniGames[currMiniGameIdx]);
            }
        }

        // TODO: Present result
        GameManager.Instance.GestureManager.StartDetecting();
    }

    public void SetupMiniGame(GameID.GAME_ID minigame)
    {
        //do stuff
        GameManager.Instance.CurrentGame = minigame;
        GameManager.Instance.GestureManager.LoadGameGestures(minigame);
        currMiniGame = miniGameObjects[currMiniGameIdx].GetComponent<MiniGame>();
        currMiniGame.OnGameStarted();
        GameManager.Instance.GestureManager.StartDetecting();
    }
}
