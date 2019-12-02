using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContextSliderController : MonoBehaviour
{
    //Singleton of the ContextSliderController class
    public static ContextSliderController singleton = null;

    private bool isContext = true;

    public GameObject gameSelectionPanel;

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
        this.ShowContextMenu();
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
            this.isContext = false;
            this.ShowNoContextMenu();
        }
        else
        {
            gameObject.GetComponent<Slider>().value = 0;
            this.isContext = true;
            this.ShowContextMenu();
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

    /* <summary>
     * The function prepares the panel in case of context mode
     * </summary>
     */
    private void ShowContextMenu()
    {
        //Configuring the panel
        //Enabling the slider
        gameSelectionPanel.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        //Disabling the toggles
        gameSelectionPanel.gameObject.transform.GetChild(1).gameObject.SetActive(false);
    }

    /* <summary>
     * The function prepares the panel in case of NO context mode
     * </summary>
     */
    private void ShowNoContextMenu()
    {
        //Configuring the panel
        //Enabling the slider
        gameSelectionPanel.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        //Disabling the toggles
        gameSelectionPanel.gameObject.transform.GetChild(1).gameObject.SetActive(true);
    }
}
