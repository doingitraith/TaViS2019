using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Windows.Kinect;
using System;

public class BasicAvatarController : MonoBehaviour
{
    private bool walking = false;
    private bool drinking = false;
    public bool textPlaying = true;

    // all kinect joints as transform objects
    public Transform SpineBase;
    public Transform SpineMid;
    public Transform Neck;
    public Transform Head;
    public Transform ShoulderLeft;
    public Transform ElbowLeft;
    public Transform WristLeft;
    public Transform HandLeft;
    public Transform ShoulderRight;
    public Transform ElbowRight;
    public Transform WristRight;
    public Transform HandRight;
    public Transform HipLeft;
    public Transform KneeLeft;
    public Transform AnkleLeft;
    public Transform FootLeft;
    public Transform HipRight;
    public Transform KneeRight;
    public Transform AnkleRight;
    public Transform FootRight;
    public Transform SpineShoulder;
    public Transform HandTipLeft;
    public Transform ThumbLeft;
    public Transform HandTipRight;
    public Transform ThumbRight;

    private Animator animator;

    // root transformation, used to determine the initial rotation of the complete model
    public Transform RootTransform;

    // avatar of the motion capturing data
    public BasicAvatarModel MoCapAvatar;

    // dict of all the joint-transforms that are available in the model
    protected Dictionary<JointType, Transform> knownJoints = new Dictionary<JointType, Transform>();

    // initial joint rotations of the model (the rotations are "local rotations" relative to the RootTransform rotation; see Start function for further details)
    protected Dictionary<JointType, Quaternion> initialModelJointRotations = new Dictionary<JointType, Quaternion>();

    protected Dictionary<JointType, Vector3> initialModelJointPositions = new Dictionary<JointType, Vector3>();

    // array of all transform objects, some entries might be null
    protected Transform[] allJoints;

    // called by derived class (e.g. UnityChanController) at the end of its own Start function after setting the available joints
    public virtual void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        animator.enabled = false;
        allJoints = new Transform[] { SpineBase, SpineMid, Neck, Head, ShoulderLeft, ElbowLeft, WristLeft, HandLeft, ShoulderRight, ElbowRight, WristRight, HandRight, HipLeft, KneeLeft, AnkleLeft, FootLeft, HipRight, KneeRight, AnkleRight, FootRight, SpineShoulder, HandTipLeft, ThumbLeft, HandTipRight, ThumbRight };
        // check which joints were set
        foreach (JointType jt in Enum.GetValues(typeof(JointType)))
        {
            Transform joint = allJoints[(int)jt];
            if (joint != null) knownJoints[jt] = joint;
        }

        // compute initial rotation of the joints of the model
        // Note: because we want the rotation to be relative to Quaternion.identity (no rotation), we compute a "local rotation" relative to the RootTransform rotation of the model
        foreach (JointType jt in knownJoints.Keys)
        {
            initialModelJointRotations[jt] = Quaternion.Inverse(RootTransform.rotation) * knownJoints[jt].rotation;
            initialModelJointPositions[jt] = knownJoints[jt].position;
        }
    }

    // Update rotation of all known joints
    public virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SetWalking(!walking);
        }
        if (textPlaying)
        {
            if (walking)
            {
                return;
            }
        }
        foreach (JointType jt in knownJoints.Keys)
        {
            // the applyRelativeRotationChange function returns the new "local rotation" relative to the RootTransform Rotation...
            Quaternion localRotTowardsRootTransform = MoCapAvatar.applyRelativeRotationChange(jt, initialModelJointRotations[jt]);
            Vector3 localPosTowardsRootTransform = MoCapAvatar.getRawWorldPosition(jt);
            // ...therefore we have to multiply it with the RootTransform Rotation to get the global rotation of the joint
            knownJoints[jt].rotation = RootTransform.rotation * localRotTowardsRootTransform;
            //knownJoints[jt].position = localPosTowardsRootTransform;
        }
    }

    public void SetWalking(bool walking)
    {
        if (walking)
        {
            gameObject.GetComponent<UnityEngine.AudioSource>().Play();
        }
        else
        {
            gameObject.GetComponent<UnityEngine.AudioSource>().Stop();
        }
        this.walking = walking;
        gameObject.GetComponent<Animator>().enabled = walking;
    }

    public bool GetWalking()
    {
        return walking;
    }
}
