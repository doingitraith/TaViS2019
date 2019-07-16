using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

public class OutLeftLegSegment1 : IRelativeGestureSegment
{
    /// <summary>
    /// Checks the gesture.
    /// </summary>
    /// <param name="skeleton">The skeleton.</param>
    /// <returns>GesturePartResult based on if the gesture part has been completed</returns>

    public GesturePartResult CheckGesture(BasicAvatarModel skeleton)
    {
        Vector3 shoulderLeft = skeleton.getRawWorldPosition(JointType.ShoulderLeft);
        Vector3 footLeft = skeleton.getRawWorldPosition(JointType.FootLeft);
        Vector3 footRight = skeleton.getRawWorldPosition(JointType.FootRight);

        //x: rechts: negativ, links: positiv.
        //z: hinten: negative, vorne: positiv 
        if (footLeft.x > footRight.x)
        {
            if (footLeft.x > shoulderLeft.x)
            {
                return GesturePartResult.Succeed;
            }
            return GesturePartResult.Pausing;
        }
       
        return GesturePartResult.Fail;
    }
}
public class OutLeftLegSegment2 : IRelativeGestureSegment
{
    /// <summary>
    /// Checks the gesture.
    /// </summary>
    /// <param name="skeleton">The skeleton.</param>
    /// <returns>GesturePartResult based on if the gesture part has been completed</returns>

    public GesturePartResult CheckGesture(BasicAvatarModel skeleton)
    {
        Vector3 shoulderLeft = skeleton.getRawWorldPosition(JointType.ShoulderLeft);
        Vector3 footLeft = skeleton.getRawWorldPosition(JointType.FootLeft);
        Vector3 footRight = skeleton.getRawWorldPosition(JointType.FootRight);

        //x: rechts: negativ, links: positiv.
        //z: hinten: negative, vorne: positiv 
        if (footLeft.x > shoulderLeft.x)
        {
            if (footLeft.x > footRight.x)
            {
                return GesturePartResult.Succeed;
            }
            return GesturePartResult.Pausing;
        }
        return GesturePartResult.Fail;
    }
}
