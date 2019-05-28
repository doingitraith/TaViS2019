using UnityEngine;
using Windows.Kinect;

public class WaveLeftSegment1 : IRelativeGestureSegment
{
    /// <summary>
    /// Checks the gesture.
    /// </summary>
    /// <param name="skeleton">The skeleton.</param>
    /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
    public GesturePartResult CheckGesture(BasicAvatarModel skeleton)
    {
        // hand above elbow
        Vector3 handLeft = skeleton.getRawWorldPosition(JointType.HandLeft);
        Vector3 elbowLeft = skeleton.getRawWorldPosition(JointType.ElbowLeft); 

        if (handLeft.y > elbowLeft.y)
        {
            // hand right of elbow
            if (handLeft.x > elbowLeft.x)
            {
                return GesturePartResult.Succeed;
            }

            // hand has not dropped but is not quite where we expect it to be, pausing till next frame
            return GesturePartResult.Pausing;
        }

        // hand dropped - no gesture fails
        return GesturePartResult.Fail;
    }
}

public class WaveLeftSegment2 : IRelativeGestureSegment
{
    /// <summary>
    /// Checks the gesture.
    /// </summary>
    /// <param name="skeleton">The skeleton.</param>
    /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
    public GesturePartResult CheckGesture(BasicAvatarModel skeleton)
    {
        // hand above elbow
        Vector3 handLeft = skeleton.getRawWorldPosition(JointType.HandLeft);
        Vector3 elbowLeft = skeleton.getRawWorldPosition(JointType.ElbowLeft); 

        if (handLeft.y > elbowLeft.y)
        {
            // hand left of elbow
            if (handLeft.x < elbowLeft.x)
            {
                return GesturePartResult.Succeed;
            }

            // hand has not dropped but is not quite where we expect it to be, pausing till next frame
            return GesturePartResult.Pausing;
        }

        // hand dropped - no gesture fails
        return GesturePartResult.Fail;
    }
}
