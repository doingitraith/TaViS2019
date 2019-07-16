using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTrapdoor : MonoBehaviour
{
    public GameObject trapdoor;
    public GameObject target;
    public GameObject piano;
    public Transform endPos;
    public Transform pianoEndPos;
    bool buttonPressed;
    bool hasTargetFallen;
    float falling = 5f;
    // Start is called before the first frame update
    void Start()
    {
        hasTargetFallen = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Hand"))
        {
            trapdoor.SetActive(true);
            GetComponent<Collider>().enabled = false;
            StartCoroutine("Falling");
        }
        GetComponent<AudioSource>().Play();
    }

    IEnumerator Falling()
    {
        /*
        while (piano.transform.position.y >= pianoEndPos.position.y)
        {
            if (!hasTargetFallen)
            {
            */
                while (target.transform.position.y >= endPos.position.y)
                {
                    target.transform.position = Vector3.MoveTowards(target.transform.position, endPos.position, 2 * Time.deltaTime);
                    yield return new WaitForEndOfFrame();
                }
                hasTargetFallen = true;
        /*
            }
            piano.transform.position = Vector3.MoveTowards(piano.transform.position, pianoEndPos.position, 2 * Time.deltaTime);
        }
        */
        GameManager.Instance.MiniGameManager.TriggerGestureResult(
            new GestureEvaluationResult(GestureManager.GESTURENAME.END, GestureEvaluationResult.GESTURE_PERFORMANCE.NONE));
    }
}
