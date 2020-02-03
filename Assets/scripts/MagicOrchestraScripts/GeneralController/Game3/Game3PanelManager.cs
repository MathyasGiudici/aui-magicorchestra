using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game3PanelManager : MonoBehaviour
{
    public GameObject togglePrefab = null;
    public GameObject toggleGroup = null;

    private List<SequenceObjectFile> sequenceObjectFiles = null;

    /* <summary>
     * Start is called before the first frame update
     * </summary>
     */
    void Start()
    {
        this.EnableFirstPanel();
    }

    private void OnEnable()
    {
        this.EnableFirstPanel();
    }

    public void FirstPanelGamePressed()
    {
        // Setting paramteres
        Game3Parameters.Difficulty      = this.GetChildElement(0).GetComponent<DifficultySlider>().GetCurrentDifficulty();
        Game3Parameters.TimeInShowing   = this.GetChildElement(1).GetComponent<TimeSlider>().GetCurrentTime();
        Game3Parameters.LightColor      = this.GetChildElement(2).GetComponent<ColourSlider>().GetCurrentColor();
        Game3Parameters.IsGestureMode   = false;
        Game3Parameters.TimeInDetecting = this.GetChildElement(3).GetComponent<TimeSlider>().GetCurrentTime();
        Game3Parameters.ShowPlane       = this.GetChildElement(4).GetComponent<GeneralGameToggle>().GetToggleStatus();
        Game3Parameters.IsZenithOrthographic = ZenithModeSlider.singleton.GetOrthographicStatus();

        this.EnableSecondPanel();
        this.LoadSequences();
    }

    private void LoadSequences()
    {
        if (this.sequenceObjectFiles == null)
        {
            this.sequenceObjectFiles = CsvLoader.LoadingFromFile(MagicOrchestraUtils.pathToCorsiSequences);
        }

        // Getting the sequence panel
        GameObject sequencePanel = gameObject.transform.GetChild(1).transform.GetChild(0).gameObject;
        SequenceObjectFile sequenceObjectFile = sequenceObjectFiles[Game3Parameters.Difficulty - 2];

        // Looping on sequences of the target difficulty
        for (int arrayIndex = 0; arrayIndex < sequenceObjectFile.sequences.Count; arrayIndex++)
        {
            // Getting a sequence
            List<int> currentArray = sequenceObjectFile.sequences[arrayIndex];

            if (sequencePanel.transform.childCount != sequenceObjectFile.sequences.Count)
            {
                // Creating a new toggle
                GameObject toggle = Instantiate(togglePrefab, sequencePanel.transform, false);
                toggle.transform.SetParent(sequencePanel.transform);
                toggle.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                toggle.GetComponent<SequenceToggle>().SetIndex(arrayIndex);

                // Setting toggle group
                toggleGroup.GetComponent<ToggleGroup>().RegisterToggle(toggle.GetComponent<Toggle>());
                toggle.GetComponent<Toggle>().group = toggleGroup.GetComponent<ToggleGroup>();

                // In order to leave only one toggle on
                if (arrayIndex != 0)
                {
                    toggle.GetComponent<Toggle>().isOn = false;
                }

                // Creating the visual sequence
                string stringToWrite = "";
                foreach (int number in currentArray)
                    stringToWrite = stringToWrite + number.ToString() + " ";
                toggle.transform.GetChild(1).gameObject.GetComponent<Text>().text = stringToWrite;
            }
            else
            {
                // Creating the visual sequence
                string stringToWrite = "";
                foreach (int number in currentArray)
                    stringToWrite = stringToWrite + number.ToString() + " ";
                sequencePanel.transform.GetChild(arrayIndex).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = stringToWrite;
            }
        }
    }

    public void SecondPanelGamePressed()
    {
        int arrayIndex = -1;

        foreach (Toggle toggle in toggleGroup.GetComponent<ToggleGroup>().ActiveToggles())
        {
            if(toggle.GetComponent<Toggle>().isOn)
            {
                arrayIndex = toggle.GetComponent<SequenceToggle>().GetIndex();
            }
        }

        if( arrayIndex == -1)
        {
            Debug.Log("Some problems in retriving toggle associated to a sequence");
        }

        Game3Parameters.Sequence = this.sequenceObjectFiles[Game3Parameters.Difficulty - 2].sequences[arrayIndex].ToArray();

        SceneManager.LoadScene("CorsiTestScene");
    }

    public void BackToFirstPanel()
    {
        this.EnableFirstPanel();
    }

    private void EnableFirstPanel()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
    }

    private void EnableSecondPanel()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
    }

    private GameObject GetChildElement(int index)
    {
        return gameObject.transform.GetChild(0).transform.GetChild(index).transform.GetChild(1).gameObject;
    }
}