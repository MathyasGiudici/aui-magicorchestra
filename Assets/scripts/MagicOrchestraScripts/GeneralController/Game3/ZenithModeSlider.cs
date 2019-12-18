using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZenithModeSlider : MonoBehaviour
{
    //Singleton of the ContextSliderController class
    public static ZenithModeSlider singleton = null;

    private bool isOrthographic = true;

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
            this.isOrthographic = false;
        }
        else
        {
            gameObject.GetComponent<Slider>().value = 0;
            this.isOrthographic = true;
        }
    }

    /* <summary>
     * The function returns a value representig if the game must be in context mode
     * </summary>
     */
    public bool GetOrthographicStatus()
    {
        return this.isOrthographic;
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
