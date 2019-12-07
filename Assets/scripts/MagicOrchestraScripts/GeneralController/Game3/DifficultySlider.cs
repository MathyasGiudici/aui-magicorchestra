using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySlider : MonoBehaviour
{
    private int currentDifficulty = 2;

    /* <summary>
     * Update is called once per frame
     * </summary>
     */
    void Update()
    {
        float value = gameObject.GetComponent<Slider>().value;
        float newValue = (float)Mathf.Round(value * 10f) / 10f;
        gameObject.GetComponent<Slider>().value = newValue;

        // Range [2,9]
        newValue *= 7;
        newValue = Mathf.RoundToInt(newValue) + 2;
        this.currentDifficulty = (int) newValue;
        gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject.GetComponent<Text>().text = newValue.ToString();

    }

    public int GetCurrentDifficulty()
    {
        return this.currentDifficulty;
    }
}
