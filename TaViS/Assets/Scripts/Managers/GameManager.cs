using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Central of the game, stores all managers and is always accessible
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
    public GameObject eric;

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
        DontDestroyOnLoad(this.gameObject);
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
        suspicousnessLevel = Mathf.Clamp(suspicousnessLevel, 0, scoreDangerous);
    }

    //Ends the game
    public void RestartMission()
    {
        Application.Quit();
        //Nice to have: restart the game
        //SceneManager.LoadScene(0);
    }
}
