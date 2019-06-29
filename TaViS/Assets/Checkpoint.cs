using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public string bodyPartTag;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == bodyPartTag)
        {
            gameObject.SetActive(false);
            IncreaseScore();
        }
    }

    private void IncreaseScore()
    {

    }
}
