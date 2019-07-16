using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

public class DiscoArmLeftSegment1 : IRelativeGestureSegment
{
    /// <summary>
    /// Checks the gesture.
    /// </summary>
    /// <param name="skeleton">The skeleton.</param>
    /// <returns>GesturePartResult based on if the gesture part has been completed</returns>

    public GesturePartResult CheckGesture(BasicAvatarModel skeleton)
    {
        Vector3 handLeft = skeleton.getRawWorldPosition(JointType.HandLeft);
        Vector3 elbowLeft = skeleton.getRawWorldPosition(JointType.ElbowLeft);
        Vector3 spineShoulder = skeleton.getRawWorldPosition(JointType.SpineShoulder);

        if (handLeft.y > elbowLeft.y) //x: rechts: positiv, links: negativ.
        {
            if (elbowLeft.y > spineShoulder.y)
            {
                if (handLeft.x < spineShoulder.x) //x: rechts: positiv, links: negativ.
                {
                    Debug.Log("Disco Left Segment 1 Success");
                    return GesturePartResult.Succeed;
                }
                Debug.Log("Disco Left Segment 1 Pause");
                return GesturePartResult.Pausing;
            }
            Debug.Log("Disco Left Segment 1 Pause");
            return GesturePartResult.Pausing;
        }
        return GesturePartResult.Fail;
    }
}


public class DiscoArmLeftSegment2 : IRelativeGestureSegment
{
    /// <summary>
    /// Checks the gesture.
    /// </summary>
    /// <param name="skeleton">The skeleton.</param>
    /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
    public GesturePartResult CheckGesture(BasicAvatarModel skeleton)
    {
        Vector3 handLeft = skeleton.getRawWorldPosition(JointType.HandLeft);
        Vector3 elbowLeft = skeleton.getRawWorldPosition(JointType.ElbowLeft);
        Vector3 spineShoulder = skeleton.getRawWorldPosition(JointType.SpineShoulder);
        Vector3 spine = skeleton.getRawWorldPosition(JointType.SpineMid);

        if (elbowLeft.y < spineShoulder.y)
        {
            if (handLeft.y < elbowLeft.y)
            {
                if (handLeft.x > spine.x) //x: rechts: negativ, links: positiv.
                {
                    Debug.Log("Disco Left Segment 2 Success");
                    return GesturePartResult.Succeed;
                }
                Debug.Log("Disco Left Segment 2 Pause");
                return GesturePartResult.Pausing;
            }
            Debug.Log("Disco Left Segment 2 Pause");
            return GesturePartResult.Pausing;
        }
        return GesturePartResult.Fail;
    }
}