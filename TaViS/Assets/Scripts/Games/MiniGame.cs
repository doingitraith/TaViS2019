using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MiniGame
{
    GameID.GAME_ID Id;
    public string GameName;
    public Transform CameraStartPosition;
    public Quaternion CameraStartRotation;

    public abstract void OnGameStarted();
    public abstract void OnGameFinished();
    public abstract void OnGameFailed();
    public abstract void ResetGame();
}
