using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

public class OutRightLegSegment1 : IRelativeGestureSegment
{
    /// <summary>
    /// Checks the gesture.
    /// </summary>
    /// <param name="skeleton">The skeleton.</param>
    /// <returns>GesturePartResult based on if the gesture part has been completed</returns>

    public GesturePartResult CheckGesture(BasicAvatarModel skeleton)
    {
        Vector3 shoulderRight = skeleton.getRawWorldPosition(JointType.ShoulderRight);
        Vector3 footLeft = skeleton.getRawWorldPosition(JointType.FootLeft);
        Vector3 footRight = skeleton.getRawWorldPosition(JointType.FootRight);

        //x: rechts: negativ, links: positiv.
        //z: hinten: negative, vorne: positiv 
        if (footRight.x < footLeft.x)
        {
            if (footRight.x < shoulderRight.x)
            {
                return GesturePartResult.Succeed;
            }
            return GesturePartResult.Pausing;
        }

        return GesturePartResult.Fail;
    }
}
public class OutRightLegSegment2 : IRelativeGestureSegment
{
    /// <summary>
    /// Checks the gesture.
    /// </summary>
    /// <param name="skeleton">The skeleton.</param>
    /// <returns>GesturePartResult based on if the gesture part has been completed</returns>

    public GesturePartResult CheckGesture(BasicAvatarModel skeleton)
    {
        Vector3 shoulderRight = skeleton.getRawWorldPosition(JointType.ShoulderRight);
        Vector3 footLeft = skeleton.getRawWorldPosition(JointType.FootLeft);
        Vector3 footRight = skeleton.getRawWorldPosition(JointType.FootRight);

        //x: rechts: negativ, links: positiv.
        //z: hinten: negative, vorne: positiv 
        if (footRight.x < shoulderRight.x)
        {
            if (footRight.x < footLeft.x)
            {
                return GesturePartResult.Succeed;
            }
            return GesturePartResult.Pausing;
        }
        return GesturePartResult.Fail;
    }
}