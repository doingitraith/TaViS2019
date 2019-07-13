using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Apple : MonoBehaviour
{
    Rigidbody apple;
  
    // Start is called before the first frame update
    void Start()
    {
        //initial random velocity of the apple, so player has to balance it out
        apple = GetComponent<Rigidbody>();
        float x = Random.Range(0.0f, 2.0f);
        float y = Random.Range(0.0f, 2.0f);
        //float z = Random.Range(0.0f, 0.5f);

        apple.velocity = new Vector3(x, y, apple.velocity.z);

    }

    // Update is called once per frame
    void Update()
    {


    }


}
