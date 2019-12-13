using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SequenceContextManager : MonoBehaviour
{
    //Singleton of the SequenceContextManager class
    public static SequenceContextManager singleton = null;

    // Background panel to active in case of no context mode
    public GameObject bgPanel;

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
        if (!MagicOrchestraParameters.IsContext)
            RemoveContext();
    }

    /* <summary>
     * Update is called once per frame
     * </summary>
     */
    void Update()
    {
        if (!MagicOrchestraParameters.IsContext)
            RemoveContext();
    }

    /* <summary>
     * Function sets the attribute active of the panel which displayes the number of the sequence
     * </summary>
     */
    public void SetActiveDisplayedNumber(bool isActive)
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(isActive);
    }

    /* <summary>
     * Function to set the number displayed
     * </summary>
     */
    public void ChangeDisplyedNumber(string newNumber)
    {
        gameObject.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = newNumber;
    }

    /* <summary>
     * Function to remove object with a context
     * </summary>
     */
    private void RemoveContext()
    {
        // Showing white background
        bgPanel.SetActive(true);

        // Disabling background
        gameObject.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);

        // Text Color black
        gameObject.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().color = new Color(0, 0, 0);
    }
}
