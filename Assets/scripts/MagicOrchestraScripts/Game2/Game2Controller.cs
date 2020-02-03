using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game2Controller : MonoBehaviour
{
    //Singleton of the Game1PhasesManager class
    public static Game2Controller singleton = null;

    // Cameras
    public GameObject frontalCamera;
    public GameObject zenithCamera;
    public GameObject controllerCamera;

    //Private setting variables
    private int[] sequence = { 6, 1, 9, 4, 7, 3 };
    private float timeInShowing = 3.0f;
    private bool isReverse = false;

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
        if (Game2Parameters.Difficulty == 0)
        {
            return;
        }
        this.sequence = Game2Parameters.Sequence;
        this.timeInShowing = Game2Parameters.TimeInShowing;
        this.isReverse = Game2Parameters.IsReverse;
        Game2HitImageController.singleton.Init();

        if (MagicOrchestraBuilderManager.singleton != null)
        {
            MagicOrchestraBuilderManager.singleton.frontalCamera = this.frontalCamera;
            MagicOrchestraBuilderManager.singleton.zenithCamera = this.zenithCamera;
            MagicOrchestraBuilderManager.singleton.controllerCamera = this.controllerCamera;
            MagicOrchestraBuilderManager.singleton.ActivateAllCameras();
        }
    }

    /* <summary>
    * StartFrontalSequence manage the first phase of the game
    * </summary>
    *
    * <param name="isReverse"> bool that represents if the user has to repeat the sequence in the reverse mode or not </param>
    * <param name="showTime"> time must be a float number that represents the number of seconds the number is shown </param>
    * <param name="sequence"> sequence is an array of numbers representing the numbers' sequence </param>
    * <remarks> sequence must contain the numbers and NOT as indexes </remarks>
    */
    public void StartFrontalPhase()
    {
        this.DisablePlayerMovement();

        UserDigitSpan.singleton.PutRecognitionOff();
        SequenceShower.singleton.Show(this.sequence, this.timeInShowing);
    }

    /* <summary>
     * EndFrontalSequence is called when the first phase is ended
     * </summary>
     */
    public void EndFrontalPhase()
    {
        Game2CanvasController.singleton.DecisionPoint();
    }

    public void StartUserPhase()
    {
        SequenceShower.singleton.ShowUserMessage(this.isReverse);

        UserDigitSpan.singleton.Init(this.sequence, this.isReverse);
        UserDigitSpan.singleton.PutRecognitionOn();

        //Enabling the user movement
        EnablingPlayerMovement();
    }

    public void CorrectUserSequence()
    {
        SequenceShower.singleton.StopClassCoroutine();
        SceneManager.LoadScene("MagicOrchestra");
    }

    public void WrongUserSequence()
    {
        SequenceShower.singleton.StopClassCoroutine();

        UserDigitSpan.singleton.PutRecognitionOff();
        DisablePlayerMovement();
        Game2CanvasController.singleton.DecisionPoint();
    }

    /* <summary>
     * Function to allow user movement detection
     * </summary>
     */
    public void EnablingPlayerMovement()
    {
        if(TesterGame2.singleton != null)
            TesterGame2.singleton.isRecognitionEnabled = true;
        if(Game2Player.singleton != null)
            Game2Player.singleton.isRecognitionEnabled = true;
    }

    /* <summary>
     * Function to disable user movement detection
     * </summary>
     */
    public void DisablePlayerMovement()
    {
        if (TesterGame2.singleton != null)
            TesterGame2.singleton.isRecognitionEnabled = false;
        if (Game2Player.singleton != null)
            Game2Player.singleton.isRecognitionEnabled = false;
    }
}
