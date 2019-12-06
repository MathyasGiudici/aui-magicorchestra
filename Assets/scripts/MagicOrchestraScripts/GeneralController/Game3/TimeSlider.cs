using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSlider : MonoBehaviour
{
    private float currentTime = 0.0f;

    /* <summary>
     * Update is called once per frame
     * </summary>
     */
    void Update()
    {
        float value = gameObject.GetComponent<Slider>().value;
        float newValue = (float) Mathf.Round(value * 10f) / 10f;
        gameObject.GetComponent<Slider>().value = newValue;
        newValue *= 5;
        gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject.GetComponent<Text>().text = newValue.ToString();
        this.currentTime = newValue;
    }

    public float GetCurrentTime()
    {
        return this.currentTime;
    }
}
