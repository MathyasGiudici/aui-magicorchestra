using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Game3PanelManager : MonoBehaviour
{
    public GameObject togglePrefab = null;

    private List<List<int>> sequences = null;

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
        Game3Parameters.IsGestureMode   = this.GetChildElement(3).GetComponent<GestureToggle>().GetGestureMode();
        Game3Parameters.TimeInDetecting = this.GetChildElement(4).GetComponent<TimeSlider>().GetCurrentTime();

        // TODO: Parameter log for debug
        Game3Parameters.LogMe();

        this.EnableSecondPanel();
        this.LoadSequence();
    }

    private void LoadSequence()
    {
        // Loading the sequence file
        string path = Application.streamingAssetsPath + MagicOrchestraUtils.pathToCorsiSequences + Game3Parameters.Difficulty.ToString() + ".json";
        string jsonToString = File.ReadAllText(path);
        CorsiSequenceObject corsiObj = JsonUtility.FromJson<CorsiSequenceObject>(jsonToString);

        Debug.Log("dif:" + corsiObj.difficulty);
        Debug.Log("seq:" + corsiObj.sequences);
        // Saving the reference
        this.sequences = corsiObj.sequences;

        // Getting the sequence panel
        GameObject sequencePanel = gameObject.transform.GetChild(1).transform.GetChild(0).gameObject;

        // Looping on sequences
        for(int arrayIndex = 0; arrayIndex < corsiObj.sequences.Count; arrayIndex++)
        {
            int[] currentArray = corsiObj.sequences.ToArray()[arrayIndex].ToArray();

            // Creating a new toogle
            GameObject toggle = Instantiate(togglePrefab);
            toggle.transform.SetParent(sequencePanel.transform);
            toggle.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            toggle.GetComponent<SequenceToggle>().SetIndex(arrayIndex);

            string stringToWrite = "";
            // Creating the visual sequence

            for(int elemIndex=0; elemIndex < currentArray.Length; elemIndex++)
            {
                stringToWrite += currentArray[elemIndex].ToString() + " - ";
            }
            stringToWrite = stringToWrite.Remove(stringToWrite.Length - 3);
            toggle.transform.GetChild(1).gameObject.GetComponent<Text>().text = stringToWrite;
        }
    }

    public void SecondPanelGamePressed()
    {
        

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

[Serializable]
public class CorsiSequenceObject
{
    public int difficulty;
    public List<List<int>> sequences;
}
