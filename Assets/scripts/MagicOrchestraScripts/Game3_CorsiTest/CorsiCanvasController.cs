using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CorsiCanvasController : MonoBehaviour
{
    //Singleton of the CorsiCanvasController class
    public static CorsiCanvasController singleton = null;

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
        parametersSpace.text = Game3Parameters.StringifyMe(); 
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

        CorsiController.singleton.StartFrontalPhase();
    }

    public void StartUserPhase()
    {
        showSequenceButton.GetComponent<Button>().interactable = false;
        userInteractionButton.GetComponent<Button>().interactable = false;

        CorsiController.singleton.StartUserPhase();
    }

    public void BackToHome()
    {
        SceneManager.LoadScene("MagicOrchestra");
    }
}
