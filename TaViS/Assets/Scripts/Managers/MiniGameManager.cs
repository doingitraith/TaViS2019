using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
<<<<<<< HEAD
        gestureManager = GameObject.Find("GameManager").GetComponent<GestureManager>();
        gestureManager.LoadGameGestures(GameManager.currentGame);
=======
        
>>>>>>> parent of 50858ad... the real push from 31.5
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerGestureResult(GestureEvaluationResult gestureEvaluationResult)
    {
<<<<<<< HEAD
        //handle score, trigger event with name...
        int score = gestureEvaluationResult.score;
    }

    public void SetupMiniGame(GameID.GAME_ID minigame)
    {
        //do stuff
        gestureManager.LoadGameGestures(GameManager.currentGame);
    }

=======
        // do stuff
    }
>>>>>>> parent of 50858ad... the real push from 31.5
}
