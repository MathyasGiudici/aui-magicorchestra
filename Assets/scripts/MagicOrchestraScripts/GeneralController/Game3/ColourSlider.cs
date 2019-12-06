using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColourSlider : MonoBehaviour
{
    private string currentColor = "000";

    /* <summary>
     * Update is called once per frame
     * </summary>
     */
    void Update()
    {
        Color newColor = Color.HSVToRGB(gameObject.GetComponent<Slider>().value, 1, 1);
        this.MakeStringColor(newColor);
        gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = newColor;
        gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = newColor;
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
