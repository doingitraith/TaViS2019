using UnityEngine;
using Windows.Kinect;

public class SwipeLeftArmSegment1 : IRelativeGestureSegment
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
        Vector3 spine = skeleton.getRawWorldPosition(JointType.SpineShoulder);
        Vector3 head = skeleton.getRawWorldPosition(JointType.Head);

        //move left hand to right of torso
        if (handLeft.y < head.y && (handLeft.x > elbowLeft.x))
        {
            if (elbowLeft.x > spine.x)
            {
                return GesturePartResult.Succeed;
            }

            return GesturePartResult.Pausing;
        }

        return GesturePartResult.Fail;
    }
}

public class SwipeLeftArmSegment2 : IRelativeGestureSegment
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
        Vector3 spine = skeleton.getRawWorldPosition(JointType.SpineShoulder);
        Vector3 head = skeleton.getRawWorldPosition(JointType.Head);

        //move hand from right of torso to left of elbow
        if (handLeft.y < head.y && handLeft.x < spine.x)
        {
            if (elbowLeft.x < spine.x)
            {
                return GesturePartResult.Succeed;
            }

            return GesturePartResult.Pausing;
        }

        return GesturePartResult.Fail;
    }
}

