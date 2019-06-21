using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MiniGame
{
    public GameID.GAME_ID Id;
    public string GameName;
    public Transform StartPosition;
    public Quaternion CameraStartRotation;

    public abstract void OnGameStarted();
    public abstract void OnGameFinished();
    public abstract void OnGameFailed();
    public abstract void ResetGame();
}
