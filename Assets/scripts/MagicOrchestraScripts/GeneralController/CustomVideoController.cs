using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class CustomVideoController : MonoBehaviour
{
    // Cameras
    public GameObject frontalCamera;
    public GameObject zenithCamera;
    public GameObject controllerCamera;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(this.VideoCoroutine());

        if (MagicOrchestraBuilderManager.singleton != null)
        {
            MagicOrchestraBuilderManager.singleton.frontalCamera = this.frontalCamera;
            MagicOrchestraBuilderManager.singleton.zenithCamera = this.zenithCamera;
            MagicOrchestraBuilderManager.singleton.controllerCamera = this.controllerCamera;
            MagicOrchestraBuilderManager.singleton.ActivateAllCameras();
        }
    }

     void ReturnToHome(VideoPlayer videoPlayer)
    {
        SceneManager.LoadScene("MagicOrchestra");
    }

    private IEnumerator VideoCoroutine()
    {
        RawImage rawTexture = gameObject.GetComponent<RawImage>();
        VideoPlayer video = gameObject.GetComponent<VideoPlayer>();
        video.Prepare();
        while (!video.isPrepared)
        {
            yield return new WaitForSeconds(0.01f);
        }

        video.loopPointReached += ReturnToHome;
        rawTexture.color = new Color(255, 255, 255);
        rawTexture.texture = video.texture;
        video.Play();

    }
}
