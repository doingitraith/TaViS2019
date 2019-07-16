using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Windows.Kinect;

public class RetakePhoto : MonoBehaviour
{
    private Text countdown;
    private int timerSeconds;
    private UnityEngine.AudioSource audioSource;
    public bool isPhotoTaken;
    public bool isPhotoAccepted;
    public bool isPhotoSaved;
    public TakePhoto photo;
    public AudioClip buttonPressed;
    public AudioClip photoTaken;

    public bool timerStarted;
    List<DetectionResult> glassesResults;

    // Start is called before the first frame update
    void Start()
    {
        timerStarted = false;
        isPhotoTaken = false;
        isPhotoTaken = false;
        timerSeconds = 3;
        countdown = GameObject.Find("Countdown").GetComponent<Text>();
        audioSource = GetComponent<UnityEngine.AudioSource>();
        glassesResults = new List<DetectionResult>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator StartTimer()
    {
        photo.gameObject.SetActive(false);
        GetComponent<Collider>().enabled = false;
        timerStarted = true;
        while (timerSeconds >= 0)
        {
            countdown.text = "Smile again!\n" + timerSeconds;
            timerSeconds--;
            yield return new WaitForSeconds(1.0f);
        }
        CheckForGlasses();

        timerSeconds = 3;
        countdown.text = "";
        audioSource.clip = photoTaken;
        audioSource.Play();
        isPhotoTaken = true;
        GetComponent<Collider>().enabled = true;
        photo.gameObject.SetActive(true);
        timerStarted = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Hand"))
        {
            audioSource.clip = buttonPressed;
            audioSource.Play();
            StartCoroutine(StartTimer());
        }
    }

    public void AddResult(DetectionResult glasses)
    {
        glassesResults.Add(glasses);
    }

    public void CheckForGlasses()
    {
        if (glassesResults.Count > 0)
        {
            int yes = glassesResults.Count(d => d == DetectionResult.Yes);
            int no = glassesResults.Count(d => d == DetectionResult.No);
            int maybe = glassesResults.Count(d => d == DetectionResult.Maybe);
            int unknown = glassesResults.Count(d => d == DetectionResult.Unknown);

            int max = new List<int> { yes, no/*, maybe, unknown*/ }.Max();


            if (max == no)
                GameManager.Instance.isWearingGlasses = false;

            if (max == yes)
                GameManager.Instance.isWearingGlasses = true;
            GameManager.Instance.Ui.UpdateFeedbackTextGesture(null, null, null, true);
            glassesResults.Clear();
        }
    }
}
