using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MagicOrchestraParameters
{
    private static bool isContext, isGuided;
    private static int gameNumber;

    public static bool IsContext
    {
        get
        {
            return isContext;
        }
        set
        {
            isContext = value;
        }
    }

    public static bool IsGuided
    {
        get
        {
            return isGuided;
        }
        set
        {
            isGuided = value;
        }
    }

    public static int GameNumber
    {
        get
        {
            return gameNumber;
        }
        set
        {
            gameNumber = value;
        }
    }



    public static class Game3Settings
    { 
        // private static int[] sequence;
        private static string lightColor;
        private static bool isGestureMode;
        private static float timeInShowing, timeInDetecting;


        public static string LightColor
        {
            get
            {
                return lightColor;
            }
            set
            {
                lightColor = value;
            }
        }
    }
}
