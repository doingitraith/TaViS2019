using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;





public class BallMovement : MonoBehaviour
{
    Rigidbody ball;

    public float movementSpeedModifier = 0.5f;

    //Vector3 handLeft = skeleton.getRawWorldPosition(JointType.HandLeft);
    //Vector3 handRight = skeleton.getRawWorldPosition(JointType.HandRight);

    // Start is called before the first frame update
    void Start()
    {
        ball = GetComponent<Rigidbody>();

        //initial velocity, können es random machen, sodass der Spieler balancieren muss
        float x = Random.Range(0.0f, 0.5f);
        float y = Random.Range(0.0f, 0.5f);
        float z = Random.Range(0.0f, 0.5f);

        ball.velocity = new Vector3(x, y, z);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void FixedUpdate()
    {
       
    }
}
