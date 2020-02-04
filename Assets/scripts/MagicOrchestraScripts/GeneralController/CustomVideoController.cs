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

    /* <summary>
     * Start is called before the first frame update
     * </summary>
     */
    void Start()
    {
        // MagicOrchestraBuilderManager update of cameras
        if (MagicOrchestraBuilderManager.singleton != null)
        {
            MagicOrchestraBuilderManager.singleton.frontalCamera = this.frontalCamera;
            MagicOrchestraBuilderManager.singleton.zenithCamera = this.zenithCamera;
            MagicOrchestraBuilderManager.singleton.controllerCamera = this.controllerCamera;
            MagicOrchestraBuilderManager.singleton.ActivateAllCameras();
        }

        // Checking if there is a video to play
        if(CutSceneParameters.TargetVideoIndex == -1)
        {
            this.ReturnToHome(null);
        }

        // Loading the Target video 
        VideoPlayer video = gameObject.GetComponent<VideoPlayer>();

        video.source = VideoSource.Url;

        video.url = Application.streamingAssetsPath + "/Videos/" +
                    CutSceneParameters.Videos[CutSceneParameters.TargetVideoIndex] + ".mp4";

        // Starting the Coroutine
        StartCoroutine(this.VideoCoroutine());

        this.CheckingLightRoutine();
    }

    /* <summary>
     * Called before return to MagicOrchestra scene
     * </summary>
     */
    private void ReturnToHome(VideoPlayer videoPlayer)
    {
        // Stopping all the coroutines
        this.StopAllCoroutines();

        // Checking if there are light to switch off
        if (MagicRoomLightManager.instance != null)
            MagicOrchestraUtils.SwitchOffLightFeedback();

        SceneManager.LoadScene("MagicOrchestra");
    }

    /* <summary>
     * Video routine in order to play
     * </summary>
     */
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

    private void CheckingLightRoutine()
    {
        if (MagicRoomLightManager.instance == null)
            return;

        switch (CutSceneParameters.TargetVideoIndex)
        {
            case 0:
                StartCoroutine(this.CorsiMelodyLights());
                break;
            default:
                break;
        }
    }

    private IEnumerator CorsiMelodyLights()
    {
        MagicRoomLightManager.instance.sendColour("#390d16", 90);
        yield return new WaitForSeconds(1f);
        MagicRoomLightManager.instance.sendColour("#581027", 90);
        yield return new WaitForSeconds(1f);
        MagicRoomLightManager.instance.sendColour("#6c1d37", 90);
        yield return new WaitForSeconds(1f);
        MagicRoomLightManager.instance.sendColour("#f1a75d", 90);
        yield return new WaitForSeconds(2f);
        MagicRoomLightManager.instance.sendColour("#000000", 0);
    }
}
