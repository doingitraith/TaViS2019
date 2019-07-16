using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

public class DiscoArmRightSegment1 : IRelativeGestureSegment
{
    /// <summary>
    /// Checks the gesture.
    /// </summary>
    /// <param name="skeleton">The skeleton.</param>
    /// <returns>GesturePartResult based on if the gesture part has been completed</returns>

    public GesturePartResult CheckGesture(BasicAvatarModel skeleton)
    {
        Vector3 handRight = skeleton.getRawWorldPosition(JointType.HandRight);
        Vector3 elbowRight = skeleton.getRawWorldPosition(JointType.ElbowRight);
        Vector3 spineShoulder = skeleton.getRawWorldPosition(JointType.SpineShoulder);

        if (handRight.y > elbowRight.y) //x: rechts: positiv, links: negativ.
        {
            if (elbowRight.y > spineShoulder.y)
            {
                if (handRight.x > spineShoulder.x) //x: rechts: positiv, links: negativ.
                {
                    Debug.Log("Disco Right Segment 1 Success");
                    return GesturePartResult.Succeed;
                }
                Debug.Log("Disco Right Segment 1 Pause");
                return GesturePartResult.Pausing;
            }
            Debug.Log("Disco Right Segment 1 Pause");
            return GesturePartResult.Pausing;
        }
        return GesturePartResult.Fail;
    }
}


public class DiscoArmRightSegment2 : IRelativeGestureSegment
{
    /// <summary>
    /// Checks the gesture.
    /// </summary>
    /// <param name="skeleton">The skeleton.</param>
    /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
    public GesturePartResult CheckGesture(BasicAvatarModel skeleton)
    {
        Vector3 handRight = skeleton.getRawWorldPosition(JointType.HandRight);
        Vector3 elbowRight = skeleton.getRawWorldPosition(JointType.ElbowRight);
        Vector3 spineShoulder = skeleton.getRawWorldPosition(JointType.SpineShoulder);
        Vector3 spine = skeleton.getRawWorldPosition(JointType.SpineMid);

        if (elbowRight.y < spineShoulder.y)
        {
            if (handRight.y < elbowRight.y)
            {
                if (handRight.x < spine.x) //x: rechts: negativ, links: positiv.
                {
                    Debug.Log("Disco Right Segment 2 Success");
                    return GesturePartResult.Succeed;
                }
                Debug.Log("Disco Right Segment 2 Pause");
                return GesturePartResult.Pausing;
            }
            Debug.Log("Disco Right Segment 2 Pause");
            return GesturePartResult.Pausing;
        }
        return GesturePartResult.Fail;
    }
}

