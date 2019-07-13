using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakePhoto : MonoBehaviour
{
    private Text countdown;
    private int timerSeconds;
    public bool isPhotoTaken;
    public bool isPhotoAccepted;
    public bool isPhotoSaved;

    // Start is called before the first frame update
    void Start()
    {
        isPhotoTaken = false;
        isPhotoTaken = false;
        isPhotoSaved = false;
        timerSeconds = 3;
        countdown = GameObject.Find("Countdown").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator StartTimer()
    {
        GetComponent<Collider>().enabled = false;
        while (timerSeconds >= 0)
        {
            countdown.text = "Smile!\n" + timerSeconds;
            timerSeconds--;
            yield return new WaitForSeconds(1.0f);
        }
        timerSeconds = 3;
        countdown.text = "";
        GetComponentInChildren<TMPro.TextMeshPro>().SetText("Accept");
        isPhotoSaved = true;
        isPhotoTaken = true;
        GetComponent<Collider>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Hand"))
        {
            if (!isPhotoTaken && !isPhotoSaved)
            {
                StartCoroutine(StartTimer());
            }
            if(isPhotoSaved)
            {
                isPhotoTaken = false;
                isPhotoAccepted = true;
            }
        }
    }
}
