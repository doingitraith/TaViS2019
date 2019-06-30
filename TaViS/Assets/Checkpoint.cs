using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public string bodyPartTag = "Hand";
    public string id;
    CheckpointManager cm;

    private void Start()
    {
        cm = GameObject.Find("CheckpointManager").GetComponent<CheckpointManager>();
        id = gameObject.name;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == bodyPartTag)
        {
            if(name == "0")
            {
                cm.SetFirst(other.gameObject);
                cm.IncreaseScore();
                gameObject.SetActive(false);
            }
            // must be the exact same bodypart e.g left hand not right hand from start to finish
            else if (other.gameObject.Equals(cm.GetActor()))
            {
                switch (name)
                {
                    case "1": cm.SetMiddle(); break;
                    case "2": cm.SetLast(); break;
                }
                cm.IncreaseScore();
                gameObject.SetActive(false);
            }
        }
    }
}