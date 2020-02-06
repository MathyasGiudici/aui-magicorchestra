using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZenithCubeController : MonoBehaviour
{
    //Some utils parameters of the class
    private GameObject parent;
    private bool isGestureMode = SequenceUser.singleton.GetGestureMode();
    private float timeToGong = SequenceUser.singleton.GetCaptureTime();
    private float timer;

    /* <summary>
     * OnTriggerEnter is called when the player enters and starts the timer
     * </summary>
     */
    void OnTriggerEnter(Collider other)
    {
        //Setting a timer
        this.timer = 0;

        //Setting the parent
        parent = gameObject.transform.parent.gameObject;

        //Informing the SequenceUser of the current position
        int cubeNumber = int.Parse(parent.name[parent.name.Length - 1].ToString());
        SequenceUser.singleton.NewCurrentCube(cubeNumber, this.gameObject);
    }

    /* <summary>
     * OnTriggerStay is called when the player is in the cube and updates the timer
     * </summary>
     */
    void OnTriggerStay(Collider other)
    {
        //Updating the timer
        this.timer += Time.deltaTime;

        //Reproducing a sound after a given amount of time
        if ((parent != null) && ((this.timer%60) >= timeToGong) && ((this.timer % 60) < (timeToGong + Time.deltaTime)))
        {
            //Reproducing a sound
            parent.GetComponent<AudioSource>().Play();

            //Checking if the gestureMode is enabled
            if (!isGestureMode)
            {
                //To select a cube i've to stay over its
                SequenceUser.singleton.CubeSelection();
            }
        }
            
    }

    /* <summary>
     * OnTriggerExit is called when the player exits from the cube and destroy the timer
     * </summary>
     */
    private void OnTriggerExit(Collider other)
    {
        //Reset the timer
        this.timer = 0;

        //Reset the color
        CorsiUtils.RestoreIntialCube(this.gameObject, SequenceUser.singleton.defaultMaterial);

        //Informing the SequenceUser of the current position
        SequenceUser.singleton.DeleteCurrentCube();
    }
}
