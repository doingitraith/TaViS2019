using UnityEngine;
using System;

public class Gesture : MonoBehaviour
{
    private GestureManager gestureManager;
    /// <summary>
    /// The parts that make up this gesture
    /// </summary>
    private IRelativeGestureSegment[] gestureParts;

    /// <summary>
    /// The current gesture part that we are matching against
    /// </summary>
    private int currentGesturePart = 0;

    /// <summary>
    /// the number of frames to pause for when a pause is initiated
    /// </summary>
    private int pausedFrameCount = 10;

    /// <summary>
    /// The current frame that we are on
    /// </summary>
    private int frameCount = 0;

    /// <summary>
    /// Are we paused?
    /// </summary>
    private bool paused = false;

    /// <summary>
    /// The name of gesture that this is
    /// </summary>
    private GestureManager.GESTURENAME name;

    /// <summary>
    /// Initializes a new instance of the <see cref="Gesture"/> class.
    /// </summary>
    /// <param name="type">The type of gesture.</param>
    /// <param name="gestureParts">The gesture parts.</param>
    public Gesture(GestureManager.GESTURENAME name, IRelativeGestureSegment[] gestureParts)
    {
        this.gestureParts = gestureParts;
        this.name = name;
        gestureManager = GameObject.Find("GameManager").GetComponent<GestureManager>();
    }

    /// <summary>
    /// Occurs when [gesture recognised].
    /// </summary>
    public event EventHandler<GestureEventArgs> GestureRecognizedInGesture;

    /// <summary>
    /// Updates the gesture.
    /// </summary>
    /// <param name="data">The skeleton data.</param>
    public void UpdateGesture(BasicAvatarModel data)
    {
        if (this.paused)
        {
            if (this.frameCount == this.pausedFrameCount)
            {
                this.paused = false;
            }

            this.frameCount++;
        }

        GesturePartResult result = this.gestureParts[this.currentGesturePart].CheckGesture(data);
        if (result == GesturePartResult.Succeed)
        {
            // There are still segments of our gesture 
            if (this.currentGesturePart + 1 < this.gestureParts.Length)
            {
                // increase the currentGesturePart to check for the next part of the gesture the next time this method is called
                this.currentGesturePart++;
                this.frameCount = 0;    // reset the frame counter
                this.pausedFrameCount = 10; // make a short break of 10 frames 
                this.paused = true;
            }
            else // Found last segment of the gesture
            {
                if (this.GestureRecognizedInGesture != null) // make sure our event is associated with a method
                {
                    // fire the event
                    // The method to be called is defined in GestureController.cs
                    this.GestureRecognizedInGesture(this, new GestureEventArgs(this.name, data.getTrackingID())); 
                    this.Reset();
                }
            }
        }
        else if (result == GesturePartResult.Fail || this.frameCount == 50)
        {
            this.currentGesturePart = 0;
            this.frameCount = 0;
            this.pausedFrameCount = 5;
            this.paused = true;
        }
        else
        {
            gestureManager.IncreasePauseCount();
            this.frameCount++;
            this.pausedFrameCount = 5;
            this.paused = true;
        }
    }

    /// <summary>
    /// Resets this instance.
    /// </summary>
    public void Reset()
    {
        this.currentGesturePart = 0;
        this.frameCount = 0;
        this.pausedFrameCount = 5;
        this.paused = true;
    }
}
