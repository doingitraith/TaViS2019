using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

public class TrayControl : MonoBehaviour
{
    GameObject pointMan;
    Rigidbody rb;

    //Vector3 handLeft;
    //Vector3 handRight;

    bool hasGrabbed = false;
    bool moveable = false;

    public float rotation = 0;


    /*
    public override void OnGameFailed()
    {
        throw new System.NotImplementedException();
    }

    public override void OnGameFinished()
    {
        throw new System.NotImplementedException();
    }

    public override void OnGameStarted()
    {
        pointMan = GameObject.Find("KinectModelRel");

        Vector3 handLeft =pointMan.GetComponent<KinectPointManAvatarModel>().getRawWorldPosition(JointType.HandLeft);
        Vector3 handRight = pointMan.GetComponent<KinectPointManAvatarModel>().getRawWorldPosition(JointType.HandRight);
    }

    public override void ResetGame()
    {
        throw new System.NotImplementedException();
    }

    */

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
           
    }

    // Update is called once per frame
    void Update()
    {
        if () { }
        transform.Rotate(0, 0, rotation * Time.deltaTime);
        
    }

    public void AdjustRotation(float newRotation)
    {
        rotation = newRotation;    
    }

}
