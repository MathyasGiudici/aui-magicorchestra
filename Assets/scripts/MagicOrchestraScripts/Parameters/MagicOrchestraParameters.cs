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

    public static string StringifyMe()
    {
        string toReturn = "";

        toReturn += "Magic Orchestra in ";
        if (IsContext)
        {
            toReturn += "Modalità contesto";
            if (isGuided)
                toReturn += " guidata\n";
            else
                toReturn += "\n";
        }
        else
            toReturn += "Modalità non contesto\n";

        return toReturn;
    }

    public static void LogMe()
    {
        Debug.Log("IsContext: " + IsContext);
        Debug.Log("IsGuided: " + IsGuided);
        Debug.Log("GameNumber: " + GameNumber);        
    }
}
