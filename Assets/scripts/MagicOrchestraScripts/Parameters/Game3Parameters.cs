using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game3Parameters
{
    private static int difficulty;
    private static int[] sequence;
    private static string lightColor;
    private static bool isGestureMode, showPlane, isZenithOrthographic, confirmSound;
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

    public static bool IsZenithOrthographic
    {
        get
        {
            return isZenithOrthographic;
        }
        set
        {
            isZenithOrthographic = value;
        }
    }

    public static bool ConfirmSound
    {
        get
        {
            return confirmSound;
        }
        set
        {
            confirmSound = value;
        }
    }

    public static string StringifyMe()
    {
        string toReturn = "";

        toReturn += ("Difficoltà: " + Difficulty + "\n");
        toReturn += ("Sequenza: " + MagicOrchestraUtils.StringifySequence(Sequence) + "\n");
        toReturn += ("Tempo proiezione: " + TimeInShowing + MagicOrchestraUtils.SecondsTextItalianSuffix(TimeInShowing) + "\n");
        toReturn += ("Modalità gesture: " + MagicOrchestraUtils.TrueFalseConverter(IsGestureMode) + "\n");
        if(!IsGestureMode)
            toReturn += ("Tempo posizionamento: " + TimeInDetecting + MagicOrchestraUtils.SecondsTextItalianSuffix(TimeInDetecting) + "\n");
        if(MagicOrchestraParameters.IsContext)
            toReturn += ("Modalità con piano: " + MagicOrchestraUtils.TrueFalseConverter(ShowPlane) + "\n");
        if (IsZenithOrthographic)
            toReturn += ("Camera zenitale ortonormale\n");
        else
            toReturn += ("Camera zenitale prospettica\n");
        toReturn += ("Suono di conferma: " + MagicOrchestraUtils.TrueFalseConverter(ConfirmSound) + "\n");
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
        Debug.Log("Zenith camera orthographic" + IsZenithOrthographic);
        Debug.Log("Suono di conferma:" + ConfirmSound);
    }
}
