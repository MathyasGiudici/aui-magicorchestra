using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game3Parameters
{
    private static int difficulty;
    private static int[] sequence;
    private static string lightColor;
    private static bool isGestureMode;
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

    public static string StringifyMe()
    {
        string toReturn = "";

        toReturn += ("Sequenza: " + MagicOrchestraUtils.StringifySequence(Sequence) + "\n");
        toReturn += ("Difficoltà: " + Difficulty + "\n");
        toReturn += ("Tempo proiezione frontale: " + TimeInShowing + " secondi\n");
        // toReturn += ("Colore: " + LightColor + "\n");
        toReturn += ("Modalità Gesture: " + IsGestureMode + "\n");
        toReturn += ("Tempo di riconoscimento: " + TimeInDetecting + " secondi\n");

        return toReturn;
    }


    public static void LogMe()
    {
        Debug.Log("Sequenza: " + MagicOrchestraUtils.StringifySequence(Sequence));
        Debug.Log("Difficulty: " + Difficulty);
        Debug.Log("TimeInShowing: " + TimeInShowing);
        Debug.Log("LightColor: " + LightColor);
        Debug.Log("IsGestureMode: " + IsGestureMode);
        Debug.Log("TimeInDetecting: " + TimeInDetecting);
    }
}
