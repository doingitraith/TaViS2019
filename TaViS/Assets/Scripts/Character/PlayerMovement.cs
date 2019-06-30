using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform[] path;
    public float tolerance;
    int currentNodeIndex = 0;
    Transform currentNode;
    Transform playerTransform;
    public float walkingSpeed = 2f;
    private bool walking = false;
    BasicAvatarController bm;
    float time;
    Quaternion lookRotation;
    bool nodeReached = false;

    // Start is called before the first frame update
    void Start()
    {
        bm = GameObject.FindGameObjectWithTag("Player").GetComponent<BasicAvatarController>();
        walking = bm.GetWalking();
        currentNode = path[0];
    }

    void NextNode()
    {
        time = 0;
        currentNode = path[currentNodeIndex];
        lookRotation = Quaternion.LookRotation(currentNode.position - transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            SetWalking(true, transform);
        }
        time += Time.deltaTime * walkingSpeed;
        if (walking)
        {
            if (!nodeReached)
            {
                transform.position = Vector3.Lerp(playerTransform.position, currentNode.position, Time.deltaTime * walkingSpeed);
            
                lookRotation = Quaternion.LookRotation(currentNode.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * walkingSpeed);
            }
            else
            {
                if(currentNodeIndex < path.Length - 1)
                {
                    nodeReached = false;
                    currentNodeIndex++;
                    NextNode();
                }
                else
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, currentNode.rotation, 1);
                    SetWalking(false, transform);
                    gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    GameManager.Instance.CameraUtils.GoToFrontOrBackView(CameraUtils.FACING.FRONT);
                }
            }
        }
    }

    public void SetWalking(bool walking, Transform playerTransform)
    {
        if (walking)
        {
            GameManager.Instance.CameraUtils.GoToFrontOrBackView(CameraUtils.FACING.BACK);
        }
        this.playerTransform = playerTransform;
        bm.SetWalking(walking);
        this.walking = walking;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Path")
        {
            Debug.Log("Collision");
            other.gameObject.SetActive(false);
            nodeReached = true;
        }
    }
}
