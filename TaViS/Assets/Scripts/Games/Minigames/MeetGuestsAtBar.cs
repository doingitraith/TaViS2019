using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeetGuestsAtBar : MiniGame
{
    public override void OnGameFailed()
    {
        throw new System.NotImplementedException();
    }

    public override void OnGameFinished()
    {
        throw new System.NotImplementedException();
    }

    public override void OnGameStarted()
    {
        Id = GameID.GAME_ID.TIP_HAT_DRINK;
        GameName = Id.ToString();
    }

    public override void ResetGame()
    {
        throw new System.NotImplementedException();
    }
}
