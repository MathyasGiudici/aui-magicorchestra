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
        Game3Parameters.IsGestureMode   = this.GetChildElement(3).GetComponent<GestureToggle>().GetGestureMode();
        Game3Parameters.TimeInDetecting = this.GetChildElement(4).GetComponent<TimeSlider>().GetCurrentTime();;

        this.EnableSecondPanel();
        this.LoadSequences();
    }

    private void LoadSequences()
    {
        this.LoadingFromFile();

        // Getting the sequence panel
        GameObject sequencePanel = gameObject.transform.GetChild(1).transform.GetChild(0).gameObject;
        SequenceObjectFile sequenceObjectFile = sequenceObjectFiles[Game3Parameters.Difficulty - 2];

        // Looping on sequences of the target difficulty
        for (int arrayIndex = 0; arrayIndex < sequenceObjectFile.sequences.Count; arrayIndex++)
        {
            // Getting a sequence
            List<int> currentArray = sequenceObjectFile.sequences[arrayIndex];

            // Creating a new toggle
            GameObject toggle = Instantiate(togglePrefab);
            toggle.transform.SetParent(sequencePanel.transform);
            toggle.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            toggle.GetComponent<SequenceToggle>().SetIndex(arrayIndex);

            // Setting toggle group
            toggleGroup.GetComponent<ToggleGroup>().RegisterToggle(toggle.GetComponent<Toggle>());
            toggle.GetComponent<Toggle>().group = toggleGroup.GetComponent<ToggleGroup>();

            // In order to leave only one toggle on
            if( arrayIndex != 0)
            {
                toggle.GetComponent<Toggle>().isOn = false;
            }

            // Creating the visual sequence
            string stringToWrite = "";
            foreach(int number in currentArray)
                stringToWrite = stringToWrite + number.ToString() + " ";
            toggle.transform.GetChild(1).gameObject.GetComponent<Text>().text = stringToWrite;
        }
    }

    private void LoadingFromFile()
    {
        if( this.sequenceObjectFiles != null)
        {
            // Parsing already done
            return;
        }

        // Instanciting the sequences list
        this.sequenceObjectFiles = new List<SequenceObjectFile>();

        // Loading the sequence file
        string path = Application.streamingAssetsPath + MagicOrchestraUtils.pathToCorsiSequences;
        string cvsFile = File.ReadAllText(path);
        String[] lines = cvsFile.Split("\n"[0]);

        // Looping on lines of the file
        // KEEP ATTENTION:
        // - line starts from 1 in order to skip the header line
        // - line arrives before lines.Lenght - 1 because the last line is empty
        for(int line=1; line < lines.Length -1; line++)
        {
            // Parsing the file removing the separetors
            String[] lineData = lines[line].Split(","[0]);

            // Creating the Unity Object representing the sequences
            SequenceObjectFile objectFile = new SequenceObjectFile();
            objectFile.sequences = new List<List<int>>();

            // Converting the difficulty
            objectFile.difficulty = int.Parse(lineData[0]);

            // Retriving all the sequences of the target difficulty
            for(int seqID=1 ; seqID < lineData.Length ; seqID++)
            {
                // Creating the list of a given sequence
                List<int> seqList = new List<int>();

                // Looping on chars to add numbers in the list
                foreach (char singleChar in lineData[seqID])
                {
                    if (singleChar != " "[0])
                        seqList.Add(int.Parse(singleChar.ToString()));
                }

                // Adding the sequence to the sequences' list
                objectFile.sequences.Add(seqList);
            }

            // Adding the Object in the Class list
            this.sequenceObjectFiles.Add(objectFile);
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

public class SequenceObjectFile
{
    public int difficulty;
    public List<List<int>> sequences;
}