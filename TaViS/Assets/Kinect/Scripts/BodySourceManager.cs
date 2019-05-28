using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class BodySourceManager : MonoBehaviour
{

    private KinectSensor _sensor;
    private BodyFrameReader _reader;
    private Body[] _data;

    // Use this for initialization
    void Start()
    {
        _sensor = KinectSensor.GetDefault();

        if (_sensor != null)
        {
            _reader = _sensor.BodyFrameSource.OpenReader();

            if (!_sensor.IsOpen)
            {
                _sensor.Open();
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

                frame.Dispose();
                frame = null;
            }
        }
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
}
