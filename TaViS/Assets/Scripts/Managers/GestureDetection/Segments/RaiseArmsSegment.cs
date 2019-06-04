using UnityEngine;
using Windows.Kinect;

public class RaiseArmsSegment1 : IRelativeGestureSegment
{
    /// <summary>
    /// Checks the gesture.
    /// </summary>
    /// <param name="skeleton">The skeleton.</param>
    /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
    public GesturePartResult CheckGesture(BasicAvatarModel skeleton)
    {
        // hand below elbow and elbow below neck
        Vector3 handLeft = skeleton.getRawWorldPosition(JointType.HandLeft);
        Vector3 elbowLeft = skeleton.getRawWorldPosition(JointType.ElbowLeft);
        Vector3 neck = skeleton.getRawWorldPosition(JointType.Neck);
        

        if (handLeft.y < elbowLeft.y)
        {
            if (elbowLeft.y < neck.y)
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

public class RaiseArmsSegment2 : IRelativeGestureSegment
{
    /// <summary>
    /// Checks the gesture.
    /// </summary>
    /// <param name="skeleton">The skeleton.</param>
    /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
    public GesturePartResult CheckGesture(BasicAvatarModel skeleton)
    {
        // hand above elbow and elbow above neck
        Vector3 handLeft = skeleton.getRawWorldPosition(JointType.HandLeft);
        Vector3 elbowLeft = skeleton.getRawWorldPosition(JointType.ElbowLeft);
        Vector3 neck = skeleton.getRawWorldPosition(JointType.Neck);


        if (handLeft.y > elbowLeft.y)
        {
            if (elbowLeft.y > neck.y)
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
