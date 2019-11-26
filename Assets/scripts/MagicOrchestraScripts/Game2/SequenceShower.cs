using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SequenceShower : MonoBehaviour
{
    //Singleton of the SequenceShower class
    public static SequenceShower singleton = null;

    //Text container
    public Text frontalText;
    public Text frontalNumberText;
    public Text zenithText;

    //Coroutine reference
    private Coroutine coroutine = null;

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
        this.frontalText.GetComponent<Text>().text = "";
        this.frontalNumberText.GetComponent<Text>().text = "";
        this.zenithText.GetComponent<Text>().text = "";
    }

    /* <summary>
    * The function shows the sequence of numbers on the frontal plane
    * </summary>
    *
    * <param name="showTime"> time must be a float number that represents the number of seconds the number is shown </param>
    * <param name="sequence"> sequence is an array of numbers representing the numbers' sequence </param>
    * <remarks> sequence must contain the numbers and NOT as indexes </remarks>
    */
    public void Show(int[] sequence, float showTime, bool isReverse)
    {
        this.coroutine = StartCoroutine(this.SequenceRoutine(sequence, showTime, isReverse));
    }

    public void ShowEndMessage(bool hasWin)
    {
        Destroy(gameObject.GetComponent<UserDigitSpan>());
        this.coroutine = StartCoroutine(this.FinalCoroutine(hasWin));
    }
    

    private IEnumerator SequenceRoutine(int[] sequence, float showTime, bool isReverse)
    {
        //Showing starting information
        this.frontalText.GetComponent<Text>().text = "Guarda la sequenza attentamente";
        yield return new WaitForSeconds(MagicOrchestraUtils.generalTextTimeShow_long);
        this.frontalText.GetComponent<Text>().text = "";
        yield return new WaitForSeconds(MagicOrchestraUtils.generalPauseTime_short);

        foreach (int number in sequence)
        {
            if (number >= 1 && number <= 9)
            {
                this.frontalNumberText.GetComponent<Text>().text = number.ToString();
                yield return new WaitForSeconds(showTime);
                this.frontalNumberText.GetComponent<Text>().text = "";
            }
        }

        //Enabling user actions
        this.InstanciateUserDigitSpan(sequence, isReverse);

        //Showing user turn information
        if (!isReverse)
            this.frontalText.GetComponent<Text>().text = "Ripeti la sequenza";
        else
            this.frontalText.GetComponent<Text>().text = "Ripeti la sequenza IN ORDINE INVERSO";

        yield return new WaitForSeconds(MagicOrchestraUtils.generalTextTimeShow_long);
        this.frontalText.GetComponent<Text>().text = "";
        yield return new WaitForSeconds(MagicOrchestraUtils.generalPauseTime_short);

        GameController.singleton.EndFrontalPhase();

        this.StopClassCoroutine();
    }

    private IEnumerator FinalCoroutine(bool hasWin)
    {
        //Showing user turn information
        if (hasWin)
            this.frontalText.GetComponent<Text>().text = "Sequenza corretta!";
        else
            this.frontalText.GetComponent<Text>().text = "Sequenza non corretta, riproviamo!";

        yield return new WaitForSeconds(MagicOrchestraUtils.generalTextTimeShow_long);
        this.frontalText.GetComponent<Text>().text = "";
        yield return new WaitForSeconds(MagicOrchestraUtils.generalPauseTime_short);

        //Recalling correct function
        if (hasWin)
            GameController.singleton.CorrectUserSequence();
        else
            GameController.singleton.WrongUserSequence();

        //this.StopClassCoroutine();
    }

    public void StopClassCoroutine()
    {
        StopCoroutine(this.coroutine);
    }

    private void InstanciateUserDigitSpan(int[] sequence, bool isReverse)
    {
        gameObject.AddComponent<UserDigitSpan>();
        UserDigitSpan.singleton.Init(sequence, isReverse);
    }
}
