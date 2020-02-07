using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class MagicOrchestraUtils
{
    // Paths
    public static string pathToSpritesInstrumaps = "Sprites" + Path.DirectorySeparatorChar.ToString() + "Instruments" + Path.DirectorySeparatorChar.ToString();
    public static string pathToSpritesInstrumaps_shapes = "Sprites" + Path.DirectorySeparatorChar.ToString() + "Shapes" + Path.DirectorySeparatorChar.ToString();
    public static string pathToSpritesInstrumaps_arena = "Sprites" + Path.DirectorySeparatorChar.ToString() + "Arena" + Path.DirectorySeparatorChar.ToString();

    // Slices Paths
    public static string busyContextSlicePath = pathToSpritesInstrumaps_arena + "busySlice";
    public static string freeContextSlicePath = pathToSpritesInstrumaps_arena + "freeSlice";
    public static string busyNoContextSlicePath = pathToSpritesInstrumaps_arena + "NCBusySlice";
    public static string freeNoContextSlicePath = pathToSpritesInstrumaps_arena + "NCFreeSlice";
    public static string busyContextDoubleSlicePath = pathToSpritesInstrumaps_arena + "busyDoubleSlice";
    public static string freeContextDoubleSlicePath = pathToSpritesInstrumaps_arena + "freeDoubleSlice";
    public static string busyNoContextDoubleSlicePath = pathToSpritesInstrumaps_arena + "NCBusyDoubleSlice";
    public static string freeNoContextDoubleSlicePath = pathToSpritesInstrumaps_arena + "NCFreeDoubleSlice";

    public static string pathToTextOfNumbers = "Audio" + Path.DirectorySeparatorChar.ToString() +"Text" + Path.DirectorySeparatorChar.ToString() + "Numbers" + Path.DirectorySeparatorChar.ToString();
    public static string pathToTextMessages = "Audio" + Path.DirectorySeparatorChar.ToString() + "Text" + Path.DirectorySeparatorChar.ToString() + "Messages" + Path.DirectorySeparatorChar.ToString();

    public static string pathToCorsiSequences = Path.DirectorySeparatorChar.ToString() + "corsi_sequences.csv";
    public static string pathToGame2Sequence = Path.DirectorySeparatorChar.ToString() + "digitspan_sequences.csv";
    public static string pathToInstrumapSequences = Path.DirectorySeparatorChar.ToString() + "objectsPosition.txt";

    // Colors
    public const string backgroundColor = "56122A";
    public const string backgroundColorTextPanel = "93373F";
    public const string primaryColor = "F0A84D";
    public const string secondaryColor = "FFFFFF" ;

    // Times/Pauses
    public const float generalTextTimeShow_long = 5.5f;
    public const float generalPauseTime_short = 1.0f;

    // MESSAGES
    // Generals
    public const string correctSequenceMessage = "Molto bene!\nLa sequenza è corretta!";
    public const string wrongSequenceMessage = "Ops... hai sbagliato!\nRiproviamo?";
    public const string pauseMessage = "Gioco in pausa";
    public const string buttonStopGameMessage = "Metti in pausa";
    public const string buttonResumeGameMessage = "Riprendi a giocare";

    // Game1
    public const string watchArenaMessage = "Ricorda la disposizione dei simboli";
    public const string watchArenaMessage_context = "Ricorda la disposizione\ndegli strumenti nell’orchestra";
    public const string endWatchArenaMessage = "Ti ricordi tutto?";
    public const string correctArenaDisposition = "Perfetto! Ottimo lavoro!";
    public const string reorderObjectsMessage = "Rimetti i simboli al loro posto trascinandoli con la mano";
    public const string reorderObjectsMessage_context = "Rimetti gli strumenti al loro posto trascinandoli con la mano";

    // Game2
    public const string beginGame2SequenceMessage = "Guarda e memorizza\nla sequenza dei numeri";
    public const string beginGame2SequenceMessage_context = "Guarda e memorizza\nla sequenza dei numeri\ndi ogni strumento";
    public const string repeatSequenceMessage = "Ripeti la sequenza appoggiando le carte numerate sulla palla magica";
    public const string repeatSequenceReverseMessage = "Ripeti la sequenza IN ORDINE INVERSO appoggiando le carte numerate sulla palla magica";

    // Game3
    public const string beginCorsiSequenceMessage = "Guarda e memorizza la sequenza\ndei cubi luminosi";
    public const string beginCorsiSequenceMessage_context = "Memorizza la sequenza\ndei cubi luminosi\nche compongono la melodia";
    public const string beginUserTurnMessage = "Ripeti la sequenza\nposizionandoti sopra i cubi\nfinchè non si illuminano";

    /* <summary>
    * Function to stringify a sequence of numbers from an array
    * </summary>
    */
    public static string StringifySequence(int[] sequence)
    {
        string sequenceString = "";
            foreach (int number in sequence)
            sequenceString = sequenceString + number.ToString() + " ";

        return sequenceString;
    }

    /* <summary>
    * Function to stringify in italian second/seconds
    * </summary>
    */
    public static string SecondsTextItalianSuffix(float number)
    {
        if (Mathf.RoundToInt(number) == 1)
            return " secondo";
        else
            return " secondi";
    }

    /* <summary>
    * Function to stringify in italian true/false
    * </summary>
    */
    public static string TrueFalseConverter(bool boolStatus)
    {
        if (boolStatus)
            return "Abilitato";
        else
            return "Disabilitato";
    }

    /* <summary>
    * From a float number return the rounded at .5
    * </summary>
    */
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

    /* <summary>
    * Function to get the nearest skelton
    * </summary>
    */
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

    /* <summary>
    * Function to give a positive feedback with the lights
    * </summary>
    */
    public static void PositiveLightFeedback()
    {
        if(MagicRoomLightManager.instance == null)
        {
            return;
        }

        MagicRoomLightManager.instance.sendColour("#00FF00", 80);
    }

    /* <summary>
    * Function to give a negative feedback with the lights
    * </summary>
    */
    public static void NegativeLightFeedback()
    {
        if (MagicRoomLightManager.instance == null)
        {
            return;
        }

        MagicRoomLightManager.instance.sendColour("#FF0000", 80);
    }

    /* <summary>
    * Function to switch off the lights
    * </summary>
    */
    public static void SwitchOffLightFeedback()
    {
        if (MagicRoomLightManager.instance == null)
        {
            return;
        }

        MagicRoomLightManager.instance.sendColour("#000000", 0);
    }

}
