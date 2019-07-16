using UnityEngine;
using Windows.Kinect;

/// <summary>
/// The first part of the swipe left gesture
/// </summary>
public class SwipeToLeftSegment1 : IRelativeGestureSegment
{
    /// <summary>
    /// Checks the gesture.
    /// </summary>
    /// <param name="skeleton">The skeleton.</param>
    /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
    public GesturePartResult CheckGesture(BasicAvatarModel skeleton)
    {
        // right hand in front of right shoulder
        Vector3 handLeft = skeleton.getRawWorldPosition(JointType.HandLeft);
        Vector3 shoulderCenter = skeleton.getRawWorldPosition(JointType.SpineShoulder);
        Vector3 handRight = skeleton.getRawWorldPosition(JointType.HandRight);
        Vector3 elbowRight = skeleton.getRawWorldPosition(JointType.ElbowRight);      

        if (handRight.z > elbowRight.z && handLeft.y < shoulderCenter.y)
        {
            // right hand below shoulder height but above hip height
            Vector3 head = skeleton.getRawWorldPosition(JointType.Head);
            Vector3 hipCenter = skeleton.getRawWorldPosition(JointType.SpineBase);
            Vector3 shoulderRight = skeleton.getRawWorldPosition(JointType.ShoulderRight);   

            if (handRight.y < head.y && handRight.y > hipCenter.y)
            {
                // right hand right of right shoulder
                if (handRight.x > shoulderRight.x)
                {
                    //Debug.Log("Segment1 Success");
                    return GesturePartResult.Succeed;
                }
                return GesturePartResult.Pausing;
            }
            return GesturePartResult.Fail;
        }
        return GesturePartResult.Fail;
    }
}

/// <summary>
/// The second part of the swipe left gesture
/// </summary>
public class SwipeToLeftSegment2 : IRelativeGestureSegment
{
    /// <summary>
    /// Checks the gesture.
    /// </summary>
    /// <param name="skeleton">The skeleton.</param>
    /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
    public GesturePartResult CheckGesture(BasicAvatarModel skeleton)
    {
        // right hand in front of right shoulder
        Vector3 handLeft = skeleton.getRawWorldPosition(JointType.HandLeft);
        Vector3 shoulderCenter = skeleton.getRawWorldPosition(JointType.SpineShoulder);
        Vector3 handRight = skeleton.getRawWorldPosition(JointType.HandRight);
        Vector3 elbowRight = skeleton.getRawWorldPosition(JointType.ElbowRight);      

        if (handRight.z > elbowRight.z && handLeft.y < shoulderCenter.y)
        {
            // right hand below shoulder height but above hip height
            Vector3 head = skeleton.getRawWorldPosition(JointType.Head);
            Vector3 hipCenter = skeleton.getRawWorldPosition(JointType.SpineBase);
            

            if (handRight.y < head.y && handRight.y > hipCenter.y)
            {
                // right hand left of right shoulder & right of left shoulder
                Vector3 shoulderRight = skeleton.getRawWorldPosition(JointType.ShoulderRight);
                Vector3 shoulderLeft = skeleton.getRawWorldPosition(JointType.ShoulderLeft);   

                if (handRight.x < shoulderRight.x && handRight.x > shoulderLeft.x)
                {
                    //Debug.Log("Segment2 Success");
                    return GesturePartResult.Succeed;
                }
                return GesturePartResult.Pausing;
            }
            return GesturePartResult.Fail;
        }
        return GesturePartResult.Fail;
    }
}

/// <summary>
/// The third part of the swipe left gesture
/// </summary>
public class SwipeToLeftSegment3 : IRelativeGestureSegment
{
    /// <summary>
    /// Checks the gesture.
    /// </summary>
    /// <param name="skeleton">The skeleton.</param>
    /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
    public GesturePartResult CheckGesture(BasicAvatarModel skeleton)
    {
        // right hand in front of right shoulder
        Vector3 handLeft = skeleton.getRawWorldPosition(JointType.HandLeft);
        Vector3 shoulderCenter = skeleton.getRawWorldPosition(JointType.SpineShoulder);
        Vector3 handRight = skeleton.getRawWorldPosition(JointType.HandRight);
        Vector3 elbowRight = skeleton.getRawWorldPosition(JointType.ElbowRight);     
 
        if (handRight.z > elbowRight.z && handLeft.y < shoulderCenter.y)
        {
            // right hand below shoulder height but above hip height
            Vector3 head = skeleton.getRawWorldPosition(JointType.Head);
            Vector3 hipCenter = skeleton.getRawWorldPosition(JointType.SpineBase);

            if (handRight.y < head.y && handRight.y > hipCenter.y)
            {
                // //right hand left of center hip              
                if (handRight.x < hipCenter.x)
                {
                    //Debug.Log("Segment3 Success");
                    return GesturePartResult.Succeed;
                }

                return GesturePartResult.Pausing;
            }

            return GesturePartResult.Fail;
        }

        return GesturePartResult.Fail;
    }
}