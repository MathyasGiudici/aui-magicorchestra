using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorsiController : MonoBehaviour
{
    //Singleton of the CorsiController class
    public static CorsiController singleton = null;

    //Protected parameters that contains the main custom parameters of the game
    //int[] sequence = { 2, 3, 6, 7, 4, 8, 1, 9, 5 };
    int[] sequence = { 2 , 4};
    string lightColor = "#ff0000";

    float timeInShowing = 2;

    bool isGestureMode = false;
    float timeInDetecting = 2.0f;

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
        Debug.Log("Corsi Test is on fire!");
    }

    /* <summary>
     * Update is called once per frame
     * </summary>
     */
    void Update()
    {
        //Instruction to Start Sequence
        if (Input.GetKeyUp(KeyCode.S) == true)
        {
            StartFrontalPhase();
        }
    }

    /* <summary>
     * StartFrontalSequence manage the first phase of the game
     * </summary>
     */
    public void StartFrontalPhase()
    {
        this.DisablePlayerMovement();
        SequenceLighter.singleton.StartSequence(lightColor, timeInShowing, sequence);
    }

    /* <summary>
     * EndFrontalSequence is called when the first phase is ended
     * </summary>
     */
    public void EndFrontalPhase()
    {
        Debug.Log("End of frontal sequence");
        StartUserPhase();
    }

    /* <summary>
     * StartFrontalSequence manage the second phase of the game
     * </summary>
     */
    public void StartUserPhase()
    {
        //Enabling the user movement
        EnablingPlayerMovement();

        //Instantiating the User phase manager
        SequenceUser.singleton.StartSequence(lightColor,timeInDetecting,isGestureMode,sequence);
    }

    /* <summary>
     * UserGesture is called when a gesture to select a cube by the user is performed
     * </summary>
     */
    public void UserGesture()
    {
        if (SequenceUser.singleton.GetGestureMode())
            SequenceUser.singleton.CubeSelection();
    }

    /* <summary>
     * EndFrontalSequence is called when the second phase is ended and
     * the user has performed a correct sequence
     * </summary>
     */
    public void CorrectUserSequence()
    {
        Debug.Log("Sequence reproduced by the user correctly");
    }

    /* <summary>
     * EndFrontalSequence is called when the second phase is ended and
     * the user has NOT performed a correct sequence
     * </summary>
     */
    public void WrongUserSequence()
    {
        Debug.Log("Sequence reproduced by the user NOT correctly");
        this.StartFrontalPhase(); 
    }

    /* <summary>
     * Function to allow user movement detection
     * </summary>
     */
    public void EnablingPlayerMovement()
    {
        TesterMovement.singleton.isMovementEnabled = true;
    }

    /* <summary>
     * Function to disable user movement detection
     * </summary>
     */
    public void DisablePlayerMovement()
    {
        TesterMovement.singleton.isMovementEnabled = false;
    }
}
