using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColourSlider : MonoBehaviour
{
    //Singleton of the ColourSlider class
    public static ColourSlider singleton = null;

    private string currentColor = "000";

    /* <summary>
     * Start is called before the first frame update
     * </summary>
     */
    void Start()
    {
    }

    /* <summary>
     * Update is called once per frame
     * </summary>
     */
    void Update()
    {
        MakeColor(gameObject.GetComponent<Slider>().value);
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

    // Invoked when the value of the slider changes.
    public void MakeColor(float value)
    {
        Color newColor = Color.HSVToRGB(value, 1, 1);
        this.MakeStringColor(newColor);
        gameObject.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = newColor;
        gameObject.transform.GetChild(1).gameObject.GetComponent<Image>().color = newColor;
        Debug.Log(this.GetCurrentColor());
    }

    private void MakeStringColor(Color color)
    {
        this.currentColor = "#" + ColorUtility.ToHtmlStringRGB(color).ToLower();
    }

    public string GetCurrentColor()
    {
        return this.currentColor;
    }
}
