using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSlider : MonoBehaviour
{
    // Internal value of time
    private float currentTime = 0.0f;

    // Time values setting from Unity
    public float lowTime = 1.0f;
    public float highTime = 5.0f;

    /* <summary>
     * Update is called once per frame
     * </summary>
     */
    void Update()
    {
        float value = gameObject.GetComponent<Slider>().value;
        float newValue = (float) Mathf.Round(value * 10f) / 10f;
        gameObject.GetComponent<Slider>().value = newValue;

        // Range [1,5]
        newValue *= (highTime - lowTime);
        newValue += lowTime;

        string toVisualize;
        if (Mathf.RoundToInt(newValue) == 1)
            toVisualize = newValue.ToString() + " secondo";
        else
            toVisualize = newValue.ToString() + " secondi";

        gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject.GetComponent<Text>().text = toVisualize;
        this.currentTime = newValue;
    }

    public float GetCurrentTime()
    {
        return this.currentTime;
    }
}
