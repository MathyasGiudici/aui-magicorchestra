using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    private bool value = true; 

    /* <summary>
     * Update is called once per frame
     * </summary>
     */
    void Update()
    {
        float value = gameObject.GetComponent<Slider>().value;
        if (value >= 0.5)
        {
            gameObject.GetComponent<Slider>().value = 1;
            this.value = true;
        }
        else
        {
            gameObject.GetComponent<Slider>().value = 0;
            this.value = false;
        }
    }

    /* <summary>
     * The function returns a value representig if the game must be in context mode
     * </summary>
     */
    public bool GetStatus()
    {
        return this.value;
    }

    public void ClickDetection(GameObject slider)
    {
        float value = slider.GetComponent<Slider>().value;
        if (value >= 0.5)
        {
            slider.GetComponent<Slider>().value = 0;
        }
        else
        {
            slider.GetComponent<Slider>().value = 1;
        }
    }
}
