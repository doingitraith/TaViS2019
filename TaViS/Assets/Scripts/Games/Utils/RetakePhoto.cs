using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetakePhoto : MonoBehaviour
{
    private Text countdown;
    private int timerSeconds;
    public bool isPhotoTaken;
    public bool isPhotoAccepted;
    public bool isPhotoSaved;
    public TakePhoto photo;

    // Start is called before the first frame update
    void Start()
    {
        isPhotoTaken = false;
        isPhotoTaken = false;
        timerSeconds = 3;
        countdown = GameObject.Find("Countdown").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator StartTimer()
    {
        photo.gameObject.SetActive(false);
        GetComponent<Collider>().enabled = false;
        while (timerSeconds >= 0)
        {
            countdown.text = "Smile again!\n" + timerSeconds;
            timerSeconds--;
            yield return new WaitForSeconds(1.0f);
        }
        timerSeconds = 3;
        countdown.text = "";
        GetComponent<AudioSource>().Play();
        isPhotoTaken = true;
        GetComponent<Collider>().enabled = true;
        photo.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Hand"))
        {
            StartCoroutine(StartTimer());
        }
    }
}
