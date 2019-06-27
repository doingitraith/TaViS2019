using UnityEngine;
using System.Collections;
using Windows.Kinect;
using System;
using UnityEngine.UI;

public class BodySourceManager : MonoBehaviour
{

    private KinectSensor _sensor;
    private BodyFrameReader _reader;
    private Body[] _data;
    private GestureDetector gestureDetector;
    private int bodyCount;
    private int counter = 0;

    public Text ConfidenceUI;

    // Use this for initialization
    void Awake()
    {
        _sensor = KinectSensor.GetDefault();

        if (_sensor != null)
        {
            _reader = _sensor.BodyFrameSource.OpenReader();
            bodyCount = _sensor.BodyFrameSource.BodyCount;

            if (!_sensor.IsOpen)
            {
                _sensor.Open();
                gestureDetector = new GestureDetector(_sensor);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_reader != null)
        {
            BodyFrame frame = _reader.AcquireLatestFrame();

            if (frame != null)
            {
                if (_data == null)
                {
                    _data = new Body[_sensor.BodyFrameSource.BodyCount];
                }

                frame.GetAndRefreshBodyData(_data);

                for(int bodyIndex = 0; bodyIndex < bodyCount; bodyIndex++)
                {
                    Body body = _data[bodyIndex];
                    if(body != null)
                    {
                        var trackingId = body.TrackingId;
                        if(trackingId != gestureDetector.TrackingId)
                        {
                            gestureDetector.TrackingId = trackingId;
                            gestureDetector.IsPaused = (trackingId == 0);
                            gestureDetector.OnGestureDetected += CreateOnGestureHandler(bodyIndex);
                        }
                    }
                }

                frame.Dispose();
                frame = null;
            }
        }
    }

    private EventHandler<GestureDetector.GestureDetectorEventArgs> CreateOnGestureHandler(int bodyIndex)
    {
        return (object sender, GestureDetector.GestureDetectorEventArgs e) => OnGestureDetected(sender, e, bodyIndex);
    }

    private void OnGestureDetected(object sender, GestureDetector.GestureDetectorEventArgs e, int bodyIndex)
    {
        var isDetected = e.IsBodyTrackingIdValid && e.IsGestureDetected;

        GestureManager.GESTURENAME recognized = GameManager.Instance.GestureManager.ConvertStringToGestureName(e.GestureID);
        GameManager.Instance.GestureManager.VisualBuilderGestureRecognized(recognized, e.DetectionConfidence);
        /*
        if (e.GestureID == "TipHat")
        {
            ConfidenceUI.text = "Confidence("+(counter++)+"): "+ e.DetectionConfidence;
        }
        //this.bodyText[bodyIndex] = text.ToString();
        */
    }

    void OnApplicationQuit()
    {
        if (_reader != null)
        {
            _reader.Dispose();
            _reader = null;
        }

        if (_sensor.IsOpen)
        {
            _sensor.Close();
        }

        _sensor = null;
    }

    public Body[] GetData()
    {
        return _data;
    }

    public KinectSensor getKinectSensor()
    {
        return _sensor;
    }
}
