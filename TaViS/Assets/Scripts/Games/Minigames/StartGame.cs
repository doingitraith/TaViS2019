using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MiniGame
{
    public GameObject UI;
    public GameObject parent;
    public TakePhoto takePhoto;
    public RetakePhoto retakePhoto;

    public RawImage cameraView;
    public RawImage photoDisplay;

    private void Start()
    {
        hasCheckpoints = false;
        Id = GameID.GAME_ID.START;
        UI.SetActive(true);
    }

    void Update()
    {
        cameraView.texture = GameManager.Instance.colorTexture;
        
        // Photo taken
        if(takePhoto.isPhotoTaken)
        {
            takePhoto.isPhotoTaken = false;
            retakePhoto.isPhotoTaken = false;
            photoDisplay.gameObject.SetActive(true);
            photoDisplay.texture = new Texture2D(cameraView.texture.width, cameraView.texture.height, TextureFormat.ARGB32, false);
            Graphics.CopyTexture(cameraView.texture, photoDisplay.texture);
            retakePhoto.gameObject.SetActive(true);
        }
        // Retake photo taken
        if(retakePhoto.isPhotoTaken)
        {
            retakePhoto.isPhotoTaken = false;
            takePhoto.isPhotoTaken = false;
            photoDisplay.gameObject.SetActive(true);
            photoDisplay.texture = cameraView.texture;
            photoDisplay.texture = new Texture2D(cameraView.texture.width, cameraView.texture.height, TextureFormat.ARGB32, false);
            Graphics.CopyTexture(cameraView.texture, photoDisplay.texture);
        }
        if(takePhoto.isPhotoAccepted)
        {
            takePhoto.isPhotoAccepted = false;
            takePhoto.isPhotoTaken = false;
            retakePhoto.isPhotoTaken = false;
            GameManager.Instance.takenPicure = photoDisplay.texture as Texture2D;

            GameManager.Instance.MiniGameManager.TriggerGestureResult(
                new GestureEvaluationResult(GestureManager.GESTURENAME.TAKE_PHOTO,
                                            GestureEvaluationResult.GESTURE_PERFORMANCE.NONE));
        }

    }

    public override void OnGameFailed()
    {
        base.OnGameFailed();
        throw new System.NotImplementedException();
    }

    public override void OnGameFinished()
    {
        base.OnGameFinished();
        takePhoto.gameObject.SetActive(false);
        retakePhoto.gameObject.SetActive(false);
        UI.SetActive(false);
        parent.SetActive(false);
        //throw new System.NotImplementedException();
    }

    public override void OnGameStarted()
    {
        base.OnGameStarted();
        retakePhoto.gameObject.SetActive(false);
        takePhoto.gameObject.SetActive(true);
        photoDisplay.gameObject.SetActive(false);
        //GameManager.Instance.GestureManager.LoadGameGestures(GameID.GAME_ID.DANCE);
        //GameManager.Instance.GestureManager.StartDetecting();
        //throw new System.NotImplementedException();
    }

    public override void ResetGame()
    {
        throw new System.NotImplementedException();
    }

}
