using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUtils : MonoBehaviour
{
    bool shouldMirrorCamera = false;
    bool shouldRotateByDegree = false;
    bool shouldShowFront = false;
    bool shouldShowBack = false;

    bool firstToThirdTransition = false;
    bool thirdToFirstTransition = false;
    bool startThirdToFirstTransition = false;

    float rotated = 0;
    float degreeToRotate = 0;
    float distanceToPlayer = 0.0f;
    float fov = 0f;
    public float rotationStep = 1.2f;
    public float firstPersonOffset = 0.1f;
    public float firstPersonFoV = 10f;
    public float firstPersonTransitionSpeed = 8f;
    public float firstPersonIgnoreDist = 0.2f;

    Matrix4x4 projectionMat;
    Transform player = null;

    Vector3 frontPosition = Vector3.forward;
    Vector3 backPosition = Vector3.forward;

    GameObject playerBody = null;
    GameObject frontView = null;
    GameObject backView = null;
    GameObject head = null;

    int rotationDir = -1;

    public enum FACING
    {
        FRONT,
        BACK
    }

    public enum PERSPECTIVE
    {
        FIRST,
        THIRD
    }

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GameObject.FindGameObjectWithTag("Player");
        player = GameObject.Find("CameraTarget").transform;
        frontView = GameObject.Find("CameraFrontPosition");
        backView = GameObject.Find("CameraBackPosition");
        head = GameObject.FindGameObjectWithTag("1stPerson");

        fov = gameObject.GetComponent<Camera>().fieldOfView;

        UpdateFrontPos();
        UpdateBackPos();

        distanceToPlayer = Mathf.Abs(transform.position.z - player.transform.position.z);
        projectionMat = Camera.main.projectionMatrix;
        MirrorCamera(shouldMirrorCamera);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (shouldRotateByDegree)
        {
            Rotate(degreeToRotate);
        }
        else if (shouldShowBack || shouldShowFront)
        {
            Rotate(360);
        }


        if (firstToThirdTransition)
        {
            GoToBackPos();
        }

        if (startThirdToFirstTransition)
        {
            GoToHeadPos();
        }
    }

    void GoToHeadPos()
    {
        Vector3 end = new Vector3(head.transform.position.x, head.transform.position.y + firstPersonOffset, head.transform.position.z);
        if (transform.position == end)
        {
            transform.SetParent(head.transform);
            gameObject.GetComponent<Camera>().fieldOfView = firstPersonFoV;
            thirdToFirstTransition = false;
            startThirdToFirstTransition = false;
            SetPlayerLayer(playerBody.transform, 0);
        }
        else
        {
            //hide eyes etc
            if (Vector3.Magnitude(head.transform.position - transform.position) < firstPersonIgnoreDist)
            {
                foreach (Transform child in playerBody.transform)
                {
                    SetPlayerLayer(playerBody.transform, 9);
                }
            }
            transform.position = Vector3.Lerp(transform.position, end, Time.deltaTime * firstPersonTransitionSpeed);
        }
    }

    void SetPlayerLayer(Transform bodyPart, int layer)
    {

        bodyPart.gameObject.layer = layer;
        foreach (Transform child in bodyPart)
        {
            child.gameObject.layer = layer;
            if (child.childCount > 0)
            {
                SetPlayerLayer(child.transform, layer);
            }
        }

    }

    void GoToBackPos()
    {
        if (transform.position == backPosition)
        {
            firstToThirdTransition = false;
            GoToFrontOrBackView(FACING.FRONT);
        }
        else
        {
            UpdateBackPos();
            transform.position = Vector3.Lerp(transform.position, backPosition, Time.deltaTime * firstPersonTransitionSpeed);
        }
    }

    public void MirrorCamera(bool shouldMirrorCamera)
    {
        if (shouldMirrorCamera)
        {
            Matrix4x4 mat = Camera.main.projectionMatrix;
            mat *= Matrix4x4.Scale(new Vector3(-1, 1, 1));
            Camera.main.projectionMatrix = mat;
            shouldMirrorCamera = true;
        }
        else
        {
            Camera.main.projectionMatrix = projectionMat;
            shouldMirrorCamera = false;
        }
    }

    void UpdateBackPos()
    {
        backPosition = backView.transform.position;
    }

    void UpdateFrontPos()
    {
        frontPosition = frontView.transform.position;
    }

    void Rotate(float degree)
    {
        UpdateBackPos();
        UpdateFrontPos();
        if (shouldShowFront)
        {
            if (transform.position == frontPosition)
            {
                StopRotating();
            }
        }

        if (shouldShowBack)
        {
            if (transform.position == backPosition)
            {
                if (thirdToFirstTransition)
                {
                    startThirdToFirstTransition = true;
                }
                StopRotating();
            }
        }

        if (rotated / degree < 1)
        {
            rotated += rotationStep;
            transform.RotateAround(player.transform.position, rotationDir * Vector3.up, rotationStep);
        }
        else
        {
            StopRotating();
        }
    }

    void StopRotating()
    {
        shouldRotateByDegree = false;
        shouldShowBack = false;
        shouldShowFront = false;
        rotated = 0;
    }

    public void RotateByXDegreeAroundPlayer(float degree)
    {
        degreeToRotate = degree;
        shouldRotateByDegree = true;
    }

    public void GoToFrontOrBackView(FACING facing)
    {
        switch (facing)
        {
            case FACING.FRONT:
                shouldShowFront = true;
                break;
            case FACING.BACK:
                shouldShowBack = true;
                break;
        }
    }

    public void TogglePerspective(PERSPECTIVE perspective)
    {
        if (perspective == PERSPECTIVE.FIRST)
        {
            thirdToFirstTransition = true;
            GoToFrontOrBackView(FACING.BACK);
        }
        else
        {
            if (transform.parent != null)
            {
                transform.parent = null;
            }
            thirdToFirstTransition = false;
            UpdateBackPos();
            gameObject.GetComponent<Camera>().fieldOfView = fov;
            transform.rotation = backView.transform.rotation;
            firstToThirdTransition = true;
        }
    }


    void OnPreRender()
    {
        if (shouldMirrorCamera)
        {
            GL.invertCulling = true;
        }
    }

    void OnPostRender()
    {
        if (shouldMirrorCamera)
        {
            GL.invertCulling = false;
        }
    }
}
