using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class MagicOrchestraUtils
{
    public static string pathToCorsiSequences = Path.DirectorySeparatorChar.ToString() + "corsi_sequences.csv";
    public static string pathToGame2Sequence = Path.DirectorySeparatorChar.ToString() + "digitspan_sequences.csv";

    public const string backgroundColor = "56122A";
    public const string backgroundColorTextPanel = "93373F";
    public const string primaryColor = "F0A84D";
    public const string secondaryColor = "FFFFFF" ;

    public const float generalTextTimeShow_short = 2.0f;
    public const float generalTextTimeShow_long = 4.0f;

    public const float generalPauseTime_short = 2.0f;
    public const float generalPauseTime_long = 4.0f;

    public const string beginSequenceMessage = "Guarda la sequenza attentamente";
    public const string watchArenaMessage = "Osserva attentamente la disposizione degli oggetti!"; //Instrumaps
    public const string endWatchArenaMessage = "Sei stato attento?\nTi ricordi tutto?"; //InstruMaps 
    public const string beginUserTurnMessage = "Ora è il tuo turno!";
    public const string reorderObjectsMessage = "Rimetti gli oggetti al loro posto!";
    public const string repeatSequenceMessage = "Ripeti la sequenza";
    public const string repeatSequenceReverseMessage = repeatSequenceMessage + " IN ORDINE INVERSO";
    public const string correctSequenceMessage = "Bravissimo!\nSequenza corretta";
    public const string wrongSequenceMessage = "Ops... hai sbagliato!";

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

    public static KinectBodySkeleton GetNearestSkeleton()
    {
        if (MagicRoomKinectV2Manager.instance.MagicRoomKinectV2Manager_active)
        {
            // User position part
            Vector3 skelpos = Vector3.zero;

            float minZfounded = 0.0f;
            KinectBodySkeleton minSkel = null;

            foreach (KinectBodySkeleton skel in MagicRoomKinectV2Manager.instance.skeletons)
            {
                // Is the skel valid?
                if (skel != null && skel.SpineBase != Vector3.zero && (skelpos.z == 0 || skelpos.z > skel.SpineBase.z))
                {
                    if(minSkel == null)
                    {
                        minZfounded = skel.SpineBase.z;
                        minSkel = skel;
                    }
                    else
                    {
                        if(skel.SpineBase.z < minZfounded)
                        {
                            minZfounded = skel.SpineBase.z;
                            minSkel = skel;
                        }
                    }
                }
            }

            return minSkel;
        }
        else
        {
            return null;
        }
    }

    public static void PositiveLightFeedback()
    {
        if(MagicRoomLightManager.instance == null)
        {
            return;
        }

        MagicRoomLightManager.instance.sendColour("#00FF00", 80);
    }

    public static void NegativeLightFeedback()
    {
        if (MagicRoomLightManager.instance == null)
        {
            return;
        }

        MagicRoomLightManager.instance.sendColour("#FF0000", 80);
    }

    public static void SwitchOffLightFeedback()
    {
        if (MagicRoomLightManager.instance == null)
        {
            return;
        }

        MagicRoomLightManager.instance.sendColour("#000000", 0);
    }
}
