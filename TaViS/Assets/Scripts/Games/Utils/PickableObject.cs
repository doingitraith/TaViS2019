using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickableObject : MonoBehaviour
{
    //head, tablet, etc
    public GameObject objectToMoveTo;
    public Transform targetOnObjectToMoveTo;
    protected bool releaseObject = false;
    protected bool followPlayer = false;
    protected bool parentToTarget = false;
    public float xOffset = 0.05f;
    public float yOffset = 0.05f;
    public float zOffset = 0.05f;
    float timeElapsed = .0f;
    public float autoMoveSpeed = 2;
    public float autoRotationSpeed = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected void Update()
    {
        if (releaseObject)
        {
            OnTargetObjectReached();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!GameManager.Instance.playerCarriesObject)
        {
            if (!followPlayer)
            {
                if (other.gameObject.CompareTag("Hand"))
                {
                    //Debug.Log("Collision " + other.name);
                    transform.SetParent(other.transform);
                    transform.position = other.transform.position; //new Vector3(other.transform.position.x + xOffset, other.transform.position.y + yOffset, other.transform.position.z + zOffset);
                    followPlayer = true;
                    GameManager.Instance.playerCarriesObject = true;
                    StartCoroutine(DisableCollider(gameObject.GetComponent<Collider>()));
                }
            }
            else
            {
                if (other.gameObject.Equals(objectToMoveTo))
                {
                    followPlayer = false;
                    if (parentToTarget)
                    {
                        transform.SetParent(targetOnObjectToMoveTo.parent);
                    }
                    else
                    {
                        transform.SetParent(null);
                    }
                    releaseObject = true;
                    GameManager.Instance.playerCarriesObject = false;
                }
            }
        }
    }

    protected void OnTargetObjectReached()
    {
        float t = timeElapsed / autoMoveSpeed;
        if (transform.position != targetOnObjectToMoveTo.position)
        {
            transform.position = Vector3.Lerp(transform.position, targetOnObjectToMoveTo.position, t);
        }

        t = timeElapsed / autoRotationSpeed;
        if (transform.rotation != targetOnObjectToMoveTo.rotation)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetOnObjectToMoveTo.transform.rotation, t);
        }

        if(transform.position == targetOnObjectToMoveTo.position && transform.rotation == targetOnObjectToMoveTo.rotation)
        {
            releaseObject = false;
        }
      
        timeElapsed += Time.deltaTime;
    }

    IEnumerator DisableCollider(Collider other)
    {
        other.gameObject.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        other.gameObject.GetComponent<Collider>().enabled = true;
    }
}
