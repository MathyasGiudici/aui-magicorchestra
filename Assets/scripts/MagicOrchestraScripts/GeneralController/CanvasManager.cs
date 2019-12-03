using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    //Singleton of the CanvasManager class
    public static CanvasManager singleton = null;


    /* <summary>
     * Start is called before the first frame update
     * </summary>
     */
    void Start()
    {
        //ShowIntro();
    }

    /* <summary>
     * Update is called once per frame
     * </summary>
     */
    void Update()
    {

    }

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
     * The function shows the panel of the intro of magic orchestra
     * </summary>
     */
    public void ShowIntro()
    {
        this.EnablePanel("Intro");
    }

    /* <summary>
     * The function shows the panel of the Game3 Parameters
     * </summary>
     */
    public void ShowGameSettingPanel(string panelName)
    {
        this.EnablePanel(panelName);
    }

    /* <summary>
    * The function renders a given panel on the UI
    * 
    * <param name="panelName"> It is the name of the panel to show </param>
    * </summary>
    */
    private void EnablePanel(string panelName)
    {
        if (string.IsNullOrWhiteSpace(panelName))
        {
            Debug.Log("Incorret name for CanvasManager:" + panelName);
            return;
        }

        this.DisableAllPanels();
        gameObject.transform.Find(panelName).gameObject.SetActive(true);
    }

    /* <summary>
    * The function disable all the panels of the UI
    * </summary>
    */
    private void DisableAllPanels()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
            gameObject.transform.GetChild(i).gameObject.SetActive(false);

    }
}
