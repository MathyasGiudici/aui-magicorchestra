using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game1PhasesManager : MonoBehaviour
{
    //Text container
    public GameObject panelMessage;
    public GameObject frontalTextMessage;

    private bool isArenaPresent = false;

    private int score = 0;


    //Singleton of the Game1PhasesManager class
    public static Game1PhasesManager singleton = null;

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

    /// <summary>
    /// It activste the coroutine used to show the arena disposition, which is the final disposition 
    /// that the user has to replicate
    /// </summary>
    /// <param name="numItems"> Number of items depending on the chosen difficulty </param>
    public void ShowArenaDisposition(int numItems)
    {
        StartCoroutine(WatchDispositionCoroutine());
    }


    /// <summary>
    /// It activate the courutine 
    /// </summary>
    public void ReorderItems()
    {
        StartCoroutine(ReorderItemsCoroutine());

        this.ActiveCollisionDetection();
        DragAndDropHandler.singleton.EnableRaycaster();
    }

    /// <summary>
    /// Activate the detection of collisions between slices and objects and assignment of the target slice to the game object
    /// </summary>
    private void ActiveCollisionDetection()
    {
        CollisionDetector script;

        foreach (ObjectSliceCouple couple in ArenaObjectsHandler.singleton.objectSliceCouples)
        {
            script = (CollisionDetector)couple.arenaObject.GetComponent(typeof(CollisionDetector));
            script.AssignTheTargetSlice();
            script.EnableCollisionDetector();
        }
    }

    /// <summary>
    /// Update the current score until the user entirely complete the task
    /// </summary>
    public void UpdateCurrentScore()
    {
        this.score++;

        if (this.score == Game1Parameters.Difficulty)
        {
            // TODO: Schermata vittoria! 
        }
    }


    /// <summary>
    /// Courutine of the first panel that tells to show the object disposition in the arena.
    /// </summary>
    /// <returns></returns>
    IEnumerator WatchDispositionCoroutine()
    {
        panelMessage.transform.GetChild(1).gameObject.SetActive(true);

        // Showing the Starting Panel with the instructions
        frontalTextMessage.GetComponent<Text>().text = MagicOrchestraUtils.watchArenaMessage;
        panelMessage.SetActive(true);

        if (!this.isArenaPresent)
        {
            ArenaSetup.singleton.CreateArena(Game1Parameters.Difficulty);
            this.isArenaPresent = true;
        }
        else
        {
            ArenaObjectsHandler.singleton.SetArenaPositions();
        }
        yield return new WaitForSeconds(MagicOrchestraUtils.generalTextTimeShow_long);
        
        // Showing thr arena disposition that the user has to remember
        panelMessage.SetActive(false);
        frontalTextMessage.GetComponent<Text>().text = "";
        //yield return new WaitForSeconds((float)Game1Parameters.TimeInShowing);
        yield return new WaitForSeconds(2f);

        frontalTextMessage.GetComponent<Text>().text = MagicOrchestraUtils.endWatchArenaMessage;
        panelMessage.SetActive(true);
        ArenaObjectsHandler.singleton.SetDragAndDropPositions();

        yield return new WaitForSeconds(MagicOrchestraUtils.generalTextTimeShow_long);

        // Showing thr arena disposition that the user has to remember
        panelMessage.transform.GetChild(1).gameObject.SetActive(false);
        frontalTextMessage.GetComponent<Text>().text = "";

        InstruMapsController.singleton.EndWatchArenaPhase();
    }


    IEnumerator ReorderItemsCoroutine()
    {
        panelMessage.transform.GetChild(1).gameObject.SetActive(true);

        // Showing the Starting Panel with the instructions
        frontalTextMessage.GetComponent<Text>().text = MagicOrchestraUtils.reorderObjectsMessage;
        panelMessage.SetActive(true);
        yield return new WaitForSeconds(MagicOrchestraUtils.generalTextTimeShow_long);

        // Showing thr arena disposition that the user has to remember
        panelMessage.SetActive(false);
        frontalTextMessage.GetComponent<Text>().text = "";

        yield return new WaitForEndOfFrame();
    }

}
