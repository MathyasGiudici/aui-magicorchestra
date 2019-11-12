using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceUser : MonoBehaviour
{
    //Singleton of the SequenceUser class
    public static SequenceUser singleton = null;

    //Material for the cubes
    public Material defaultMaterial;
    public Material lightMaterial;

    //Panels
    public GameObject correctSequencePanel;
    public GameObject wrongSequencePanel;

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
     * The function shows the sequence of lights (cubes) on the frontal plane
     * </summary>
     *
     * <param name="color"> color must be a string containing the hexadecimal value of the color </param>
     * <param name="time"> time must be a float number that represents the number of seconds the light should be on </param>
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
    }

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
        //Lighting the cube
        this.coroutine = StartCoroutine(this.LightCoroutine(currentCube));

        //Checking the correctness of the sequence
        if ( currentIndexSequence <= (sequence.Length - 1))
        {
            if (sequence[currentIndexSequence] == currentCubeNumber)
            {
                if (currentIndexSequence == (sequence.Length - 1))
                    this.CorrectSequence();
            }
            else
            {
                this.ErrorInSequence();
            }  
        }
        else
        {
            Debug.Log("Something strange");
        }        

        currentIndexSequence++;
    }

    private IEnumerator LightCoroutine(GameObject cubeToLight)
    {
        //Turn on the light
        CorsiUtils.ShowLightOnCube(cubeToLight, lightMaterial);
        yield return new WaitForSeconds(0.5f);

        //Turn off the light
        CorsiUtils.RestoreIntialCube(cubeToLight, defaultMaterial);
        StopCoroutine(this.coroutine);
    }

    /* <summary>
     * The function is called if the sequence is correct
     * </summary>
     */
    private void CorrectSequence()
    {
        CorsiController.singleton.CorrectUserSequence();
        return;
        this.coroutine = StartCoroutine(this.PanelCoroutine(correctSequencePanel,true));
    }

    /* <summary>
     * The function is called if the sequence is NOT correct
     * </summary>
     */
    private void ErrorInSequence()
    {
        CorsiController.singleton.WrongUserSequence();
        return;
        this.coroutine = StartCoroutine(this.PanelCoroutine(wrongSequencePanel,false));
    }

    private IEnumerator PanelCoroutine(GameObject panel, bool isSequenceCorrect)
    {
        //Showing the Panel
        panel.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        panel.SetActive(false);
        yield return new WaitForSeconds(2.0f);

        if (isSequenceCorrect)
            CorsiController.singleton.CorrectUserSequence();
        else
            CorsiController.singleton.WrongUserSequence();

        //Stopping coroutine
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

    public float GetCaptureTime()
    {
        return this.captureTime;
    }

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
