using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

public class SwipeLeftLegSegment1 : IRelativeGestureSegment
{
    /// <summary>
    /// Checks the gesture.
    /// </summary>
    /// <param name="skeleton">The skeleton.</param>
    /// <returns>GesturePartResult based on if the gesture part has been completed</returns>

    public GesturePartResult CheckGesture(BasicAvatarModel skeleton)
    {
        Vector3 footRight = skeleton.getRawWorldPosition(JointType.FootRight);
        Vector3 footLeft = skeleton.getRawWorldPosition(JointType.FootLeft);
        //Vector3 legRight = skeleton.getRawWorldPosition(JointType.KneeRight);
        Vector3 spine = skeleton.getRawWorldPosition(JointType.SpineBase);


        if (spine.x < footLeft.x)
        {
            if (footRight.x > footLeft.x)
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

public class SwipeLeftLegSegment2 : IRelativeGestureSegment
{
    /// <summary>
    /// Checks the gesture.
    /// </summary>
    /// <param name="skeleton">The skeleton.</param>
    /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
    public GesturePartResult CheckGesture(BasicAvatarModel skeleton)
    {
        Vector3 footRight = skeleton.getRawWorldPosition(JointType.FootRight);
        Vector3 footLeft = skeleton.getRawWorldPosition(JointType.FootLeft);
        //Vector3 legRight = skeleton.getRawWorldPosition(JointType.KneeRight);
        Vector3 spine = skeleton.getRawWorldPosition(JointType.SpineBase);


        if (footRight.x > footLeft.x)
        {
            if (spine.x < footLeft.x)
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
