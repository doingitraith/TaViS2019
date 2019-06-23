using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class KinectModelViewer : MonoBehaviour
{
    private bool isActive = true;
    public void ToggleKinectModelCoordinateSystems()
    {
        isActive = !isActive;
        Resources.FindObjectsOfTypeAll<GameObject>().Where(x => x.name.Equals("XAxis")).ToList<GameObject>().ForEach(x => x.SetActive(isActive));
        Resources.FindObjectsOfTypeAll<GameObject>().Where(y => y.name.Equals("YAxis")).ToList<GameObject>().ForEach(y => y.SetActive(isActive));
        Resources.FindObjectsOfTypeAll<GameObject>().Where(z => z.name.Equals("ZAxis")).ToList<GameObject>().ForEach(z => z.SetActive(isActive));
    }
}
