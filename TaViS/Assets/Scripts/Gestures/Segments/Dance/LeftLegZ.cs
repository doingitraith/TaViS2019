﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

//not stable enough, not used in game
public class LeftLegZSegment1 : IRelativeGestureSegment
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
        Vector3 handLeft = skeleton.getRawWorldPosition(JointType.HandLeft);
        Vector3 handRight = skeleton.getRawWorldPosition(JointType.HandRight);
        Vector3 elbowLeft = skeleton.getRawWorldPosition(JointType.ElbowLeft);
        Vector3 elbowRight = skeleton.getRawWorldPosition(JointType.ElbowRight);

        if (handLeft.x > elbowLeft.x && handRight.x < elbowRight.x && spine.x < footLeft.x)
        {
            if ((footRight.x > footLeft.x) && ((footRight.z < footLeft.z))) //x: rechts: negativ, links: positiv.
                                                                            //z: hinten: negative, vorne: positiv
            {
                return GesturePartResult.Succeed;
            }
            return GesturePartResult.Pausing;
        }

        return GesturePartResult.Fail;
    }
}

public class LeftLegZSegment2 : IRelativeGestureSegment
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
        Vector3 handLeft = skeleton.getRawWorldPosition(JointType.HandLeft);
        Vector3 handRight = skeleton.getRawWorldPosition(JointType.HandRight);
        Vector3 elbowLeft = skeleton.getRawWorldPosition(JointType.ElbowLeft);
        Vector3 elbowRight = skeleton.getRawWorldPosition(JointType.ElbowRight);

        if (handLeft.x > elbowLeft.x && handRight.x < elbowRight.x && (footRight.x > footLeft.x)) //x: rechts: negativ, links: positiv.
                                                                        //z: hinten: negative, vorne: positiv 
        {
            if (spine.x < footLeft.x)
            {
                return GesturePartResult.Succeed;
            }

            return GesturePartResult.Pausing;
        }
        return GesturePartResult.Fail;
    }
}
