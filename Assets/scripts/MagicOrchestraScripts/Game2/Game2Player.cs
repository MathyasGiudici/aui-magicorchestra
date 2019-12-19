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

        //GameObject magicRoom = GameObject.Find("Magic Room Adapter");
        List<GameObject> toys = MagicRoomSmartToyManager.instance.getSmartToys();
        GameObject rfid = null;

        foreach (GameObject toy in toys)
            if (toy.GetComponent<SmartToy>().rfidsensor)
                rfid = toy;

        if (rfid == null)
                Debug.Log("Ball not connected");

        // TODO: vediamo se arriva fino a qui
        Debug.Log(rfid.GetComponent<RFIDReader>().lastread);
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