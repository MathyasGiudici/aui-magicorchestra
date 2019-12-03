using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContextSliderController : MonoBehaviour
{
    //Singleton of the ContextSliderController class
    public static ContextSliderController singleton = null;

    private bool isContext = false;

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
            this.isContext = true;
        }
        else
        {
            gameObject.GetComponent<Slider>().value = 0;
            this.isContext = false;
        }
    }

    /* <summary>
     * The function returns a value representig if the game must be in context mode
     * </summary>
     */
    public bool GetContextStatus()
    {
        return this.isContext;
    }


}
