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
        Vector3 handLeft = skeleton.getRawWorldPosition(JointType.HandLeft);
        Vector3 elbowLeft = skeleton.getRawWorldPosition(JointType.ElbowLeft);
        Vector3 neck = skeleton.getRawWorldPosition(JointType.Neck);
        

        if (handLeft.y > elbowLeft.y)
        {
            if (elbowLeft.y > neck.y)
            {
                return GesturePartResult.Succeed;
            }

            return GesturePartResult.Pausing;
        }

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
        Vector3 handRight = skeleton.getRawWorldPosition(JointType.HandRight);
        Vector3 elbowRight = skeleton.getRawWorldPosition(JointType.ElbowRight);
        Vector3 neck = skeleton.getRawWorldPosition(JointType.Neck);


        if (handRight.y > elbowRight.y)
        {
            if (elbowRight.y > neck.y)
            {
                return GesturePartResult.Succeed;
            }

            return GesturePartResult.Pausing;
        }

        return GesturePartResult.Fail;
    }
}
