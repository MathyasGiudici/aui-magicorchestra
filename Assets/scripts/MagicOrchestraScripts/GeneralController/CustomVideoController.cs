using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class CustomVideoController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(this.VideoCoroutine());
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
