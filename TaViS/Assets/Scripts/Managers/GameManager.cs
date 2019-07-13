using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public CameraUtils CameraUtils;
    public MiniGameManager MiniGameManager;
    public GestureManager GestureManager;
    public CheckpointManager CheckpointManager;
    public UI Ui;
    public GameID.GAME_ID CurrentGame { get; set; }
    public bool isRightHanded = false; // ;)
    public bool isWearingGlasses = false;
    public float suspicousnessLevel = 0;
    public float score = 0.0f;
    public int scoreDangerous = 20;
    public int scoreSuspicous = 10;
    public int scoreUnaware = 0;
    public bool playerCarriesObject = false;
    public Texture2D colorTexture;
    public Texture2D takenPicure;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        CurrentGame = GameID.GAME_ID.START;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeScore(float score)
    {
        this.score += score;
    }

    public void ChangeSuspicousness(float suspicousness)
    {
        this.suspicousnessLevel += suspicousness;
    }
}
