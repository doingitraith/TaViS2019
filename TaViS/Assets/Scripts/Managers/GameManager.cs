using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameID.GAME_ID currentGame;
    private GestureManager gestureManager;
    // Start is called before the first frame update
    void Start()
    {
        currentGame = GameID.GAME_ID.START;

        gestureManager = GameObject.Find("GameManager").GetComponent<GestureManager>();
        gestureManager.UpdateGestures(currentGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameID.GAME_ID GetGAME_ID()
    {
        return currentGame;
    }
}
