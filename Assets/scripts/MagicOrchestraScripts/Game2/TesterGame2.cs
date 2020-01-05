using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesterGame2 : MonoBehaviour
{
    //Singleton of the SequenceShower class
    public static TesterGame2 singleton = null;


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
    void Update()
    {

        if(!(isRecognitionEnabled))
            return;

        if(Input.GetKeyUp(KeyCode.Alpha1) == true)
        {
            UserDigitSpan.singleton.SelectNumber(1);
            return;
        }
        if (Input.GetKeyUp(KeyCode.Alpha2) == true)
        {
            UserDigitSpan.singleton.SelectNumber(2);
            return;
        }
        if (Input.GetKeyUp(KeyCode.Alpha3) == true)
        {
            UserDigitSpan.singleton.SelectNumber(3);
            return;
        }
        if (Input.GetKeyUp(KeyCode.Alpha4) == true)
        {
            UserDigitSpan.singleton.SelectNumber(4);
            return;
        }
        if (Input.GetKeyUp(KeyCode.Alpha5) == true)
        {
            UserDigitSpan.singleton.SelectNumber(5);
            return;
        }
        if (Input.GetKeyUp(KeyCode.Alpha6) == true)
        {
            UserDigitSpan.singleton.SelectNumber(6);
            return;
        }
        if (Input.GetKeyUp(KeyCode.Alpha7) == true)
        {
            UserDigitSpan.singleton.SelectNumber(7);
            return;
        }
        if (Input.GetKeyUp(KeyCode.Alpha8) == true)
        {
            UserDigitSpan.singleton.SelectNumber(8);
            return;
        }
        if (Input.GetKeyUp(KeyCode.Alpha9) == true)
        {
            UserDigitSpan.singleton.SelectNumber(9);
            return;
        }
    }

}
