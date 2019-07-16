using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class PullToLeftSegment1 : IRelativeGestureSegment {

	/// <summary>
    /// Checks the gesture.
    /// </summary>
    /// <param name="skeleton">The skeleton.</param>
    /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
    public GesturePartResult CheckGesture(BasicAvatarModel skeleton)
    {
        // left hand under shoulder and right hand in front of body
        Vector3 handLeft = skeleton.getRawWorldPosition(JointType.HandLeft);
        Vector3 shoulderCenter = skeleton.getRawWorldPosition(JointType.SpineShoulder);
        Vector3 handRight = skeleton.getRawWorldPosition(JointType.HandRight);        

        if (handLeft.y < shoulderCenter.y && handRight.z > shoulderCenter.z)
        {
            // right hand above head and right of right elbow
            Vector3 head = skeleton.getRawWorldPosition(JointType.Head);
            Vector3 elbowRight = skeleton.getRawWorldPosition(JointType.ElbowRight);        

            if (handRight.y > head.y && handRight.x > elbowRight.x)
            {
                return GesturePartResult.Succeed;
            }

            return GesturePartResult.Pausing;
        }

        return GesturePartResult.Fail;
    }
}

public class PullToLeftSegment2 : IRelativeGestureSegment
{

    /// <summary>
    /// Checks the gesture.
    /// </summary>
    /// <param name="skeleton">The skeleton.</param>
    /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
    public GesturePartResult CheckGesture(BasicAvatarModel skeleton)
    {        
        // left hand under shoulder and right hand in front of body
        Vector3 handLeft = skeleton.getRawWorldPosition(JointType.HandLeft);
        Vector3 shoulderCenter = skeleton.getRawWorldPosition(JointType.SpineShoulder);
        Vector3 handRight = skeleton.getRawWorldPosition(JointType.HandRight);        

        if (handLeft.y < shoulderCenter.y && handRight.z > shoulderCenter.z)
        {
            // right hand above hip center and below head and left of right elbow and right of hip center
            Vector3 head = skeleton.getRawWorldPosition(JointType.Head);
            Vector3 elbowRight = skeleton.getRawWorldPosition(JointType.ElbowRight);
            Vector3 hipCenter = skeleton.getRawWorldPosition(JointType.SpineBase);      

            if (handRight.y < head.y && handRight.y > hipCenter.y &&
                handRight.x < elbowRight.x && handRight.x > hipCenter.x)
            {
                return GesturePartResult.Succeed;
            }

            return GesturePartResult.Pausing;
        }

        return GesturePartResult.Fail;
    }
}

public class PullToLeftSegment3 : IRelativeGestureSegment
{

    /// <summary>
    /// Checks the gesture.
    /// </summary>
    /// <param name="skeleton">The skeleton.</param>
    /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
    public GesturePartResult CheckGesture(BasicAvatarModel skeleton)
    {
        // left hand under shoulder and right hand in front of body
        Vector3 handLeft = skeleton.getRawWorldPosition(JointType.HandLeft);
        Vector3 shoulderCenter = skeleton.getRawWorldPosition(JointType.SpineShoulder);
        Vector3 handRight = skeleton.getRawWorldPosition(JointType.HandRight);        

        if (handLeft.y < shoulderCenter.y && handRight.z > shoulderCenter.z)
        {
            // right hand below right elbow and left of hip center
            Vector3 elbowRight = skeleton.getRawWorldPosition(JointType.ElbowRight);
            Vector3 hipCenter = skeleton.getRawWorldPosition(JointType.SpineBase);      

            if (handRight.y < elbowRight.y && handRight.x < hipCenter.x)
            {
                return GesturePartResult.Succeed;
            }

            return GesturePartResult.Pausing;
        }

        return GesturePartResult.Fail;
    }
}
