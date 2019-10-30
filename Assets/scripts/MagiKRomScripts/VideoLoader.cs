using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class VideoLoader : MonoBehaviour {
    public static VideoLoader instance;
    public string path;
    //public string url = "file:///C:/Video/";
    public string fileName = "video.ogv";

    RawImage player;
    AudioSource sound;
    MovieTexture movie;

    // Use this for initialization
    void Start () {
        if (instance == null)
        {
            instance = this;
        }
        else {
            Destroy(this);
        }
        path = Application.streamingAssetsPath + "/video";
        fileName = "video.ogv";
        player = GetComponent<RawImage>();
        sound = GetComponent<AudioSource>();

        StartCoroutine(loadMovie());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setMovie(string movieName)
    {
        fileName = movieName;
        StartCoroutine(loadMovie());
    }
    IEnumerator loadMovie()
    {
        Debug.Log(path + "/" + fileName);
        Debug.Log(File.Exists(path + "/" + fileName));
        WWW www = new WWW(path + "/" + fileName);
        while (!www.isDone) {
            yield return null;
        }
        Debug.Log(www.GetMovieTexture().duration + " <--- wwww");

        if (www.error != null)
        {
            Debug.Log("Error: Can't laod movie! - " + www.error);
            yield break;

        }
        else
        {
            MovieTexture video = www.GetMovieTexture() as MovieTexture;
            Debug.Log("Movie loaded");
            Debug.Log(www.GetMovieTexture());
            movie = video;
            setMovie();
            playMovie();
        }
    }


    public void setMovie()
    {
        Debug.Log(movie.name + " ------");
        player.texture = movie;
        sound.clip = movie.audioClip;
    }

    public void playMovie()
    {
        movie.Play();
        sound.Play();
    }

    public void pauseMovie()
    {
        movie.Pause();
        sound.Pause();
    }
}
