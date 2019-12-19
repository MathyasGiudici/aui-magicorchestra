using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game2Player : MonoBehaviour
{
    //Singleton of the SequenceShower class
    public static Game2Player singleton = null;


    public bool isRecognitionEnabled = false;

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
    * Update is called once per frame
    * </summary>
    */
    public void Update()
    {
        // Checking recognition status
        if (!(isRecognitionEnabled))
            return;

        // Looking for the magic ball
        GameObject ball = GameObject.Find("Passaporta");
        if (ball == null)
        {
            Debug.Log("Ball not connected");
            return;
        }
                
        // TODO: vediamo se arriva fino a qui
        Debug.Log(ball.GetComponent<RFIDReader>().lastread);
        return;

        if (ball.GetComponent<RFIDReader>().lastread != null)
            this.RecievedNumber(int.Parse(ball.GetComponent<RFIDReader>().lastread));
    }

    /* <summary>
    * Sends the recieved number to the Game2 Controller
    * </summary>
    */
    void RecievedNumber(int number)
    {

        if (!(isRecognitionEnabled))
            return;

        UserDigitSpan.singleton.SelectNumber(number);
    }


    public void LightOfCube()
    {
        Debug.Log("FEEDBACK: Light of the cube");
    }
}