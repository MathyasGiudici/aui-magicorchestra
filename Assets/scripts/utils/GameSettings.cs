using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameSettings : MonoBehaviour {

    public static GameSettings instance;
    bool isClosing = false, foundfile = false;
    float time = 0;
    public string appliationName = Application.productName;
    string json;
    public GameConfiguration gameconfiguration;
    public ScreenConfiguration screencofiguration;

    //add variables for your game

    // Use this for initialization
    void Awake () {
        for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }
        if (instance == null)
        {
            instance = this;
        }
        else {
            GameObject.DestroyImmediate(this);
        }
        

        //prepare the variables for your game
    }

    /// <summary>
    /// detect when to close the applicaiton
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Logger.addToLogNewLine(appliationName, "Application ending after " + Time.time + " seconds");
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.PageUp))
        {
            if (isClosing)
            {
                Logger.addToLogNewLine(appliationName, "Application ending after " + Time.time + " seconds");
                InformExperienceManagerEndedActivity();
                SceneManager.LoadSceneAsync(0);

                //Application.Quit();
            }
            else
            {
                isClosing = true;
                time = Time.time;
            }
        }
        if (Input.GetKeyDown(KeyCode.PageDown))
        {
            if (isClosing)
            {
                Logger.addToLogNewLine(appliationName, "Application ending after " + Time.time + " seconds");
                InformExperienceManagerEndedActivity();
                SceneManager.LoadSceneAsync(0);
                //Application.Quit();
            }
            else
            {
                isClosing = true;
                time = Time.time;
            }
        }
        if ((Time.time - time) > 1 && isClosing)
        {
            isClosing = false;
        }
    }

    public void InformExperienceManagerEndedActivity()
    {
        ExperienceManagerComunication.instance.SendConcludedCommand();
    }

    /// <summary>
    /// search for the game configuration file
    /// </summary>
    void searchforconfiguration()
    {
        string filepath = Application.persistentDataPath + "/Configuration.dat";
        json = "";
        if (File.Exists(filepath))
        {
            foundfile = true;
            json = File.ReadAllText(filepath);
            if (json != "")
            {
                gameconfiguration = JsonUtility.FromJson<GameConfiguration>(json);
            }
            File.Delete(filepath);
        }
    }

    /// <summary>
    /// search for the screen configuration
    /// </summary>
    void searchforconfigurationLastAdvanced()
    {
        string filepath = Application.persistentDataPath + "/previoussettings.dat";
        json = "";
        if (File.Exists(filepath))
        {
            json = File.ReadAllText(filepath);
        }
        if (json != "")
        {
            ScreenConfiguration c = JsonUtility.FromJson<ScreenConfiguration>(json);
           
            screencofiguration = c;
        }
    }

    /// <summary>
    /// save a new configuration file
    /// </summary>
    public void saveconfigurationLastAdvanced()
    {
        string filepath = Application.persistentDataPath + "/previoussettings.dat";
        ScreenConfiguration c = new ScreenConfiguration();
        
        c = screencofiguration;
        File.WriteAllText(filepath, JsonUtility.ToJson(c));
    }
}

[Serializable]
public class GameConfiguration
{
    //contiene solo variabili pubbliche per le configurazioni del gioco
}

[Serializable]
public class ScreenConfiguration
{
    public float floorsize_X = 2.74f;
    public float floorsize_Y = 2.88f;
    public int frontdisplayidentifier = 1;
    public int floordisplayidentfier = 2;
    public int menudisplayidentifier = 1;
    public float correctionfactorforscreenplacement = 0;
    public float magnfier_X = 9;
    public float magnfier_Y = 15;
    public int brighnessmax = 100;
    public string DBAddress = "http://i3lab.elet.polimi.it/magika/api/";
    public float disallignmentX;
    public float correctionfactorY;
}