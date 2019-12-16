using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MagicOrchestraUtils
{
    public const string pathToCorsiSequences = "/corsi_sequences.csv";
    public const string pathToGame2Sequence = "/digitspan_sequences.csv";

    public const string backgroundColor = "56122A";
    public const string backgroundColorTextPanel = "93373F";
    public const string primaryColor = "F0A84D";
    public const string secondaryColor = "FFFFFF" ;

    public const float generalTextTimeShow_short = 2.0f;
    public const float generalTextTimeShow_long = 4.0f;

    public const float generalPauseTime_short = 2.0f;
    public const float generalPauseTime_long = 4.0f;

    public static string StringifySequence(int[] sequence)
    {
        string sequenceString = "";
            foreach (int number in sequence)
            sequenceString = sequenceString + number.ToString() + " ";

        return sequenceString;
    }

    public static string SecondsTextItalianSuffix(float number)
    {
        if (Mathf.RoundToInt(number) == 1)
            return " secondo";
        else
            return " secondi";
    }

    public static string TrueFalseConverter(bool boolStatus)
    {
        if (boolStatus)
            return "Abilitato";
        else
            return "Disabilitato";
    }

    public static float FiveMathRounder(float number)
    {
        const float multiple = 0.5f;
        double less;
        double more;

        less = System.Math.Floor(number / multiple) * multiple;
        more = System.Math.Ceiling(number / multiple) * multiple;

        if (System.Math.Abs(less - number) < System.Math.Abs(more - less))
        {
            return (float)less;
        }
        else
        {
            return (float)more;
        }
    }
}
