using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game2PanelManager : MonoBehaviour
{
    // Public objects needed
    public GameObject togglePrefab = null;
    public GameObject toggleGroup = null;
    public Slider difficultySlider;
    public GameObject contextPanel;

    // Internal seqence
    private List<SequenceObjectFile> sequenceObjectFiles = null;

    /* <summary>
     * Start is called before the first frame update
     * </summary>
     */
    void Start()
    {
        this.LoadSequences();        
    }

    void Update()
    {
        if (MagicOrchestraParameters.IsContext)
        {
            contextPanel.SetActive(true);
        }

        else
        {
            contextPanel.SetActive(false);
        }
    }


    public void LoadSequences()
    {
        if (this.sequenceObjectFiles == null)
        {
            this.sequenceObjectFiles = CsvLoader.LoadingFromFile(MagicOrchestraUtils.pathToGame2Sequence);
        }

        // Getting the sequence panel
        GameObject sequencePanel = gameObject.transform.GetChild(3).gameObject;
        SequenceObjectFile sequenceObjectFile = sequenceObjectFiles[difficultySlider.GetComponent<DifficultySlider>().GetCurrentDifficulty() - difficultySlider.GetComponent<DifficultySlider>().lowDifficulty];


        // Looping on sequences of the target difficulty
        for (int arrayIndex = 0; arrayIndex < sequenceObjectFile.sequences.Count; arrayIndex++)
        {
            // Getting a sequence
            List<int> currentArray = sequenceObjectFile.sequences[arrayIndex];

            if(sequencePanel.transform.childCount != sequenceObjectFile.sequences.Count)
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

    public void GamePressed()
    {
        Game2Parameters.Difficulty = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<DifficultySlider>().GetCurrentDifficulty();
        Game2Parameters.IsReverse = this.gameObject.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<GeneralGameToggle>().GetToggleStatus();
        Game2Parameters.TimeInShowing = this.gameObject.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<TimeSlider>().GetCurrentTime();

        if (MagicOrchestraParameters.IsContext)
        {
            Game2Parameters.IsHintMode = this.gameObject.transform.GetChild(2).GetChild(0).GetChild(1).GetComponent<GeneralGameToggle>().GetToggleStatus();

            if (Game2Parameters.IsHintMode)
                Game2Parameters.IsShuffle = this.gameObject.transform.GetChild(2).GetChild(1).GetChild(1).GetComponent<GeneralGameToggle>().GetToggleStatus();
        }
            


        int arrayIndex = -1;

        foreach (Toggle toggle in toggleGroup.GetComponent<ToggleGroup>().ActiveToggles())
        {
            if (toggle.GetComponent<Toggle>().isOn)
            {
                arrayIndex = toggle.GetComponent<SequenceToggle>().GetIndex();
            }
        }

        if (arrayIndex == -1)
        {
            Debug.Log("Some problems in retriving toggle associated to a sequence");
        }

        Game2Parameters.Sequence = this.sequenceObjectFiles[Game2Parameters.Difficulty - 2].sequences[arrayIndex].ToArray();

        if (MagicOrchestraParameters.GuidedOnPlay)
        {
            CutSceneParameters.TargetVideoIndex = 3;
            SceneManager.LoadScene("BlackCutScenePlayer");
        }
        else
        {
            MagicOrchestraParameters.GuidedOnPlay = false;
            MagicOrchestraParameters.LastGamePlayed = -1;
            SceneManager.LoadScene("Game2");
        }
    }
}
