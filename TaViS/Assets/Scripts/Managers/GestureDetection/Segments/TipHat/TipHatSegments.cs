using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class TipHatSegment1 : IRelativeGestureSegment
{
    /// <summary>
    /// Checks the gesture.
    /// </summary>
    /// <param name="skeleton">The skeleton.</param>
    /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
    /// MOVE HAND TO HAT
    public GesturePartResult CheckGesture(BasicAvatarModel skeleton)
    {
        float tolerance = GameManager.Instance.GestureManager.gestureSegmentTolerance;
        Vector3 hand = skeleton.getRawWorldPosition(JointType.HandLeft);
        Vector3 elbow = skeleton.getRawWorldPosition(JointType.ElbowLeft);
        //Vector3 head = skeleton.getRawWorldPosition(JointType.Head);
        Vector3 headTop = GameObject.Find("head_end").gameObject.transform.position;

        if (GameManager.Instance.isRightHanded)
        {
            hand = skeleton.getRawWorldPosition(JointType.HandRight);
            elbow = skeleton.getRawWorldPosition(JointType.ElbowRight);
        }
        //move hand to head
        if (hand.y > elbow.y)
        {
            if (hand.y - Mathf.Abs(headTop.y) <= tolerance)
            {
                return GesturePartResult.Succeed;
            }
            return GesturePartResult.Pausing;
        }
        return GesturePartResult.Fail;
    }
}
public class TipHatSegment2 : IRelativeGestureSegment
{
    //Move Hat away
    public GesturePartResult CheckGesture(BasicAvatarModel skeleton)
    {
        float tolerance = GameManager.Instance.GestureManager.gestureSegmentTolerance;
        Vector3 hand = skeleton.getRawWorldPosition(JointType.HandLeft);
        Vector3 elbow = skeleton.getRawWorldPosition(JointType.ElbowLeft);
        //Vector3 head = skeleton.getRawWorldPosition(JointType.Head);
        Vector3 shoulder = skeleton.getRawWorldPosition(JointType.SpineShoulder);
        Vector3 headTop = GameObject.Find("head_end").gameObject.transform.position;

        if (GameManager.Instance.isRightHanded)
        {
            hand = skeleton.getRawWorldPosition(JointType.HandRight);
            elbow = skeleton.getRawWorldPosition(JointType.ElbowRight);
        }
        //move hand to head
        if (hand.y > elbow.y && hand.y > shoulder.y)
        {
            if (hand.y > Mathf.Abs(headTop.y) + tolerance)
            {
                return GesturePartResult.Succeed;
            }
            return GesturePartResult.Pausing;
        }
        return GesturePartResult.Fail;
    }
}
