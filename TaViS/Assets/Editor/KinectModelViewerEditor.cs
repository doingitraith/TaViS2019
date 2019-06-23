using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(KinectModelViewer))]
public class KinectModelViewerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        KinectModelViewer myTarget = (KinectModelViewer)target;

        if (GUILayout.Button("Toggle Kinect Model Coordinate Systems"))
            myTarget.ToggleKinectModelCoordinateSystems();
    }
}
