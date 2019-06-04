using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameID.GAME_ID currentGame;
    // Start is called before the first frame update
    void Start()
    {
        currentGame = GameID.GAME_ID.START;
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
