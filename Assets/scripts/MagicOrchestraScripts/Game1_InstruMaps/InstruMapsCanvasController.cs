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

    public void WatchArenaPhase()
    {
        showArenaDispositionButton.GetComponent<Button>().interactable = false;
        userInteractionButton.gameObject.SetActive(true);
        userInteractionButton.GetComponent<Button>().interactable = false;

        InstruMapsController.singleton.WatchArenaPhase();
    }

    public void StartUserPhase()
    {
        showArenaDispositionButton.GetComponent<Button>().interactable = false;
        userInteractionButton.GetComponent<Button>().interactable = false;

        InstruMapsController.singleton.StartUserPhase();
    }

    public void BackToHome()
    {
        SceneManager.LoadScene("MagicOrchestra");
    }

}
