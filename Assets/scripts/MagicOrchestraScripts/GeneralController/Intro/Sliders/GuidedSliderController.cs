using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuidedSliderController : MonoBehaviour
{
    //Singleton of the GuidedSliderController class
    public static GuidedSliderController singleton = null;

    private bool isGuided = false;

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
     * Update is called once per frame
     * </summary>
     */
    void Update()
    {
        float value = gameObject.GetComponent<Slider>().value;
        if (value >= 0.5)
        {
            gameObject.GetComponent<Slider>().value = 1;
            this.isGuided = true;
        }
        else
        {
            gameObject.GetComponent<Slider>().value = 0;
            this.isGuided = false;
        }
    }

    /* <summary>
     * The function returns a value representig if the game must be played in guided mode
     * </summary>
     */
    public bool GetGuidedStatus()
    {
        return this.isGuided;
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
