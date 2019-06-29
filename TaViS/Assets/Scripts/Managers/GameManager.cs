using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public CameraUtils CameraUtils;
    public MiniGameManager MiniGameManager;
    public GestureManager GestureManager;
    public GameID.GAME_ID CurrentGame { get; set; }
    public bool isRightHanded = false; // ;)
    public float suspicousnessLevel = 10;
    public float score = 0.0f;

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
        CurrentGame = GameID.GAME_ID.TIP_HAT_DRINK;
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
