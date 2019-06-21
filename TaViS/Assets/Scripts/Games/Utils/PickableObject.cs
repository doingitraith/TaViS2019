using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickableObject : MonoBehaviour
{
    //head, tablet, etc
    protected GameObject objectToMoveTo;
    protected Transform targetOnObjectToMoveTo;
    protected bool releaseObject = false;
    protected bool followPlayer = false;
    public float xOffset = 0.05f;
    public float yOffset = 0.05f;
    public float zOffset = 0.05f;
    public float autoMoveSpeed = 2;
    public float autoRotationSpeed = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (releaseObject)
        {
            OnTargetObjectReached();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!followPlayer)
        {
            if (collision.gameObject.CompareTag("Hand"))
            {
                transform.SetParent(collision.transform);
                transform.position = new Vector3(collision.transform.position.x + xOffset,
                                                 collision.transform.position.y + yOffset,
                                                 collision.transform.position.z + zOffset);
                followPlayer = true;
            }
        }
        else
        {
            if (collision.gameObject.Equals(objectToMoveTo))
            {
                followPlayer = false;
                transform.SetParent(null);
                releaseObject = true;
            }
        }
    }

    protected void OnTargetObjectReached()
    { 
        if (transform.position != targetOnObjectToMoveTo.position)
        {
            transform.position = Vector3.Lerp(transform.position, targetOnObjectToMoveTo.position, Time.deltaTime * autoMoveSpeed);
        }

        if (transform.rotation != targetOnObjectToMoveTo.rotation)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetOnObjectToMoveTo.transform.rotation, Time.deltaTime * autoRotationSpeed);
        }

        if(transform.position == targetOnObjectToMoveTo.position && transform.rotation == targetOnObjectToMoveTo.rotation)
        {
            releaseObject = false;
        }
    }
}
