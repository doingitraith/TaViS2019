using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dance : MiniGame
{
    public Canvas DanceUI;

    private bool isDanceBarMoving;
    private float velocity = .7f;
    GameObject moveImages;
    public List<Sprite> moveSprites;
    private Text[] texts;
    private Image[] images;

    private void Start()
    {
        Id = GameID.GAME_ID.DANCE;
        isDanceBarMoving = false;
    }

    private void Update()
    {
        if (isDanceBarMoving)
        {
            StartCoroutine(MoveDanceBar());
            isDanceBarMoving = false;
        }
    }

    public override void OnGameStarted()
    {
        base.OnGameStarted();

        DanceUI.gameObject.SetActive(true);
        Debug.Log("Game started: " + GameName);
        Debug.Log("Current gesture: " + currentGesture);
        moveImages = GameObject.Find("MoveImages").gameObject;
        
        texts = moveImages.GetComponentsInChildren<Text>();
        images = moveImages.GetComponentsInChildren<Image>();

        for (int i = 0; i < GestureNames.Count; i++)
        {
            texts[texts.Length-1-i].text = GestureNames[i].ToString();
            images[images.Length-1-i].sprite = moveSprites[i];
        }

        // Randomize MoveList
        /*
        ShuffleArray();

        Text[] newTexts = moveImages.GetComponentsInChildren<Text>();
        Image[] newImages = moveImages.GetComponentsInChildren<Image>();

        for (int i = 0; i < GestureNames.Count; i++)
        {
            newTexts[texts.Length - 1 - i].text = texts[i].text;
            newImages[images.Length - 1 - i].sprite = images[i].sprite;
        }
        */
        currentGesture = GestureNames[currentStep];
        isDanceBarMoving = true;
        GameManager.Instance.GestureManager.StopDetecting();
    }

    private void ShuffleArray()
    {
        for (int i = 0; i < texts.Length; i++)
        {
            int rnd = Random.Range(0, texts.Length);

            //Switch Gestures
            GestureManager.GESTURENAME tempGesture = GestureNames[rnd];
            GestureNames[rnd] = GestureNames[i];
            GestureNames[i] = tempGesture;

            //Switch Texts
            Text tempText = texts[rnd];
            texts[rnd] = texts[i];
            texts[i] = tempText;

            //Switch Images
            Image tempImage = images[rnd];
            images[rnd] = images[i];
            images[i] = tempImage;
        }
    }

    public override void OnGameNextStep()
    {
        base.OnGameNextStep();
    }

    public override void OnGameFinished()
    {
        base.OnGameFinished();
        DanceUI.gameObject.SetActive(false);
        Debug.Log("Game finished: " + GameName);
        GameManager.Instance.MiniGameManager.EndMiniGame();
    }

    public override void OnGameFailed()
    {
        base.OnGameFailed();
    }

    public override void ResetGame()
    {
        DanceUI.gameObject.SetActive(false);
        isDanceBarMoving = false;
    }

    private IEnumerator MoveDanceBar()
    {
        while (moveImages.transform.localPosition.x <= 1800)
        {
            moveImages.transform.Translate(Vector3.right*velocity);
            yield return null;
        }
    }
}
