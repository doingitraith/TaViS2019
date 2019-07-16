using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hands : MonoBehaviour
{
    public Image handLeftUi;
    public Image handRightUi;

    public bool isBalance = false;

    Transform handLeft;
    Transform handRight;

    Slider sliderLeft;
    Slider sliderRight;

    UnityEricController eric;

    // Start is called before the first frame update
    void Start()
    {
        eric = GameObject.Find("Eric").GetComponent<UnityEricController>();
        sliderLeft = GameObject.Find("SliderLeft").GetComponent<Slider>();
        sliderRight = GameObject.Find("SliderRight").GetComponent<Slider>();
        handLeft = eric.GetHandLeft();
        handRight = eric.GetHandRight();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBalance)
        {
            handLeft = eric.GetHandLeft();
            handRight = eric.GetHandRight();
            MapToScreenPos();
            MapToSliders();
        }
    }

    void MapToScreenPos()
    {
        Vector3 posLeft = handLeft.position;
        handLeftUi.transform.position = new Vector2(Camera.main.WorldToScreenPoint(posLeft).x, Camera.main.WorldToScreenPoint(posLeft).y);
        Vector3 posRight = handRight.position;
        handRightUi.transform.position = new Vector2(Camera.main.WorldToScreenPoint(posRight).x, Camera.main.WorldToScreenPoint(posRight).y);
        //Debug.Log("Left" + handLeftUi.transform.position.y);
        //Debug.Log("Right" + handRightUi.transform.position.y);
    }

    void MapToSliders()
    {
        //sliderLeft.value = (handLeftUi.transform.position.y + 160) / (320);
        //sliderRight.value = (handRightUi.transform.position.y + 160) / (320);
        //Debug.Log("Hand right: " + handRightUi.transform.position.y+", Hand left: "+ handLeftUi.transform.position.y);
        sliderLeft.value = handLeftUi.transform.position.y - Screen.height/2.0f;
        sliderRight.value = handRightUi.transform.position.y - Screen.height / 2.0f;
        sliderRight.value *= (-1);
        //sliderLeft.value = handLeftUi.transform.position.y - 250;
        //sliderRight.value = handRightUi.transform.position.y - 250f;
    }
}
