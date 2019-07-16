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
    public float walkingSpeed = 1.3f;
    private bool walking = false;
    private bool balanceReached = false;
    private bool danceReached = false;
    private bool barReached = false;
    BasicAvatarController bm;
    float time;
    Quaternion lookRotation;
    bool nodeReached = false;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        bm = GameObject.FindGameObjectWithTag("Player").GetComponent<BasicAvatarController>();
        walking = bm.GetWalking();
        currentNode = path[0];
        lookRotation = new Quaternion();
    }

    void NextNode()
    {
        time = 0;
        currentNode = path[currentNodeIndex];

        Debug.Log("Node: " + currentNode);
        lookRotation = Quaternion.LookRotation(currentNode.position - transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector3.zero;
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
                transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * walkingSpeed *2);
            }
            else
            {
                if(currentNodeIndex < path.Length - 1)
                {
                    nodeReached = false;
                    if(currentNodeIndex == 1)
                    {
                        transform.position = Vector3.Lerp(transform.position, currentNode.position, 1);
                        transform.rotation = Quaternion.Slerp(transform.rotation, currentNode.rotation, 1);
                        barReached = true;
                        SetWalking(false, transform);
                        GameManager.Instance.CameraUtils.GoToFrontOrBackView(CameraUtils.FACING.FRONT);
                    }
                    if (currentNodeIndex == 5)
                    {
                        transform.rotation = Quaternion.Slerp(transform.rotation, currentNode.rotation, 1);
                        transform.rotation = Quaternion.Slerp(transform.rotation, currentNode.rotation, 1);
                        danceReached = true;
                        SetWalking(false, transform);
                        GameManager.Instance.CameraUtils.GoToFrontOrBackView(CameraUtils.FACING.FRONT);
                    }
                    if (currentNodeIndex == 7)
                    {
                        transform.rotation = Quaternion.Slerp(transform.rotation, currentNode.rotation, 1);
                        transform.rotation = Quaternion.Slerp(transform.rotation, currentNode.rotation, 1);
                        balanceReached = true;
                        SetWalking(false, transform);
                        GameManager.Instance.CameraUtils.GoToFrontOrBackView(CameraUtils.FACING.FRONT);
                    }
                    currentNodeIndex++;
                    NextNode();
                }
                else
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, currentNode.rotation, 1);
                    SetWalking(false, transform);
                    rb.velocity = Vector3.zero;
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

    public bool GetWalking()
    {
        return walking;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Path")
        {
            other.gameObject.SetActive(false);
            nodeReached = true;
            danceReached = false;
            barReached = false;
            balanceReached = false;
        }
    }
}