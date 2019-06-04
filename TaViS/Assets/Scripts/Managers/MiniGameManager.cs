using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    private GestureManager gestureManager;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
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
    }
}
