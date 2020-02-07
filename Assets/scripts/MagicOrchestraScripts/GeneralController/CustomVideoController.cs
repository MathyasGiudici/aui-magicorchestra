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

        this.LoadVideo();
    }

    /* <summary>
     * Function to load the video before the routine
     * </summary>
     */
    private void LoadVideo()
    {
        // Checking if there is a video to play
        if (CutSceneParameters.TargetVideoIndex == -1)
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

        switch (CutSceneParameters.TargetVideoIndex)
        {
            case 0:
                if (MagicOrchestraParameters.IsGuided)
                {
                    CutSceneParameters.TargetVideoIndex = 5;
                    SceneManager.LoadScene("BlackCutScenePlayer");
                }
                else
                {
                    MagicOrchestraParameters.GuidedOnPlay = false;
                    MagicOrchestraParameters.LastGamePlayed = -1;
                    SceneManager.LoadScene("MagicOrchestra");
                }
                break;
            case 1:
                CutSceneParameters.TargetVideoIndex = 2;
                this.LoadVideo();
                break;
            case 2:
                SceneManager.LoadScene("InstruMaps");
                break;
            case 3:
                SceneManager.LoadScene("Game2");
                break;
            case 4:
                SceneManager.LoadScene("CorsiTestScene");
                break;
            case 5:
                MagicOrchestraParameters.GuidedOnPlay = false;
                MagicOrchestraParameters.LastGamePlayed = -1;
                SceneManager.LoadScene("MagicOrchestra");
                break;
            default:
                MagicOrchestraParameters.GuidedOnPlay = false;
                MagicOrchestraParameters.LastGamePlayed = -1;
                SceneManager.LoadScene("MagicOrchestra");
                break;
        }
    }

    /* <summary>
    * Function to select correct light routine
    * </summary>
    */
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

    /* <summary>
    * Light routine for video "corsi_melody"
    * </summary>
    */
    private IEnumerator CorsiMelodyLights()
    {
        yield return new WaitForSeconds(0.5f);
        MagicRoomLightManager.instance.sendColour("#390d16", 100);
        yield return new WaitForSeconds(1f);
        MagicRoomLightManager.instance.sendColour("#581027", 100);
        yield return new WaitForSeconds(1f);
        MagicRoomLightManager.instance.sendColour("#6c1d37", 100);
        yield return new WaitForSeconds(1f);
        MagicRoomLightManager.instance.sendColour("#f1a75d", 100);
        yield return new WaitForSeconds(2f);
        MagicRoomLightManager.instance.sendColour("#000000", 0);
    }
}
