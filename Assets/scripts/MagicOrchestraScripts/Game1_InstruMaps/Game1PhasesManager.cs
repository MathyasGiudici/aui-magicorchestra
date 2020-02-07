using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game1PhasesManager : MonoBehaviour
{
    // Cameras
    public GameObject frontalCamera;
    public GameObject zenithCamera;
    public GameObject controllerCamera;

    //Text container
    public GameObject panelMessage;
    public GameObject frontalTextMessage;

    private bool isArenaPresent = false;

    private int score = 0;

    public Coroutine coroutine = null;

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


    void Start()
    {
        if (MagicOrchestraBuilderManager.singleton != null)
        {
            MagicOrchestraBuilderManager.singleton.frontalCamera = this.frontalCamera;
            MagicOrchestraBuilderManager.singleton.zenithCamera = this.zenithCamera;
            MagicOrchestraBuilderManager.singleton.controllerCamera = this.controllerCamera;
            MagicOrchestraBuilderManager.singleton.ActivateAllCameras();
        }
    }

    /// <summary>
    /// It activste the coroutine used to show the arena disposition, which is the final disposition 
    /// that the user has to replicate
    /// </summary>
    /// <param name="numItems"> Number of items depending on the chosen difficulty </param>
    public void ShowArenaDisposition()
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
        this.EnableRaycaster();
    }

    /// <summary>
    /// Stop the game for a while
    /// </summary>
    public void PauseGame()
    {
        this.DisableRaycaster();
        this.DisableCollisionDetection();

        // Showing the Pause panel
        panelMessage.SetActive(true);
        frontalTextMessage.GetComponent<Text>().text = MagicOrchestraUtils.pauseMessage;
        frontalTextMessage.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(MagicOrchestraUtils.pathToTextMessages + "pauseMessage");
        frontalTextMessage.GetComponent<AudioSource>().Play();
    }


    public void ResumeGame()
    {
        // Showing the Pause panel 
        frontalTextMessage.GetComponent<Text>().text = "";
        panelMessage.SetActive(false);

        this.EnableRaycaster();
        this.ActiveCollisionDetection();
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
    /// Activate the detection of collisions between slices and objects and assignment of the target slice to the game object
    /// </summary>
    private void DisableCollisionDetection()
    {
        CollisionDetector script;

        foreach (ObjectSliceCouple couple in ArenaObjectsHandler.singleton.objectSliceCouples)
        {
            script = (CollisionDetector)couple.arenaObject.GetComponent(typeof(CollisionDetector));
            script.DisableCollisionDetector();
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
            this.DisableRaycaster();
            this.coroutine = StartCoroutine(this.FinalCoroutine());
        }
    }

    /* 
     * ***********************************************************************************************************************************************
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *  COROUTINES * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     * ***********************************************************************************************************************************************
    */
    /// <summary>
    /// Courutine of the first panel that tells to show the object disposition in the arena.
    /// </summary>
    /// <returns></returns>
    IEnumerator WatchDispositionCoroutine()
    {
        panelMessage.SetActive(true);

        // Showing the Starting Panel with the instructions
        if (MagicOrchestraParameters.IsContext)
        {
            frontalTextMessage.GetComponent<Text>().text = MagicOrchestraUtils.watchArenaMessage_context;
            frontalTextMessage.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(MagicOrchestraUtils.pathToTextMessages + "watchArenaMessage_context");
            frontalTextMessage.GetComponent<AudioSource>().Play();
        }    
        else
        {
            frontalTextMessage.GetComponent<Text>().text = MagicOrchestraUtils.watchArenaMessage;
            frontalTextMessage.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(MagicOrchestraUtils.pathToTextMessages + "watchArenaMessage");
            frontalTextMessage.GetComponent<AudioSource>().Play();
        }

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
        yield return new WaitForSeconds((float)Game1Parameters.TimeInShowing);

        panelMessage.SetActive(true);
        frontalTextMessage.GetComponent<Text>().text = MagicOrchestraUtils.endWatchArenaMessage;
        frontalTextMessage.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(MagicOrchestraUtils.pathToTextMessages + "endWatchArenaMessage");
        frontalTextMessage.GetComponent<AudioSource>().Play();
        ArenaObjectsHandler.singleton.SetDragAndDropPositions();

        yield return new WaitForSeconds(MagicOrchestraUtils.generalTextTimeShow_long);

        // Showing thr arena disposition that the user has to remember
        panelMessage.SetActive(false);
        frontalTextMessage.GetComponent<Text>().text = "";

        InstruMapsController.singleton.EndWatchArenaPhase();
    }

    /// <summary>
    /// Corotine of the user phase, when it has to start playing
    /// </summary>
    /// <returns></returns>
    IEnumerator ReorderItemsCoroutine()
    {
        panelMessage.SetActive(true);

        // Showing the Starting Panel with the instructions
        if (MagicOrchestraParameters.IsContext)
        {
            frontalTextMessage.GetComponent<Text>().text = MagicOrchestraUtils.reorderObjectsMessage_context;
            frontalTextMessage.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(MagicOrchestraUtils.pathToTextMessages + "reorderObjectsMessage_context");
            frontalTextMessage.GetComponent<AudioSource>().Play();
        } 
        else
        {
            frontalTextMessage.GetComponent<Text>().text = MagicOrchestraUtils.reorderObjectsMessage;
            frontalTextMessage.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(MagicOrchestraUtils.pathToTextMessages + "reorderObjectsMessage");
            frontalTextMessage.GetComponent<AudioSource>().Play();
        }
        yield return new WaitForSeconds(MagicOrchestraUtils.generalTextTimeShow_long);

        // Showing thr arena disposition that the user has to remember
        panelMessage.SetActive(false);
        frontalTextMessage.GetComponent<Text>().text = "";

        yield return new WaitForEndOfFrame();
    }

    /// <summary>
    /// Coroutine of end game, after the completion of the task
    /// </summary>
    /// <returns></returns>
    IEnumerator FinalCoroutine()
    {
        InstruMapsCanvasController.singleton.DisableAllButtons();

        panelMessage.SetActive(true);
        frontalTextMessage.GetComponent<Text>().text = MagicOrchestraUtils.correctArenaDisposition;
        frontalTextMessage.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(MagicOrchestraUtils.pathToTextMessages + "correctArenaDisposition");
        frontalTextMessage.GetComponent<AudioSource>().Play();
        MagicOrchestraUtils.PositiveLightFeedback();
        yield return new WaitForSeconds(MagicOrchestraUtils.generalTextTimeShow_long);
        panelMessage.SetActive(false);
        frontalTextMessage.GetComponent<Text>().text = "";
        yield return new WaitForSeconds(MagicOrchestraUtils.generalPauseTime_short);
        MagicOrchestraUtils.SwitchOffLightFeedback();

        InstruMapsController.singleton.EndGame();
        StopClassCoroutine();
    }

    /* ***********************************************************************************************************************************************
     * ***********************************************************************************************************************************************
    */


    /// <summary>
    /// Enable raycaster in the different game mode
    /// </summary>
    private void EnableRaycaster()
    {
        if (DragAndDropHandler.singleton != null)
        {
            DragAndDropHandler.singleton.EnableRaycaster();
        }
        
        if (KinectLeftHand.singleton != null && KinectRightHand.singleton != null)
        {
            KinectLeftHand.singleton.EnableRaycaster();
            KinectRightHand.singleton.EnableRaycaster();
        }
    }


    /// <summary>
    /// Disable raycaster in the different game mode
    /// </summary>
    private void DisableRaycaster()
    {
        if (DragAndDropHandler.singleton != null)
        {
            DragAndDropHandler.singleton.DisableRaycaster();
        }

        if (KinectLeftHand.singleton != null && KinectRightHand.singleton != null)
        {
            KinectLeftHand.singleton.DisableRaycaster();
            KinectRightHand.singleton.DisableRaycaster();
        }
    }

    /* <summary>
     * StopClassCoroutine stops the corutine instanciated by this class
     * </summary>
     */
    private void StopClassCoroutine()
    {
        StopCoroutine(this.coroutine);
    }

}
