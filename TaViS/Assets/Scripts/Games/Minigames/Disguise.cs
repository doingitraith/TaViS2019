using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Windows.Kinect;

public class Disguise : MonoBehaviour
{
    private List<DetectionResult> glassesResults;
    public bool isDisguising;

    private int timerSeconds;
    private Text countdown;

    void Start()
    {
        isDisguising = false;
        timerSeconds = 10;
        glassesResults = new List<DetectionResult>();
        countdown = GameObject.Find("Countdown").GetComponent<Text>();
    }

    private IEnumerator StartTimer()
    {
        while(timerSeconds >= 0)
        {
            countdown.text = "Disguise yourself in\n" + timerSeconds;
            if (timerSeconds == 3)
                glassesResults.Clear();
            timerSeconds--;
            yield return new WaitForSeconds(1.0f);
        }
        timerSeconds = 5;
        EndDisguise();
    }

    private void Update()
    {
        
    }

    public void StartDisguise()
    {
        isDisguising = true;
        StartCoroutine(StartTimer());
    }

    public void AddResult(DetectionResult glasses)
    {
        glassesResults.Add(glasses);
    }

    public void EndDisguise()
    {
        isDisguising = false;
        countdown.text = "";
        bool isDisguised = false;

        if (glassesResults.Count > 0)
        {
            int yes = glassesResults.Count(d => d == DetectionResult.Yes);
            int no = glassesResults.Count(d => d == DetectionResult.No);
            int maybe = glassesResults.Count(d => d == DetectionResult.Maybe);
            int unknown = glassesResults.Count(d => d == DetectionResult.Unknown);

            int max = new List<int> { yes, no/*, maybe, unknown*/ }.Max();

            if (GameManager.Instance.isWearingGlasses)
            {
                if (max == no)
                    isDisguised = true;
            }
            else
            {
                if (max == yes)
                    isDisguised = true;
            }
        }
        GameManager.Instance.MiniGameManager.EndDisguise(isDisguised);
    }
}
