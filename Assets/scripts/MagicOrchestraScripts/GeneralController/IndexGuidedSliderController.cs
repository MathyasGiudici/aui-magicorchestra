using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndexGuidedSliderController : MonoBehaviour
{
    //Singleton of the IndexGuidedSliderController class
    public static IndexGuidedSliderController singleton = null;

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
     * Start is called before the first frame update
     * </summary>
     */
    void Start()
    {
        this.ChangeSubMenuStatus(false);
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
            this.isGuided = false;
            this.ChangeSubMenuStatus(true);
        }
        else
        {
            gameObject.GetComponent<Slider>().value = 0;
            this.isGuided = true;
            this.ChangeSubMenuStatus(false);
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

    /* <summary>
     * The function shows the indexes of the games
     * </summary>
     */
    private void ChangeSubMenuStatus(bool status)
    {
        gameObject.transform.parent.GetChild(1).gameObject.SetActive(status);
    }
}
