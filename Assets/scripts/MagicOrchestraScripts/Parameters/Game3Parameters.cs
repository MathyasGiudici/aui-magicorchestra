using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game3Parameters
{
    private static int difficulty;
    private static int[] sequence;
    private static string lightColor;
    private static bool isGestureMode, showPlane;
    private static float timeInShowing, timeInDetecting;

    public static int Difficulty
    {
        get
        {
            return difficulty;
        }
        set
        {
            difficulty = value;
        }
    }

    public static float TimeInShowing
    {
        get
        {
            return timeInShowing;
        }
        set
        {
            timeInShowing = value;
        }
    }

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

    public static bool IsGestureMode
    {
        get
        {
            return isGestureMode;
        }
        set
        {
            isGestureMode = value;
        }
    }

    public static float TimeInDetecting
    {
        get
        {
            return timeInDetecting;
        }
        set
        {
            timeInDetecting = value;
        }
    }

    public static int[] Sequence
    {
        get
        {
            return sequence;
        }
        set
        {
            sequence = value;
        }
    }

    public static bool ShowPlane
    {
        get
        {
            return showPlane;
        }
        set
        {
            showPlane = value;
        }
    }

    public static string StringifyMe()
    {
        string toReturn = "";

        toReturn += ("Difficoltà: " + Difficulty + "\n");
        toReturn += ("Sequenza: " + MagicOrchestraUtils.StringifySequence(Sequence) + "\n");
        toReturn += ("Tempo proiezione: " + TimeInShowing + MagicOrchestraUtils.SecondsTextItalianSuffix(TimeInShowing));
        toReturn += ("Modalità Gesture: " + MagicOrchestraUtils.TrueFalseConverter(IsGestureMode) + "\n");
        if(!IsGestureMode)
            toReturn += ("Tempo posizionamento: " + TimeInDetecting + MagicOrchestraUtils.SecondsTextItalianSuffix(TimeInDetecting));
        if(MagicOrchestraParameters.IsContext)
            toReturn += ("Modalità con piano: " + MagicOrchestraUtils.TrueFalseConverter(ShowPlane) + "\n");

        return toReturn;
    }


    public static void LogMe(bool isFirstPanel)
    {
        if(!isFirstPanel)
            Debug.Log("Sequenza: " + MagicOrchestraUtils.StringifySequence(Sequence));

        Debug.Log("Difficulty: " + Difficulty);
        Debug.Log("TimeInShowing: " + TimeInShowing);
        Debug.Log("LightColor: " + LightColor);
        Debug.Log("IsGestureMode: " + IsGestureMode);
        Debug.Log("TimeInDetecting: " + TimeInDetecting);
        Debug.Log("ShowPlane: " + ShowPlane);
    }
}
