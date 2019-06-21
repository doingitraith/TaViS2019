using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class UnityChanController : BasicAvatarController
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
        //HandTipLeft = GameObject.Find("Character1_LeftHandIndex1").transform;
        //ThumbLeft = GameObject.Find("Character1_LeftHandThumb1").transform;
        //HandTipRight = GameObject.Find("Character1_RightHandIndex1").transform;
        //ThumbRight = GameObject.Find("Character1_RightHandThumb1").transform;

        base.Start();
    }

    public virtual void Update()
    {
        // apply base Update function, which rotates all known standard joints
        base.Update();
    }




}
