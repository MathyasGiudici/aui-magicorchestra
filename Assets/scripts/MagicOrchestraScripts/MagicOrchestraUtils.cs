using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MagicOrchestraUtils
{
    public const string pathToCorsiSequences = "/corsi_sequences.csv";
    public const string pathToGame2Sequence = "/digitspan_sequences.csv";

    public const float generalTextTimeShow_short = 2.0f;
    public const float generalTextTimeShow_long = 4.0f;

    public const float generalPauseTime_short = 2.0f;
    public const float generalPauseTime_long = 4.0f;

    public static void LoggerOfObjects(Object targetObject)
    {
        var output = JsonUtility.ToJson(targetObject, true);
        Debug.Log(output);
    }

    public static string StringifySequence(int[] sequence)
    {
        string sequenceString = "";
        foreach (int number in sequence)
            sequenceString = sequenceString + number.ToString() + " ";

        return sequenceString;
    }
}
