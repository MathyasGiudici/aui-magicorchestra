using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySliderGame2 : MonoBehaviour
{
    private int currentDifficulty = 1;

    /* <summary>
     * Update is called once per frame
     * </summary>
     */
    void Update()
    {
        float value = gameObject.GetComponent<Slider>().value;
        float newValue = (float)Mathf.Round(value * 10f) / 10f;
        gameObject.GetComponent<Slider>().value = newValue;

        // Range [1,9]
        newValue *= 8;
        newValue = Mathf.RoundToInt(newValue) + 1;
        this.currentDifficulty = (int)newValue;
        gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject.GetComponent<Text>().text = newValue.ToString();

    }

    public int GetCurrentDifficulty()
    {
        return this.currentDifficulty;
    }
}
