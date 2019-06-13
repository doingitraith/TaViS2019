using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
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

    private CameraUtils cameraUtils;
    private MiniGameManager miniGameManager;
    private GestureManager gestureManager;

    public GameID.GAME_ID CurrentGame { get; set; }
    public MiniGameManager MiniGameManager { get; private set; }
    public GestureManager GestureManager { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        CurrentGame = GameID.GAME_ID.START;
        cameraUtils = FindObjectOfType<CameraUtils>();
        miniGameManager = FindObjectOfType<MiniGameManager>();
        gestureManager = FindObjectOfType<GestureManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
