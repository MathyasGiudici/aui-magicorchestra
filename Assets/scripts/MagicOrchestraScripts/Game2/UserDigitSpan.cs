using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDigitSpan : MonoBehaviour
{
    //Singleton of the UserDigitSpan class
    public static UserDigitSpan singleton = null;

    //Private setting variables
    private int[] sequence;
    private bool isReverse;

    private bool recognitionOn = false;

    private int currentIndex;

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

    public void Init(int[] sequence, bool isReverse)
    {
        this.sequence = sequence;
        this.isReverse = isReverse;

        if (isReverse)
            this.currentIndex = sequence.Length - 1;
        else
            this.currentIndex = 0;
    }

    public void SelectNumber(int number)
    {
        if (!(recognitionOn))
            return;

        if (Game2Parameters.ConfirmSound)
            this.gameObject.GetComponent<AudioSource>().Play();

        // Checking the sequence
        if (sequence[currentIndex] != number)
        {
            SequenceShower.singleton.ShowEndMessage(false);
            return;
        }

        // Checking if  is the last number to remember
        if (isReverse)
        {
            currentIndex = currentIndex - 1;

            if (currentIndex < 0)
                SequenceShower.singleton.ShowEndMessage(true);
        }
        else
        {
            currentIndex = currentIndex + 1;

            if(currentIndex >= sequence.Length)
                SequenceShower.singleton.ShowEndMessage(true);
        }
    }

    public void PutRecognitionOn()
    {
        this.recognitionOn = true;
    }

    public void PutRecognitionOff()
    {
        this.recognitionOn = false;
    }
}
