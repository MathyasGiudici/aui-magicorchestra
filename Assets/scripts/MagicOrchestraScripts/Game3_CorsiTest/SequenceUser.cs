using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SequenceUser : MonoBehaviour
{
    //Singleton of the SequenceUser class
    public static SequenceUser singleton = null;

    //Material for the cubes
    public Material defaultMaterial;
    public Material defaultMaterial_context;
    public Material defaultMaterial_no_context;
    public Material lightMaterial;

    //Text container
    public GameObject panelMessage;
    public GameObject frontalTextMessage;

    //User parameters custom
    private int[] sequence;
    private bool isGestureMode = true;
    private float captureTime = 2;

    //Current cube where the user is
    private GameObject currentCube = null;
    private int currentCubeNumber = 0;
    private int currentIndexSequence = 0;


    //Coroutine reference
    public Coroutine coroutine = null;


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
        if (MagicOrchestraParameters.IsContext)
            this.defaultMaterial = this.defaultMaterial_context;
        else
            this.defaultMaterial = this.defaultMaterial_no_context;

        this.InstantiateMaterialOnCubes();
    }

    /* <summary>
     * The function shows the sequence of lights (cubes) on the frontal plane
     * </summary>
     *
     * <param name="color"> color must be a string containing the hexadecimal value of the color </param>
     * <param name="captureTime"> time must be a float number that represents the number of seconds the light should be on </param>
     * <param name="isGestureMode"> isGestureMode represents if the user must compute a gesture to select the cube</param>
     * <param name="sequence"> sequence is an array of numbers representing the light sequence </param>
     * <remarks> sequence must contain the number of cubes and NOT as indexes </remarks>
     */
    public void StartSequence(string color, float captureTime, bool isGestureMode, int[] sequence)
    {
        //Setting the sequence
        this.sequence = sequence;

        //Setting the color of the light
        this.ChangeColor(color);

        //Setting if the user must play a gesture to select a cube
        this.isGestureMode = isGestureMode;

        //Setting the capturing time of the user
        this.captureTime = captureTime;

        //Resetting the parameters
        this.currentCube = null;
        this.currentCubeNumber = 0;
        this.currentIndexSequence = 0;

        this.InstantiateControllerOnCubes();

        this.coroutine = StartCoroutine(this.PanelCoroutine());
    }

    /* <summary>
     * InstantiateControllerOnCubes creates the ZenithCubeController in the cubes
     * </summary>
     */
    private void InstantiateMaterialOnCubes()
    {
        for (int i = 0; i < 9; i++)
        {
            gameObject.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = defaultMaterial;
            gameObject.transform.GetChild(i).gameObject.transform.GetChild(1).gameObject.GetComponent<Renderer>().material = defaultMaterial;
        }
    }

    /* <summary>
     * InstantiateControllerOnCubes creates the ZenithCubeController in the cubes
     * </summary>
     */
    private void InstantiateControllerOnCubes()
    {
        for(int i = 0; i<9; i++)
        {
            if(gameObject.transform.GetChild(i).gameObject.transform.GetChild(1).gameObject.GetComponent<ZenithCubeController>() == null)
                gameObject.transform.GetChild(i).gameObject.transform.GetChild(1).gameObject.AddComponent<ZenithCubeController>();
        }
    }

    /* <summary>
    * The function allow the selection of a cube of the sequence
    * </summary>
    */
    public void CubeSelection()
    {
        //To mitigate null pointer exception if the user play a selection where it is not on a cube
        if (currentCube == null)
            return;

        //Checking the correctness of the sequence
        if ( currentIndexSequence <= (sequence.Length - 1))
        {
            if (sequence[currentIndexSequence] == currentCubeNumber)
            {
                //Checking if the last selected cube is the last number of the sequence to be catched
                if (currentIndexSequence == (sequence.Length - 1))
                    this.coroutine = StartCoroutine(this.LightCoroutine(currentCube, true, true));
                else
                    this.coroutine = StartCoroutine(this.LightCoroutine(currentCube,false,true)); //Light the cube
            }
            else
            {
                //Sequence not correct
                this.coroutine = StartCoroutine(this.LightCoroutine(currentCube, true, false));
            }  
        }
        else
        {
            Debug.LogWarning("Something not correct with the detection of the sequence");
        }        

        currentIndexSequence++;
    }

    private IEnumerator PanelCoroutine()
    {
        //Showing the User turn Panel
        panelMessage.SetActive(true);
        frontalTextMessage.GetComponent<Text>().text = MagicOrchestraUtils.beginUserTurnMessage;
        frontalTextMessage.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(MagicOrchestraUtils.pathToTextMessages + "beginUserTurnMessage");
        frontalTextMessage.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(MagicOrchestraUtils.generalTextTimeShow_long);
        panelMessage.SetActive(false);
        frontalTextMessage.GetComponent<Text>().text = "";
        yield return new WaitForSeconds(MagicOrchestraUtils.generalPauseTime_short);

        //Abort coroutine
        StopClassCoroutine();
    }


    private IEnumerator LightCoroutine(GameObject cubeToLight, bool isFinal, bool isSequenceCorrect)
    {
        //Turn on the light
        CorsiUtils.ShowLightOnCube(cubeToLight, lightMaterial);
        yield return new WaitForSeconds(CorsiUtils.reactionLightTime);

        //Turn off the light
        CorsiUtils.RestoreIntialCube(cubeToLight, defaultMaterial);

        //Final cube detected
        if (isFinal)
        {
            float waitTime = MagicOrchestraUtils.generalTextTimeShow_long;
            //Recalling game manager
            if (isSequenceCorrect)
            {
                panelMessage.SetActive(true);
                // Message
                frontalTextMessage.GetComponent<Text>().text = MagicOrchestraUtils.correctSequenceMessage;
                // Light feedback
                MagicOrchestraUtils.PositiveLightFeedback();
                // First sound
                if (MagicOrchestraParameters.IsContext)
                    frontalTextMessage.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(MagicOrchestraUtils.pathToSoundMessages + "sequence_correct_context");
                else
                    frontalTextMessage.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(MagicOrchestraUtils.pathToSoundMessages + "sequence_correct");
                frontalTextMessage.GetComponent<AudioSource>().Play();
                waitTime -= frontalTextMessage.GetComponent<AudioSource>().clip.length;
                yield return new WaitForSeconds(frontalTextMessage.GetComponent<AudioSource>().clip.length);
                // Second sound
                frontalTextMessage.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(MagicOrchestraUtils.pathToTextMessages + "correctSequenceMessage");
                frontalTextMessage.GetComponent<AudioSource>().Play();
                waitTime -= frontalTextMessage.GetComponent<AudioSource>().clip.length;
                yield return new WaitForSeconds(frontalTextMessage.GetComponent<AudioSource>().clip.length);

                yield return new WaitForSeconds(waitTime);
                panelMessage.SetActive(false);
                frontalTextMessage.GetComponent<Text>().text = "";
                yield return new WaitForSeconds(MagicOrchestraUtils.generalPauseTime_short);
                MagicOrchestraUtils.SwitchOffLightFeedback();

                CorsiController.singleton.CorrectUserSequence();
            }
            else
            {
                panelMessage.SetActive(true);
                // Message
                frontalTextMessage.GetComponent<Text>().text = MagicOrchestraUtils.wrongSequenceMessage;
                // Light feedback
                MagicOrchestraUtils.NegativeLightFeedback();
                // First sound
                frontalTextMessage.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(MagicOrchestraUtils.pathToSoundMessages + "sequence_wrong");
                frontalTextMessage.GetComponent<AudioSource>().Play();
                waitTime -= frontalTextMessage.GetComponent<AudioSource>().clip.length;
                yield return new WaitForSeconds(frontalTextMessage.GetComponent<AudioSource>().clip.length);
                // Second sound
                frontalTextMessage.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(MagicOrchestraUtils.pathToTextMessages + "wrongSequenceMessage");
                frontalTextMessage.GetComponent<AudioSource>().Play();
                waitTime -= frontalTextMessage.GetComponent<AudioSource>().clip.length;
                yield return new WaitForSeconds(frontalTextMessage.GetComponent<AudioSource>().clip.length);

                yield return new WaitForSeconds(waitTime);
                panelMessage.SetActive(false);
                frontalTextMessage.GetComponent<Text>().text = "";
                yield return new WaitForSeconds(MagicOrchestraUtils.generalPauseTime_short);
                MagicOrchestraUtils.SwitchOffLightFeedback();

                CorsiController.singleton.WrongUserSequence();
            }
        }

        //Abort coroutine
        StopClassCoroutine();
    }

    /* <summary>
     * StopClassCoroutine stops the corutine instanciated by this class
     * </summary>
     */
    private void StopClassCoroutine()
    {
        StopCoroutine(this.coroutine);
    }

    /* <summary>
     * The function changes the current cube where the user is
     * </summary>
     */
    public void NewCurrentCube(int cubeNumber,GameObject cube)
    {
        this.currentCubeNumber = cubeNumber;
        this.currentCube = cube;
    }

    /* <summary>
     * The function changes delets the reference number to the cube where the user is
     * </summary>
     */
    public void DeleteCurrentCube()
    {
        this.currentCubeNumber = 0;
        this.currentCube = null;
    }

    /* <summary>
     * Getter for the number that represents the number of second the user must stay on the cube to select it
     * </summary>
     */
    public float GetCaptureTime()
    {
        return this.captureTime;
    }

    /* <summary>
     * Getter for the bool that represents if the user has or not to play a gesture to select the cube where he/she is
     * </summary>
     */
    public bool GetGestureMode()
    {
        return this.isGestureMode;
    }

    /* <summary>
     * The function changes the color of the Material used as light for the sequence
     * </summary>
     */
    private void ChangeColor(string color)
    {
        CorsiUtils.CreateMaterialFromColor(lightMaterial, color);
    }
}
