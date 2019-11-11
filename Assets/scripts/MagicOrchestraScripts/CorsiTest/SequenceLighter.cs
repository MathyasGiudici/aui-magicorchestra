using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceLighter : MonoBehaviour
{
    //Singleton SequenceLighter
    public static SequenceLighter singleton = null;

    //Material for the light
    public Material defaultMaterial;
    public Material lightMaterial;

    //Canvas
    public GameObject startingPanel;
    public GameObject[] userTurnPanel;

    //Time of the light
    private float time = 2;

    //Coroutine reference
    private Coroutine lightCoroutine = null;

    //Managing Singleton
    void Awake()
    {
        if ((singleton != null) && (singleton != this))
        {
            Destroy(gameObject);
            return;
        }
        singleton = this;
    }

    /* Function to show the sequence on the frontal plane
     *
     * NB: sequence is expeted as array of numbers NOT as index of an array
     */
    public void StartSequence(string color, float time, int[] sequence)
    {
        //Changing the light color
        if (color != "#ff0000")
            this.ChangeColor(color);

        //Saving the show time of the light
        if(time > 1)
            this.time = time;

        //Launching the Coroutine to show sequence
        this.lightCoroutine = StartCoroutine(this.LightCoroutine(sequence, gameObject));
    }

    private IEnumerator LightCoroutine (int[] sequence, GameObject plane)
    {
        GameObject frontalCube;

        //Showing the Starting Panel
        startingPanel.SetActive(true);
        yield return new WaitForSeconds(5.0f);
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
                ShowLight(frontalCube);
                yield return new WaitForSeconds(this.time);

                //Turn off the light
                RestoreIntialCube(frontalCube);
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

        CorsiController.singleton.EndFrontalSequence();
        StopCoroutine(this.lightCoroutine);
    }

    private void ShowLight(GameObject frontalCube)
    {
        frontalCube.GetComponent<Renderer>().material = lightMaterial;
    }

    private void RestoreIntialCube(GameObject frontalCube)
    {
        frontalCube.GetComponent<Renderer>().material = defaultMaterial;
    }

    //Change the color of the material 
    private void ChangeColor(string color)
    {
        //Converting the string
        //Escaping the intial char
        if (color.IndexOf('#') != -1)
            color = color.Replace("#", "");

        //Retriving RGB
        int r, g, b = 0;
        r = int.Parse(color.Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
        g = int.Parse(color.Substring(2, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
        b = int.Parse(color.Substring(4, 2), System.Globalization.NumberStyles.AllowHexSpecifier);

        //Creating the new Material
        Material material = new Material(lightMaterial);

        material.color = new Color(r,g,b);
        material.SetColor("_EMISSION", material.color);

        this.lightMaterial = material;
    }

}
