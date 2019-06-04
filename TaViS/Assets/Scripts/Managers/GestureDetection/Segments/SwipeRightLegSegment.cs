using UnityEngine;
using Windows.Kinect;

public class SwipeRightLegSegment1 : IRelativeGestureSegment
{
    /// <summary>
    /// Checks the gesture.
    /// </summary>
    /// <param name="skeleton">The skeleton.</param>
    /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
    public GesturePartResult CheckGesture(BasicAvatarModel skeleton)
    {
        // hand below elbow and elbow below neck
        Vector3 footRight = skeleton.getRawWorldPosition(JointType.FootRight);
        Vector3 legRight = skeleton.getRawWorldPosition(JointType.KneeRight);
        Vector3 spine = skeleton.getRawWorldPosition(JointType.SpineBase);


        if (spine.x < legRight.x)
        {
            if (legRight.x < footRight.x)
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

public class SwipeRightLegSegment2 : IRelativeGestureSegment
{
    /// <summary>
    /// Checks the gesture.
    /// </summary>
    /// <param name="skeleton">The skeleton.</param>
    /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
    public GesturePartResult CheckGesture(BasicAvatarModel skeleton)
    {
        // hand below elbow and elbow below neck
        Vector3 footRight = skeleton.getRawWorldPosition(JointType.FootRight);
        Vector3 legRight = skeleton.getRawWorldPosition(JointType.KneeRight);
        Vector3 spine = skeleton.getRawWorldPosition(JointType.SpineBase);


        if (spine.x > legRight.x)
        {
            if (legRight.x > footRight.x)
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
