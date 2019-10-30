using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Video;

public class VideoLoaderFromFile : MonoBehaviour
{

    public string videoname;
    public string prefixvideoURL;
    public string prefixvideoURLLUDOMI;
    public static VideoLoaderFromFile instance;
    public VideoPlayer player;
    public Camera[] cameras;
    bool screen, paused;
    bool islocalfileplayed;

    // Use this for initialization
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }

        prefixvideoURL = Application.streamingAssetsPath + "/video/";
        prefixvideoURLLUDOMI = "C:\\LUDOMI\\Resources\\videos/";
    }

    public void playvideo(string url, bool screen, bool paused)
    {
        islocalfileplayed = true;
        string completeurl = "";
        videoname = url;
        this.screen = screen;
        this.paused = paused;
        if (url.StartsWith("file://"))
        {
            videoname = url.Substring(7);
        }
        if (videoname.StartsWith("C://"))
        {
            completeurl = videoname;
        }
        else
        {
            completeurl = prefixvideoURL + videoname;
            if (!File.Exists(completeurl)) {
                Debug.Log("Missing video in the Streaming assets");
                completeurl = prefixvideoURLLUDOMI + videoname;
                if (!File.Exists(completeurl)) {
                    Debug.Log("Missing video in the folders");
                    return;
                }
            }
        }
        if (screen)
        {
            player.targetCamera = cameras[0];
        }
        else
        {
            player.targetCamera = cameras[1];
        }
        player.GetComponent<AudioSource>().volume = 1;
        player.url = completeurl;
        player.loopPointReached += EndReached;
        Debug.Log("Play");
        player.Play();
    }

    private void EndReached(VideoPlayer vPlayer)
    {
        Debug.Log("Stop");
        islocalfileplayed = false;
        if (paused)
        {
            player.Pause();
        }
        else
        {
            player.Stop();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.PageUp) && islocalfileplayed) {
            player.Stop();
        }
    }

    public void StopVideo() {
        /*player.GetComponent<AudioSource>().volume = 0;
        player.Stop();*/
    }
}