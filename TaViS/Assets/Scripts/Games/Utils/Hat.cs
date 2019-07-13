using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hat : PickableObject
{
    // Start is called before the first frame update
    void Start()
    {
        parentToTarget = true;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
}
