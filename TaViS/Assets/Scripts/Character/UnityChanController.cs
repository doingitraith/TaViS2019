using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class UnityChanController : BasicAvatarController
{	
	public override void Start(){
        // find transforms of model
        SpineBase = GameObject.Find("Root_M").transform;
        SpineMid = GameObject.Find("Spine1_M").transform;
        Neck = GameObject.Find("Neck_M").transform;
        Head = GameObject.Find("Head_M").transform;
        ShoulderLeft = GameObject.Find("Shoulder_L").transform;
        ElbowLeft = GameObject.Find("Elbow_L").transform;
        WristLeft = GameObject.Find("Wrist_L").transform;
        HandLeft = GameObject.Find("MiddleFinger1_L").transform;
        ShoulderRight = GameObject.Find("Shoulder_R").transform;
        ElbowRight = GameObject.Find("Elbow_R").transform;
        WristRight = GameObject.Find("Wrist_R").transform;
        HandRight = GameObject.Find("MiddleFinger1_R").transform;
        HipLeft = GameObject.Find("Hip_L").transform;
        KneeLeft = GameObject.Find("Knee_L").transform;
        AnkleLeft = GameObject.Find("Ankle_L").transform;
        FootLeft = GameObject.Find("Toes_L").transform;
        HipRight = GameObject.Find("Hip_R").transform;
        KneeRight = GameObject.Find("Knee_R").transform;
        AnkleRight = GameObject.Find("Ankle_R").transform;
        FootRight = GameObject.Find("Toes_R").transform;
        SpineShoulder = GameObject.Find("Chest_M").transform;
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
