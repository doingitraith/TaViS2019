using UnityEngine;
using System.Collections;
using Windows.Kinect;

//setup kinect input for player model
public class UnityEricController : BasicAvatarController
{
    public override void Start()
    {
        // find transforms of model
        SpineBase = GameObject.Find("hip").transform;
        SpineMid = GameObject.Find("spine_02").transform;
        Neck = GameObject.Find("neck").transform;
        Head = GameObject.Find("head").transform;
        ShoulderLeft = GameObject.Find("upperarm_l").transform;
        ElbowLeft = GameObject.Find("lowerarm_l").transform;
        WristLeft = GameObject.Find("hand_l").transform;
        HandLeft = GameObject.Find("hand_l").transform;
        ShoulderRight = GameObject.Find("upperarm_r").transform;
        ElbowRight = GameObject.Find("lowerarm_r").transform;
        WristRight = GameObject.Find("hand_r").transform;
        HandRight = GameObject.Find("hand_r").transform;
        HipLeft = GameObject.Find("upperleg_l").transform;
        KneeLeft = GameObject.Find("lowerleg_l").transform;
        AnkleLeft = GameObject.Find("foot_l").transform;
        FootLeft = GameObject.Find("ball_l").transform;
        HipRight = GameObject.Find("upperleg_r").transform;
        KneeRight = GameObject.Find("lowerleg_r").transform;
        AnkleRight = GameObject.Find("foot_r").transform;
        FootRight = GameObject.Find("ball_r").transform;
        SpineShoulder = GameObject.Find("spine_03").transform;
        //HandTipLeft = GameObject.Find("middle_03_l").transform;
        ThumbLeft = GameObject.Find("thumb_03_l").transform;
        //HandTipRight = GameObject.Find("middle_03_r").transform;
        ThumbRight = GameObject.Find("thumb_03_r").transform;

        base.Start();
    }

    public virtual void Update()
    {
        // apply base Update function, which rotates all known standard joints
        base.Update();
    }

    public Transform GetHandLeft()
    {
        return HandLeft;
    }

    public Transform GetHandRight()
    {
        return HandRight;
    }
}
