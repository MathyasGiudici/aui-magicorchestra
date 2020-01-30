using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyWarning : MonoBehaviour
{
    // Reference to the object
    public GameObject difficultyObject = null;
    public GameObject warningPanel = null;
    public Button playButton = null;

    private List<int> forbiddenD = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        this.forbiddenD.Add(5);
        this.forbiddenD.Add(9);
        this.CheckLevel();
    }

    // Update is called once per frame
    void Update()
    {
        this.CheckLevel();
    }

    private void CheckLevel()
    {
        if (this.forbiddenD.IndexOf(difficultyObject.GetComponent<DifficultySlider>().GetCurrentDifficulty()) != -1)
        {
            warningPanel.SetActive(true);
            playButton.interactable = false;
        }
        else
        {
            warningPanel.SetActive(false);
            playButton.interactable = true;
        }
    }
}
