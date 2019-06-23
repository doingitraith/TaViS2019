using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    Text suspicousness;
    public int scoreDangerous = 10;
    public int scoreSuspicous = 20;
    public int scoreUnaware = 30;

    private void Start()
    {
        suspicousness = gameObject.GetComponent<Text>();
    }

    private void FixedUpdate()
    {
        UpdateSuspiciousness();
    }
    public void UpdateSuspiciousness()
    {
        float score = GameManager.Instance.suspicousnessLevel;

        if (score > scoreSuspicous)
        {
            suspicousness.text = "The Guards suspect nothing";
        }
        else if(score > scoreDangerous)
        {
            suspicousness.text = "The Guards are suspicous";
        }
        else
        {
            suspicousness.text = "The Guards are onto you!";
        }
    }
}
