using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSlider : MonoBehaviour
{
    private float currentTime = 0.0f;

    /* <summary>
     * Start is called before the first frame update
     * </summary>
     */
    void Start()
    {
        this.CreateTimeValue();
    }

    /* <summary>
     * Update is called once per frame
     * </summary>
     */
    void Update()
    {
        this.CreateTimeValue();
    }

    public void CreateTimeValue()
    {
        float timeValue = gameObject.GetComponent<Slider>().value * 5f;
        timeValue = (float) Mathf.Round(timeValue * 100f)/100f;
        this.currentTime = timeValue;
        gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject.GetComponent<Text>().text = timeValue.ToString();
    }
}
