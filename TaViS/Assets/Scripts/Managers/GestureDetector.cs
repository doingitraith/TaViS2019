//------------------------------------------------------------------------------
// <copyright file="GestureDetector.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

    using UnityEngine;
    using System;
    using System.Collections.Generic;
    using Windows.Kinect;
    using Microsoft.Kinect.VisualGestureBuilder;

    /// <summary>
    /// Gesture Detector class which listens for VisualGestureBuilderFrame events from the service
    /// and updates the associated GestureResultView object with the latest results for the 'Seated' gesture
    /// </summary>
    public class GestureDetector : IDisposable
    {
        /// <summary> Path to the gesture database that was trained with VGB </summary>
        private readonly string gestureDatabase = "Database/Gestures.gbd";

        /// <summary> Name of the discrete gesture in the database that we want to track </summary>
        private readonly string tipHatGesture = "TipHat";

        /// <summary> Gesture frame source which should be tied to a body tracking ID </summary>
        private VisualGestureBuilderFrameSource vgbFrameSource = null;

        /// <summary> Gesture frame reader which will handle gesture events coming from the sensor </summary>
        private VisualGestureBuilderFrameReader vgbFrameReader = null;

        public event EventHandler<GestureDetectorEventArgs> OnGestureDetected;

        /// <summary>
        /// Initializes a new instance of the GestureDetector class along with the gesture frame source and reader
        /// </summary>
        /// <param name="kinectSensor">Active sensor to initialize the VisualGestureBuilderFrameSource object with</param>
        /// <param name="gestureResultView">GestureResultView object to store gesture results of a single body to</param>
        public GestureDetector(KinectSensor kinectSensor)
        {           
            if (kinectSensor == null)
            {
                throw new ArgumentNullException("kinectSensor");
            }

            // create the vgb source. The associated body tracking ID will be set when a valid body frame arrives from the sensor.
            this.vgbFrameSource = VisualGestureBuilderFrameSource.Create(kinectSensor, 0);

            // open the reader for the vgb frames
            this.vgbFrameReader = this.vgbFrameSource.OpenReader();
            if (this.vgbFrameReader != null)
            {
                this.vgbFrameReader.IsPaused = true;
                this.vgbFrameReader.FrameArrived += this.Reader_GestureFrameArrived;
            }

            // load the 'Seated' gesture from the gesture database
            string path = Application.streamingAssetsPath + "/" + this.gestureDatabase;
            using (VisualGestureBuilderDatabase database = VisualGestureBuilderDatabase.Create(path))
            {
                // we could load all available gestures in the database with a call to vgbFrameSource.AddGestures(database.AvailableGestures), 
                // but for this program, we only want to track one discrete gesture from the database, so we'll load it by name
                foreach (Microsoft.Kinect.VisualGestureBuilder.Gesture gesture in database.AvailableGestures)
                {
                    if (gesture.Name.Equals(this.tipHatGesture))
                    {
                        this.vgbFrameSource.AddGesture(gesture);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the body tracking ID associated with the current detector
        /// The tracking ID can change whenever a body comes in/out of scope
        /// </summary>
        public ulong TrackingId
        {
            get
            {
                return this.vgbFrameSource.TrackingId;
            }

            set
            {
                if (this.vgbFrameSource.TrackingId != value)
                {
                    this.vgbFrameSource.TrackingId = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not the detector is currently paused
        /// If the body tracking ID associated with the detector is not valid, then the detector should be paused
        /// </summary>
        public bool IsPaused
        {
            get
            {
                return this.vgbFrameReader.IsPaused;
            }

            set
            {
                if (this.vgbFrameReader.IsPaused != value)
                {
                    this.vgbFrameReader.IsPaused = value;
                }
            }
        }
        /// <summary>
        /// Handles gesture detection results arriving from the sensor for the associated body tracking Id
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void Reader_GestureFrameArrived(object sender, VisualGestureBuilderFrameArrivedEventArgs e)
        {
            VisualGestureBuilderFrameReference frameReference = e.FrameReference;
            using (VisualGestureBuilderFrame frame = frameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    // get the discrete gesture results which arrived with the latest frame
                    IReadOnlyDictionary<Microsoft.Kinect.VisualGestureBuilder.Gesture, DiscreteGestureResult> discreteResults = frame.DiscreteGestureResults;

                    if (discreteResults != null)
                    {
                        // we only have one gesture in this source object, but you can get multiple gestures
                        foreach (Microsoft.Kinect.VisualGestureBuilder.Gesture gesture in this.vgbFrameSource.Gestures)
                        {
                            if (gesture.Name.Equals(this.tipHatGesture) && gesture.GestureType == GestureType.Discrete)
                            {
                                DiscreteGestureResult result = null;
                                discreteResults.TryGetValue(gesture, out result);

                                if (result != null)
                                {
                                    // update the GestureResultView object with new gesture result values
                                    if(this.OnGestureDetected != null)
                                    {
                                    this.OnGestureDetected(this, new GestureDetectorEventArgs(true, result.Detected,
                                        result.Confidence,this.tipHatGesture));
                                    }
                                    //this.GestureResultView.UpdateGestureResult(true, result.Detected, result.Confidence);
                                }
                            }
                        }
                    }
                }
            }
        }

        public class GestureDetectorEventArgs : EventArgs
        {
            public bool IsBodyTrackingIdValid { get; private set; }

            public bool IsGestureDetected { get; private set; }

            public float DetectionConfidence { get; private set; }
            public string GestureID { get; private set; }

            public GestureDetectorEventArgs(bool isBodyTrackingIdValid, bool isGestureDetected, float detectionConfidence, string gestureID)
            {
                this.IsBodyTrackingIdValid = isBodyTrackingIdValid;
                this.IsGestureDetected = isGestureDetected;
                this.DetectionConfidence = detectionConfidence;
                this.GestureID = gestureID;
            }
        }

        private void Source_TrackingIdLost(object sender, TrackingIdLostEventArgs e)
        {
            if (this.OnGestureDetected != null)
            {
                this.OnGestureDetected(this, new GestureDetectorEventArgs(false, false, 0.0f, "none"));
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.vgbFrameReader != null)
                {
                    this.vgbFrameReader.FrameArrived -= this.Reader_GestureFrameArrived;
                    this.vgbFrameReader.Dispose();
                    this.vgbFrameReader = null;
                }

                if (this.vgbFrameSource != null)
                {
                    this.vgbFrameSource.TrackingIdLost -= this.Source_TrackingIdLost;
                    this.vgbFrameSource.Dispose();
                    this.vgbFrameSource = null;
                }
            }
        }
    }
