using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Balance : MiniGame
{
    GameObject pointMan;
    public Canvas balanceUI;
    public GameObject apples;
    Hands hands;
  
    bool hasGrabbed = false;
    bool moveable = false;
    bool isEnded = false;

    float trayRotation = 0.0f;

    public AppleCounter appleCounter;
    private int timerSeconds;
    private Text countdown;
    private Coroutine timer;

    public override void OnGameFailed()
    {
        base.OnGameFailed();
        hands.isBalance = false;
        balanceUI.gameObject.SetActive(false);
    }

    public override void OnGameFinished()
    {
        base.OnGameFinished();
        hands.isBalance = false;
        balanceUI.gameObject.SetActive(false);
    }

    public override void OnGameStarted()
    {
        base.OnGameStarted();
        pointMan = GameObject.Find("KinectModelRel");
        isEnded = true;
        balanceUI.gameObject.SetActive(true);
        hands = balanceUI.gameObject.GetComponent<Hands>();
        hands.isBalance = true;
        apples.SetActive(true);

        Vector3 handLeft = pointMan.GetComponent<KinectPointManAvatarModel>().getRawWorldPosition(Windows.Kinect.JointType.HandLeft);
        Vector3 handRight = pointMan.GetComponent<KinectPointManAvatarModel>().getRawWorldPosition(Windows.Kinect.JointType.HandRight);
        timer = StartCoroutine(StartTimer());
    }

    public override void ResetGame()
    {
        throw new System.NotImplementedException();
    }

    private IEnumerator StartTimer()
    {
        while (timerSeconds >= 0)
        {
            countdown.text = "" + timerSeconds;
            timerSeconds--;
            yield return new WaitForSeconds(1.0f);
        }
        countdown.text = "";
        isEnded = false;
        EvaluateApples();
    }

    private void EvaluateApples()
    {
        int remainingApples = appleCounter.getAppleCount();
        GestureEvaluationResult result = new GestureEvaluationResult(GestureManager.GESTURENAME.BALANCE, GestureEvaluationResult.GESTURE_PERFORMANCE.BAD);
        result.score = (int)GestureEvaluationResult.GESTURE_PERFORMANCE.BAD;

        switch (remainingApples)
        {
            case 1:
                result = new GestureEvaluationResult(GestureManager.GESTURENAME.BALANCE, GestureEvaluationResult.GESTURE_PERFORMANCE.OK);
                result.score = (int)GestureEvaluationResult.GESTURE_PERFORMANCE.OK;
                break;
            case 2:
                result = new GestureEvaluationResult(GestureManager.GESTURENAME.BALANCE, GestureEvaluationResult.GESTURE_PERFORMANCE.GOOD);
                result.score = (int)GestureEvaluationResult.GESTURE_PERFORMANCE.GOOD;
                break;
            case 3:
                result = new GestureEvaluationResult(GestureManager.GESTURENAME.BALANCE, GestureEvaluationResult.GESTURE_PERFORMANCE.VERY_GOOD);
                result.score = (int)GestureEvaluationResult.GESTURE_PERFORMANCE.VERY_GOOD;
                break;
            case 4:
                result = new GestureEvaluationResult(GestureManager.GESTURENAME.BALANCE, GestureEvaluationResult.GESTURE_PERFORMANCE.PERFECT);
                result.score = (int)GestureEvaluationResult.GESTURE_PERFORMANCE.PERFECT;
                break;
            default:
                break;
        }
        GameManager.Instance.MiniGameManager.TriggerGestureResult(result);
    }

    // Start is called before the first frame update
    void Start()
    {
        Id = GameID.GAME_ID.BALANCE_TABLET;
        countdown = GameObject.Find("Countdown").GetComponent<Text>();
        timerSeconds = 20;
    }

    // Update is called once per frame
    void Update()  
    {
        if (isEnded && appleCounter.getAppleCount() == 0)
        {
            isEnded = false;
            EvaluateApples();
            StopCoroutine(timer);
            countdown.text = "";
        }
        transform.Rotate(0, 0, trayRotation * Time.deltaTime);
        
    }

    public void AdjustRotation(float newRotation)
    {
        trayRotation = newRotation;
    }

    public void AdjustLeftRotation(float newRotation)
    {
        trayRotation += newRotation;
    }

    public void AdjustRightRotation(float newRotation)
    {
        trayRotation -= newRotation;
    }

}
