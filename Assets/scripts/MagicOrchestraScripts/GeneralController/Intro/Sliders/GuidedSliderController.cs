using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuidedSliderController : MonoBehaviour
{
    //Singleton of the IndexGuidedSliderController class
    public static GuidedSliderController singleton = null;

    private bool isGuided = true;

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
        if (ContextSliderController.singleton.GetContextStatus())
        {
            GuidedSliderController.singleton.gameObject.SetActive(true);
        }
        else
        {
            GuidedSliderController.singleton.gameObject.SetActive(false);
            return;
        }
        float value = gameObject.GetComponent<Slider>().value;
        if (value >= 0.5)
        {
            gameObject.GetComponent<Slider>().value = 1;
            this.isGuided = false;
        }
        else
        {
            gameObject.GetComponent<Slider>().value = 0;
            this.isGuided = true;
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
}
