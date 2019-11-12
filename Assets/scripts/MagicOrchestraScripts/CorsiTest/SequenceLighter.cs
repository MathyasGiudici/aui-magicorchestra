using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceLighter : MonoBehaviour
{
    //Singleton of the SequenceLighter class
    public static SequenceLighter singleton = null;

    //Material for the cubes
    public Material defaultMaterial;
    public Material lightMaterial;

    //Canvas
    public GameObject startingPanel;
    public GameObject[] userTurnPanel;

    //Time of the light
    private float showTime = 2;

    //Coroutine reference
    private Coroutine lightCoroutine = null;

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
     * <param name="sequence"> sequence is an array of numbers representing the light sequence </param>
     * <remarks> sequence must contain the number of cubes and NOT as indexes </remarks>
     */
    public void StartSequence(string color, float showTime, int[] sequence)
    {
        //Setting the color of the light
        this.ChangeColor(color);

        //Setting the showing time of the light
        this.showTime = showTime;

        //Launching the Coroutine to show sequence
        this.lightCoroutine = StartCoroutine(this.LightCoroutine(sequence, gameObject));
    }

    /* <summary>
    * The function shows the sequence of lights (cubes) on the frontal plane
    * </summary>
    *
    * <param name="sequence"> sequence is an array of numbers representing the light sequence </param>
    * <remarks> sequence must contain the number of cubes and NOT as indexes </remarks>
    * <param name="plane"> plane is the GameObject reference to the plane where the cubes are </param>
    */
    private IEnumerator LightCoroutine (int[] sequence, GameObject plane)
    {
        GameObject frontalCube;

        //Showing the Starting Panel
        startingPanel.SetActive(true);
        yield return new WaitForSeconds(4.0f);
        startingPanel.SetActive(false);
        yield return new WaitForSeconds(2.0f);

        //Looping on the sequence
        foreach (int number in sequence)
        {
            if (number >= 1 && number <= 9)
            {
                //Retriving the frontal cube
                frontalCube = plane.transform.GetChild(number - 1).gameObject.transform.GetChild(0).gameObject;

                //Turn on the light
                CorsiUtils.ShowLightOnCube(frontalCube,lightMaterial);
                yield return new WaitForSeconds(this.showTime);

                //Turn off the light
                CorsiUtils.RestoreIntialCube(frontalCube,defaultMaterial);
            }        
        }

        //Showing the User turn Panel
        foreach( GameObject go in userTurnPanel)
        {
            go.SetActive(true);
        }
        yield return new WaitForSeconds(2.0f);
        foreach (GameObject go in userTurnPanel)
        {
            go.SetActive(false);
        }

        CorsiController.singleton.EndFrontalPhase();
        StopCoroutine(this.lightCoroutine);
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
