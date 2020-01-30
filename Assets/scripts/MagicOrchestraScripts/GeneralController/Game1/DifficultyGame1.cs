using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyGame1 : MonoBehaviour
{
    // Internal value of difficulty
    private int currentDifficulty = 2;

    // Difficulty values setting from Unity
    public int lowDifficulty = 2;
    public int highDifficulty = 12;

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

        int correct = (int)newValue;

        if (correct == 5 || correct == 9)
            correct++;

        this.currentDifficulty = correct;
        gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject.GetComponent<Text>().text = correct.ToString();

    }

    public int GetCurrentDifficulty()
    {
        return this.currentDifficulty;
    }
}
