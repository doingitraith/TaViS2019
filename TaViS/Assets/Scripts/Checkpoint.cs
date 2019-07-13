﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public string bodyPartTag = "Hand";
    private string id;
    CheckpointManager cm;
    public Transform target;

    private void Start()
    {
        cm = GameObject.Find("CheckpointManager").GetComponent<CheckpointManager>();
        id = gameObject.name;
    }

    private void Update()
    {
        if(target != null)
        {
            transform.position = target.position;
            transform.parent = target;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(gameObject.transform.parent.name + gameObject.name + " Collision with: " + other.name);
        if(other.tag == bodyPartTag)
        {
            if(name == "0")
            {
                Debug.Log("Checkpoint 0");
                cm.SetFirst(other.gameObject);
                cm.IncreaseScore();
                gameObject.SetActive(false);
                cm.RestartTimer();
                if(GameManager.Instance.MiniGameManager.currMiniGame.currentGesture == GestureManager.GESTURENAME.TIP_HAT)
                {
                    if (other.name.Equals("hand_interact_r") )
                    {
                        foreach (Checkpoint checkpoint in cm.checkpoints)
                        {
                            if (!checkpoint.id.Equals("0"))
                            {
                                checkpoint.transform.position = new Vector3(-checkpoint.transform.position.x,
                                    checkpoint.transform.position.y,
                                    checkpoint.transform.position.z);
                            }
                        }
                    }
                }
            }
            // must be the exact same bodypart e.g left hand not right hand from start to finish
            else if (cm != null && cm.GetActor() != null && other.gameObject.Equals(cm.GetActor()))
            {
                if (cm.GetFirst())
                {
                    switch (name)
                    {
                        case "1":   cm.SetMiddle();
                                    cm.IncreaseScore();
                                    gameObject.SetActive(false);
                                    break;
                        case "2":   cm.SetLast();
                                    cm.IncreaseScore();
                                    gameObject.SetActive(false);
                                    break;
                    }
                    Debug.Log("Checkpoint " + id);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (cm.GetFirst())
        {
            if (id.Equals("INVALID"))
            {
                Debug.Log("Actor: " + cm.GetActor());
                if (other.name == cm.GetActor().name)
                {
                    Debug.Log("Invalid Penalty");
                    cm.SetInvalid();
                    gameObject.SetActive(false);
                }
            }
        }
    }
}