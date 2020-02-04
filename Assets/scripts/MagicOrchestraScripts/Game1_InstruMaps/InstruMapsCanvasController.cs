using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InstruMapsCanvasController : MonoBehaviour
{
    public Text parametersSpace;

    public Button showArenaDispositionButton;
    public Button userInteractionButton;

    // To distinguish between "pause" and "play"
    public bool isGameStarted = false;

    // To distinguish between "play for the first time" or "resume"
    public bool isPause = false;

    // Singleton of the InstruMapsController class
    public static InstruMapsCanvasController singleton = null;

    /* <summary>
     * The function is called when the component is instantiated
     * </summary>
     */
    private void Awake()
    {
        if ((singleton != null) && (singleton != this))
        {
            Destroy(this);
            return;
        }
        singleton = this;
    }


    // Start is called before the first frame update
    void Start() 
    {
        if (Game1Parameters.Difficulty != 0)
            parametersSpace.text = MagicOrchestraParameters.StringifyMe() + Game1Parameters.StringifyMe();
    }

    /// <summary>
    /// After showing for a while the arena disposition to the user we can decide to re-show it or 
    /// start the task
    /// </summary>
    public void DecisionPoint()
    {
        showArenaDispositionButton.GetComponent<Button>().interactable = true;
        userInteractionButton.GetComponent<Button>().interactable = true;
    }

    /// <summary>
    /// Allow to watch the arena disposition, at the beginning and also during the execution of the game
    /// </summary>
    public void WatchArenaPhase()
    {
        showArenaDispositionButton.GetComponent<Button>().interactable = false;
        //userInteractionButton.gameObject.SetActive(true);
        userInteractionButton.GetComponent<Button>().interactable = false;

        InstruMapsController.singleton.WatchArenaPhase();
    }

    /// <summary>
    /// Decision about the funcion of the "play "button, because it can stop or resume the game
    /// </summary>
    public void OnUserPhase()
    {
        if (this.isGameStarted)
        {
            PauseGame();
            this.isGameStarted = false;
        }
        else
        {
            StartUserPhase();
            this.isGameStarted = true;
        }
    }

    /// <summary>
    /// Start to play and allow the user to interact
    /// </summary>
    public void StartUserPhase()
    {
        showArenaDispositionButton.GetComponent<Button>().interactable = false;
        userInteractionButton.GetComponent<Button>().interactable = false;

        userInteractionButton.transform.GetChild(0).GetComponent<Text>().text = MagicOrchestraUtils.buttonStopGameMessage;
        userInteractionButton.GetComponent<Button>().interactable = true;

        if (!this.isPause)
        {
            this.isPause = true;
            InstruMapsController.singleton.StartUserPhase();
        }
        else
        {
            InstruMapsController.singleton.ResumeGame();
        }
    }

    /// <summary>
    /// Stop the user's interactions to allow the therapist to rewatch the arena disposition (also partial)
    /// </summary>
    public void PauseGame()
    {
        userInteractionButton.GetComponent<Button>().interactable = false;
        userInteractionButton.transform.GetChild(0).GetComponent<Text>().text = MagicOrchestraUtils.buttonResumeGameMessage;

        showArenaDispositionButton.GetComponent<Button>().interactable = true;
        userInteractionButton.GetComponent<Button>().interactable = true;

        InstruMapsController.singleton.PauseGame();
    }

    /// <summary>
    /// Allows to return to the main menù
    /// </summary>
    public void BackToHome()
    {
        SceneManager.LoadScene("MagicOrchestra");
    }

    /// <summary>
    /// Disables all buttons, for example just before the BackToHome
    /// </summary>
    public void DisableAllButtons()
    {
        showArenaDispositionButton.GetComponent<Button>().interactable = false;
        userInteractionButton.GetComponent<Button>().interactable = false;
    }
}
