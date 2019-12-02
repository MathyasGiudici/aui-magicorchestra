﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndexesSelecterController : MonoBehaviour
{
    //Singleton of the IndexesSelecterController class
    public static IndexesSelecterController singleton = null;

    private int currentOn;

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
     * Start is called before the first frame update
     * </summary>
     */
    void Start()
    {
        currentOn = 0;
    }

    /* <summary>
     * Update is called once per frame
     * </summary>
     */
    void Update()
    {
        IEnumerable<Toggle> toggles = gameObject.GetComponent<ToggleGroup>().ActiveToggles();

        foreach (Toggle toggle in toggles) {
            this.currentOn = (int) Int32.Parse(toggle.gameObject.name[toggle.gameObject.name.Length - 1].ToString());
        }
    }

    /* <summary>
     * The function returns the game number selected
     * </summary>
     */
    public int GetGameNumber()
    {
        return this.currentOn;
    }
}