using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CutSceneParameters {

    private static List<string> videos = new List<string>{ "corsi_melody" };
    private static int targetVideoIndex = -1;

    public static List<string> Videos
    {
        get
        {
            return videos;
        }
    }

    public static int TargetVideoIndex
    {
        get
        {
            return targetVideoIndex;
        }
        set
        {
            targetVideoIndex = value;
        }
    }

}
