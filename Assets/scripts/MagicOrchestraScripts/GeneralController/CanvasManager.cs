﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    //Singleton of the CanvasManager class
    public static CanvasManager singleton = null;

    // Cameras
    public GameObject frontalCamera;
    public GameObject zenithCamera;
    public GameObject controllerCamera;

    public Coroutine lightCoroutine = null;

    // Private Light colors
    private string[] lights = { "#EFA84E", "#F06456", "#B24A4A", "#6A1E38", "#F06456" };

    /* <summary>
     * Start is called before the first frame update
     * </summary>
     */
    void Start()
    {
        if (MagicOrchestraBuilderManager.singleton != null)
        {
            MagicOrchestraBuilderManager.singleton.frontalCamera = this.frontalCamera;
            MagicOrchestraBuilderManager.singleton.zenithCamera = this.zenithCamera;
            MagicOrchestraBuilderManager.singleton.controllerCamera = this.controllerCamera;
            MagicOrchestraBuilderManager.singleton.ActivateAllCameras();
        }

        if (MagicOrchestraParameters.GuidedOnPlay)
        {
            if(MagicOrchestraParameters.LastGamePlayed <= 3)
            {
                // format of the panel name: GameXSettingPanel
                MagicOrchestraParameters.IsContext = true;
                this.ShowGameSettingPanel("Game" + (MagicOrchestraParameters.LastGamePlayed + 1) + "SettingPanel");
            }
            else
            {
                Debug.Log("Something is not working");
            }
        }
        else
        {
            ShowIntro();
        }

        this.lightCoroutine = StartCoroutine(CorsiMelodyLights());
    }

    /* <summary>
     * The function is called when the component is instantiated
     * </summary>
     */
    void Awake()
    {
        //Code to manage the singleton uniqueness
        if ((singleton != null) && (singleton != this))
        {
            Destroy(gameObject);
            return;
        }
        singleton = this;
    }


    /* <summary>
     * The function shows the panel of the intro of magic orchestra
     * </summary>
     */
    public void ShowIntro()
    {
        MagicOrchestraParameters.GuidedOnPlay = false;
        MagicOrchestraParameters.LastGamePlayed = -1;
        this.EnablePanel("Intro");
    }

    /* <summary>
     * The function shows the panel of the game selected
     * </summary>
     */
    public void ShowGameSettingPanel(string panelName)
    {
        this.EnablePanel(panelName);
    }

    /* <summary>
    * The function renders a given panel on the UI
    * 
    * <param name="panelName"> It is the name of the panel to show </param>
    * </summary>
    */
    private void EnablePanel(string panelName)
    {
        if (string.IsNullOrWhiteSpace(panelName))
        {
            Debug.Log("Incorret name for CanvasManager:" + panelName);
            return;
        }

        this.DisableAllPanels();
        gameObject.transform.Find(panelName).gameObject.SetActive(true);
    }

    /* <summary>
    * The function disable all the panels of the UI
    * </summary>
    */
    private void DisableAllPanels()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
            gameObject.transform.GetChild(i).gameObject.SetActive(false);

    }

    /* <summary>
    * Light routine of the room
    * </summary>
    */
    private IEnumerator CorsiMelodyLights()
    {
        while (true)
        {
            foreach (string color in this.lights)
            {
                if(MagicRoomLightManager.instance != null)
                    MagicRoomLightManager.instance.sendColour(color, 50);
                yield return new WaitForSeconds(5f);
            }
            
        }
    }

    /* <summary>
    * Stop the light routine of the room
    * </summary>
    */
    public void StopLightRoutine()
    {
        StopCoroutine(this.lightCoroutine);
        if (MagicRoomLightManager.instance != null)
            MagicRoomLightManager.instance.sendColour("#000000", 0);
    }
}
