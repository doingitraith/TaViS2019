using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class DrinkSegment1 : IRelativeGestureSegment
{
    /// <summary>
    /// Checks the gesture.
    /// </summary>
    /// <param name="skeleton">The skeleton.</param>
    /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
    /// HAND IN FRONT OF BODY
    public GesturePartResult CheckGesture(BasicAvatarModel skeleton)
    {
        float tolerance = GameManager.Instance.GestureManager.gestureSegmentTolerance;
        Vector3 hand = skeleton.getRawWorldPosition(JointType.HandLeft);
        Vector3 elbow = skeleton.getRawWorldPosition(JointType.ElbowLeft);
        Vector3 head = skeleton.getRawWorldPosition(JointType.Head);
        Vector3 spine = skeleton.getRawWorldPosition(JointType.SpineMid);

        if (GameManager.Instance.isRightHanded)
        {
            hand = skeleton.getRawWorldPosition(JointType.HandRight);
            elbow = skeleton.getRawWorldPosition(JointType.ElbowRight);
        }

        if (hand.y < elbow.y && hand.z > elbow.z)
        {
            if (hand.x - Mathf.Abs(spine.x) <= tolerance && hand.z - Mathf.Abs(spine.z) <= tolerance)
            {

                return GesturePartResult.Succeed;
            }

            return GesturePartResult.Pausing;
        }

        return GesturePartResult.Fail;
    }
}

public class DrinkSegment2 : IRelativeGestureSegment
{

    /// <summary>
    /// Checks the gesture.
    /// </summary>
    /// <param name="skeleton">The skeleton.</param>
    /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
    public GesturePartResult CheckGesture(BasicAvatarModel skeleton)
    {
        float tolerance = GameManager.Instance.GestureManager.gestureSegmentTolerance;
        Vector3 hand = skeleton.getRawWorldPosition(JointType.HandLeft);
        Vector3 elbow = skeleton.getRawWorldPosition(JointType.ElbowLeft);
        Vector3 head = skeleton.getRawWorldPosition(JointType.Head);

        if (GameManager.Instance.isRightHanded)
        {
            hand = skeleton.getRawWorldPosition(JointType.HandRight);
            elbow = skeleton.getRawWorldPosition(JointType.ElbowRight);
        }
        //move hand to mouth
        if (hand.y < elbow.y && hand.z > elbow.z)
        {
            if (hand.y - Mathf.Abs(head.y) <= tolerance)
            {
                return GesturePartResult.Succeed;
            }
            return GesturePartResult.Pausing;
        }
        return GesturePartResult.Fail;
    }
}

public class DrinkSegment3 : IRelativeGestureSegment
{

    /// <summary>
    /// Checks the gesture.
    /// </summary>
    /// <param name="skeleton">The skeleton.</param>
    /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
    public GesturePartResult CheckGesture(BasicAvatarModel skeleton)
    {
        // hand above shoulder + elbow above shoulder + hand at elbow level
        float tolerance = GameManager.Instance.GestureManager.gestureSegmentTolerance;
        Vector3 hand = skeleton.getRawWorldPosition(JointType.HandLeft);
        Vector3 elbow = skeleton.getRawWorldPosition(JointType.ElbowLeft);
        Vector3 shoulderCenter = skeleton.getRawWorldPosition(JointType.SpineShoulder);
        Vector3 head = skeleton.getRawWorldPosition(JointType.Head);

        if (GameManager.Instance.isRightHanded)
        {
            hand = skeleton.getRawWorldPosition(JointType.HandRight);
            elbow = skeleton.getRawWorldPosition(JointType.ElbowRight);
        }

        if (hand.y > shoulderCenter.y)
        {
            if(elbow.y - Mathf.Abs(hand.y) <= tolerance)
            {
                return GesturePartResult.Succeed;
            }
            return GesturePartResult.Pausing;
        }
        return GesturePartResult.Fail;
    }
}
