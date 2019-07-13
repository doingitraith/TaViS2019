using UnityEngine;
using System.Collections;
using Windows.Kinect;
using System;
using UnityEngine.UI;
using Microsoft.Kinect.Face;

public class BodySourceManager : MonoBehaviour
{

    private KinectSensor _sensor;
    private BodyFrameReader _reader;
    private Body[] _data;
    private GestureDetector gestureDetector;
    private int bodyCount;
    private int counter = 0;

    private FaceFrameSource _faceSource;
    private FaceFrameReader _faceReader;

    //private ColorFrameSource _colorSource;
    private ColorFrameReader _colorReader;
    private byte[] colorData;

    public Text FaceUI;

    // Use this for initialization
    void Awake()
    {
        _sensor = KinectSensor.GetDefault();

        if (_sensor != null)
        {
            _reader = _sensor.BodyFrameSource.OpenReader();
            bodyCount = _sensor.BodyFrameSource.BodyCount;

            _faceSource = FaceFrameSource.Create(_sensor, 0, FaceFrameFeatures.Glasses);
            _faceReader = _faceSource.OpenReader();

            //_faceReader.FrameArrived += FaceReader_FrameArrived;

            _colorReader = _sensor.ColorFrameSource.OpenReader();
            FrameDescription colorFrameDescription = _sensor.ColorFrameSource.CreateFrameDescription(ColorImageFormat.Rgba);
            colorData = new byte[colorFrameDescription.BytesPerPixel * colorFrameDescription.LengthInPixels];
            GameManager.Instance.colorTexture = new Texture2D(colorFrameDescription.Width,
                colorFrameDescription.Height,TextureFormat.RGBA32, false);

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
                    if(body != null && body.IsTracked)
                    {
                        var trackingId = body.TrackingId;
                        if(!_faceSource.IsTrackingIdValid)
                            _faceSource.TrackingId = trackingId;

                        if(trackingId != gestureDetector.TrackingId)
                        {
                            gestureDetector.TrackingId = trackingId;
                            gestureDetector.IsPaused = (trackingId == 0);
                            //gestureDetector.OnGestureDetected += CreateOnGestureHandler(bodyIndex);
                        }
                    }
                }

                frame.Dispose();
                frame = null;
            }
        }
        if(_faceReader != null)
        {
            var frame = _faceReader.AcquireLatestFrame();
            if (frame != null)
            {
                FaceFrameResult result = frame.FaceFrameResult;
                if (result != null)
                {
                    var glasses = result.FaceProperties[FaceProperty.WearingGlasses];
                    //Debug.Log("Glasses: " + glasses);
                    GameManager.Instance.MiniGameManager.GlassesDetected(glasses);
                }
                
                frame.Dispose();
                frame = null;
            }
        }
        if(_colorReader != null)
        {
            var frame = _colorReader.AcquireLatestFrame();
            if(frame != null)
            {
                frame.CopyConvertedFrameDataToArray(colorData, ColorImageFormat.Rgba);
                GameManager.Instance.colorTexture.LoadRawTextureData(colorData);
                GameManager.Instance.colorTexture.Apply();

                frame.Dispose();
                frame = null;
            }
        }
    }

    private void FaceReader_FrameArrived(object sender, FaceFrameArrivedEventArgs e)
    {
        using (var frame = e.FrameReference.AcquireFrame())
        {
            if(frame != null)
            {
                FaceFrameResult result = frame.FaceFrameResult;
                if(result != null)
                {
                    var glasses = result.FaceProperties[FaceProperty.WearingGlasses];
                    Debug.Log("Glasses: " + glasses);
                    GameManager.Instance.MiniGameManager.GlassesDetected(glasses);
                }
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
        if (e.DetectionConfidence < GameManager.Instance.GestureManager.confidenceThresholdBad ||
            !GameManager.Instance.GestureManager.isDetecting)
        {
            return;
        }
        GestureManager.GESTURENAME recognized = GameManager.Instance.GestureManager.ConvertStringToGestureName(e.GestureID);
        GameManager.Instance.GestureManager.VisualBuilderGestureRecognized(recognized, e.DetectionConfidence);
        
        /*
        if (e.GestureID == "TipHat")
        {
            FaceUI.text = "Confidence("+(counter++)+"): "+ e.DetectionConfidence;
        }
        */
        
        //this.bodyText[bodyIndex] = text.ToString();
        
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
