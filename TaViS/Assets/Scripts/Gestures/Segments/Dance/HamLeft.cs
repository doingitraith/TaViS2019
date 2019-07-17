using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

//was not stable enough, not used in game
public class HamLeftSegment1 : IRelativeGestureSegment
{
    /// <summary>
    /// Checks the gesture.
    /// </summary>
    /// <param name="skeleton">The skeleton.</param>
    /// <returns>GesturePartResult based on if the gesture part has been completed</returns>

    public GesturePartResult CheckGesture(BasicAvatarModel skeleton)
    {
        Vector3 handLeft = skeleton.getRawWorldPosition(JointType.HandRight);
        Vector3 elbowLeft = skeleton.getRawWorldPosition(JointType.ElbowLeft);
        //Vector3 neck = skeleton.getRawWorldPosition(JointType.Neck);

        //x: rechts: negativ, links: positiv.
        //z: hinten: negative, vorne: positiv.
        //y: oben: positiv, unten: negativ.

        if (handLeft.x < elbowLeft.x)
        {
            if (handLeft.y > elbowLeft.y)
            {
                return GesturePartResult.Succeed;
            }
            return GesturePartResult.Pausing;
        }

        return GesturePartResult.Fail;
    }
}

public class HamLeftSegment2 : IRelativeGestureSegment
{
    /// <summary>
    /// Checks the gesture.
    /// </summary>
    /// <param name="skeleton">The skeleton.</param>
    /// <returns>GesturePartResult based on if the gesture part has been completed</returns>

    public GesturePartResult CheckGesture(BasicAvatarModel skeleton)
    {
        Vector3 handLeft = skeleton.getRawWorldPosition(JointType.HandRight);
        Vector3 elbowLeft = skeleton.getRawWorldPosition(JointType.ElbowLeft);
        Vector3 neck = skeleton.getRawWorldPosition(JointType.Neck);

        //x: rechts: negativ, links: positiv.
        //z: hinten: negativ, vorne: positiv.
        //y: oben: positiv, unten: negativ.

        if (handLeft.x > elbowLeft.x)
        {
            if (handLeft.y < elbowLeft.y)
            {
                return GesturePartResult.Succeed;
            }
            Debug.Log("Ham Left Segment 2 Pause");
            return GesturePartResult.Pausing;
        }

        return GesturePartResult.Fail;
    }
}