using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

public class TPoseSegment1 : IRelativeGestureSegment
{
    /// <summary>
    /// Checks the gesture.
    /// </summary>
    /// <param name="skeleton">The skeleton.</param>
    /// <returns>GesturePartResult based on if the gesture part has been completed</returns>

    public GesturePartResult CheckGesture(BasicAvatarModel skeleton)
    {
        Vector3 spine = skeleton.getRawWorldPosition(JointType.SpineMid);
        Vector3 handLeft = skeleton.getRawWorldPosition(JointType.HandLeft);
        Vector3 handRight = skeleton.getRawWorldPosition(JointType.HandRight);
        Vector3 elbowLeft = skeleton.getRawWorldPosition(JointType.ElbowLeft);
        Vector3 elbowRight = skeleton.getRawWorldPosition(JointType.ElbowRight);

        if (handLeft.x > elbowLeft.x && handRight.x < elbowRight.x)
        {
            return GesturePartResult.Succeed;
        }

        return GesturePartResult.Fail;
    }
}