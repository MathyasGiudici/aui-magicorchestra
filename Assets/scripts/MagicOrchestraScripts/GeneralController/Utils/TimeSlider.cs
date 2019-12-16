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
        // Parsing value
        float value = gameObject.GetComponent<Slider>().value;
        value *= (highTime - lowTime);
        value += lowTime;
        float newValue = MagicOrchestraUtils.FiveMathRounder(value);

        // Texting value
        string toVisualize = newValue.ToString() + MagicOrchestraUtils.SecondsTextItalianSuffix(newValue);

        gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject.GetComponent<Text>().text = toVisualize;

        // Saving value
        this.currentTime = newValue;


        // Correcting the slider
        newValue -= lowTime;
        newValue /= (highTime - lowTime);
        gameObject.GetComponent<Slider>().value = newValue;
    }

    public float GetCurrentTime()
    {
        return this.currentTime;
    }
}
