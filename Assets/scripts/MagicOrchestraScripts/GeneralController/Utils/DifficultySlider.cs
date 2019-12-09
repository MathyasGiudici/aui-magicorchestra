using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySlider : MonoBehaviour
{
    // Internal value of difficulty
    private int currentDifficulty = 2;

    // Difficulty values setting from Unity
    public int lowDifficulty = 2;
    public int highDifficulty = 9;

    /* <summary>
     * Update is called once per frame
     * </summary>
     */
    void Update()
    {
        float value = gameObject.GetComponent<Slider>().value;
        float newValue = (float)Mathf.Round(value * 10f) / 10f;
        gameObject.GetComponent<Slider>().value = newValue;

        // Ranging the value
        newValue *= (highDifficulty - lowDifficulty);
        newValue = Mathf.RoundToInt(newValue) + lowDifficulty;
        this.currentDifficulty = (int) newValue;
        gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject.GetComponent<Text>().text = newValue.ToString();

    }

    public int GetCurrentDifficulty()
    {
        return this.currentDifficulty;
    }
}
