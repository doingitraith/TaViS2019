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
        Vector3 head = skeleton.getRawWorldPosition(JointType.Head);
        Vector3 spineShoulder = skeleton.getRawWorldPosition(JointType.SpineShoulder);
        Vector3 spine = skeleton.getRawWorldPosition(JointType.SpineBase);

        if (handLeft.y > head.y) //x: rechts: positiv, links: negativ.
        {
            if (handLeft.x < head.x) //x: rechts: positiv, links: negativ.
            {
                return GesturePartResult.Succeed;
            }
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
        Vector3 head = skeleton.getRawWorldPosition(JointType.Head);
        Vector3 spineShoulder = skeleton.getRawWorldPosition(JointType.SpineShoulder);
        Vector3 spine = skeleton.getRawWorldPosition(JointType.SpineBase);

        if (handLeft.y < spine.y)
        {
            if (handLeft.x > spine.x) //x: rechts: negativ, links: positiv.
            {
                return GesturePartResult.Succeed;
            }
            return GesturePartResult.Pausing;
        }
        return GesturePartResult.Fail;
    }
}

