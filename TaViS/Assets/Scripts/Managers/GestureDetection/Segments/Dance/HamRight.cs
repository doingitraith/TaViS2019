using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

public class HamRightSegment1 : IRelativeGestureSegment
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
        //Vector3 neck = skeleton.getRawWorldPosition(JointType.Neck);

        //x: rechts: positiv, links: negativ.
        //y: oben: positiv, unten: negativ.

        if (handRight.x > elbowRight.x)
        {
            if (handRight.y > elbowRight.y)                                                                    
            {
                return GesturePartResult.Succeed;
            }

            // foot has not dropped but is not quite where we expect it to be, pausing till next frame
            return GesturePartResult.Pausing;
        }

        // foot dropped - no gesture fails
        return GesturePartResult.Fail;
    }
}

public class HamRightSegment2 : IRelativeGestureSegment
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
        Vector3 neck = skeleton.getRawWorldPosition(JointType.Neck);

        //x: rechts: positiv, links: negativ.
        //y: oben: positiv, unten: negativ.

        if (handRight.x < elbowRight.x)
        {
            if (handRight.y < elbowRight.y)
            {
                return GesturePartResult.Succeed;
            }

            // foot has not dropped but is not quite where we expect it to be, pausing till next frame
            return GesturePartResult.Pausing;
        }

        // foot dropped - no gesture fails
        return GesturePartResult.Fail;
    }
}