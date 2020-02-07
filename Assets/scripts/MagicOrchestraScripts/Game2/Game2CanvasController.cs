using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game2CanvasController : MonoBehaviour
{
    //Singleton of the CorsiCanvasController class
    public static Game2CanvasController singleton = null;

    public Text parametersSpace;

    public Button showSequenceButton;
    public Button userInteractionButton;

    /* <summary>
     * The function is called when the component is instantiated
     * </summary>
     */
    void Awake()
    {
        //Code to manage the singleton uniqueness
        if ((singleton != null) && (singleton != this))
        {
            Destroy(gameObject);
            return;
        }
        singleton = this;
    }

    /* <summary>
     * Start is called before the first frame update
     * </summary>
     */
    void Start()
    {
        parametersSpace.text = MagicOrchestraParameters.StringifyMe() + Game2Parameters.StringifyMe();
    }

    public void DecisionPoint()
    {
        showSequenceButton.GetComponent<Button>().interactable = true;
        userInteractionButton.GetComponent<Button>().interactable = true;
    }

    public void StartFrontalPhase()
    {
        showSequenceButton.GetComponent<Button>().interactable = false;
        userInteractionButton.gameObject.SetActive(true);
        userInteractionButton.GetComponent<Button>().interactable = false;

        Game2Controller.singleton.StartFrontalPhase();
    }

    public void StartUserPhase()
    {
        showSequenceButton.GetComponent<Button>().interactable = false;
        userInteractionButton.GetComponent<Button>().interactable = false;

        Game2Controller.singleton.StartUserPhase();
    }

    public void BackToHome()
    {    
        MagicOrchestraParameters.GuidedOnPlay = false;
        MagicOrchestraParameters.LastGamePlayed = -1;
        SceneManager.LoadScene("MagicOrchestra");
    }
}

