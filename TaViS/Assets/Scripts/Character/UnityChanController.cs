using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class UnityChanController : BasicAvatarController
{	
	public override void Start(){
        // find transforms of model
        SpineBase = GameObject.Find("Character1_Hips").transform;
        //SpineMid = GameObject.Find("Character1_Spine2").transform;
        Neck = GameObject.Find("Character1_Neck").transform;
        Head = GameObject.Find("Character1_Head").transform;
        ShoulderLeft = GameObject.Find("Character1_LeftArm").transform;
        ElbowLeft = GameObject.Find("Character1_LeftForeArm").transform;
        WristLeft = GameObject.Find("Character1_LeftHand").transform;
        //HandLeft = GameObject.Find("").transform;
        ShoulderRight = GameObject.Find("Character1_RightArm").transform;
        ElbowRight = GameObject.Find("Character1_RightForeArm").transform;
        WristRight = GameObject.Find("Character1_RightHand").transform;
        //HandRight = GameObject.Find("").transform;
        HipLeft = GameObject.Find("Character1_LeftUpLeg").transform;
        KneeLeft = GameObject.Find("Character1_LeftLeg").transform;
        AnkleLeft = GameObject.Find("Character1_LeftFoot").transform;
        FootLeft = GameObject.Find("Character1_LeftToeBase").transform;
        HipRight = GameObject.Find("Character1_RightUpLeg").transform;
        KneeRight = GameObject.Find("Character1_RightLeg").transform;
        AnkleRight = GameObject.Find("Character1_RightFoot").transform;
        FootRight = GameObject.Find("Character1_RightToeBase").transform;
        SpineShoulder = GameObject.Find("Character1_Spine2").transform;
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
