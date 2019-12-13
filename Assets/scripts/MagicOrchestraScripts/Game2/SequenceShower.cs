using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SequenceShower : MonoBehaviour
{
    //Singleton of the SequenceShower class
    public static SequenceShower singleton = null;

    //Text container
    public GameObject panelMessage;
    public GameObject frontalTextMessage;

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
        this.coroutine = StartCoroutine(this.FinalCoroutine(hasWin));
    }

    private IEnumerator SequenceRoutine(int[] sequence, float showTime, bool isReverse)
    {
        //Showing starting information
        frontalTextMessage.GetComponent<Text>().text = "Guarda la sequenza attentamente";
        panelMessage.SetActive(true);
        yield return new WaitForSeconds(MagicOrchestraUtils.generalTextTimeShow_long);
        panelMessage.SetActive(false);
        frontalTextMessage.GetComponent<Text>().text = "";
        yield return new WaitForSeconds(MagicOrchestraUtils.generalPauseTime_short);

        bool showHints = MagicOrchestraParameters.IsContext && Game2Parameters.IsHintMode;

        //Showing the sequence
        SequenceContextManager.singleton.SetActiveDisplayedNumber(true);
        if (showHints)
        {
            Game2HitImageController.singleton.ShuffleSprites();
            Game2HitImageController.singleton.SetActiveDisplayedHints(true);
        }
            

        foreach (int number in sequence)
        {
            if (number >= 1 && number <= 9)
            {
                SequenceContextManager.singleton.ChangeDisplyedNumber(number.ToString());                
                yield return new WaitForSeconds(showTime);
                SequenceContextManager.singleton.ChangeDisplyedNumber("");

                if (showHints)
                    Game2HitImageController.singleton.ChangeImage();
            }
        }

        SequenceContextManager.singleton.SetActiveDisplayedNumber(false);
        if (showHints)
            Game2HitImageController.singleton.SetActiveDisplayedHints(false);

        //Showing information about seqence
        if (!isReverse)
            frontalTextMessage.GetComponent<Text>().text = "Ripeti la sequenza";
        else
            frontalTextMessage.GetComponent<Text>().text = "Ripeti la sequenza IN ORDINE INVERSO";

        panelMessage.SetActive(true);
        yield return new WaitForSeconds(MagicOrchestraUtils.generalTextTimeShow_long);
        panelMessage.SetActive(false);
        frontalTextMessage.GetComponent<Text>().text = "";
        yield return new WaitForSeconds(MagicOrchestraUtils.generalPauseTime_short);

        Game2Controller.singleton.EndFrontalPhase();

        this.StopClassCoroutine();
    }    

    private IEnumerator FinalCoroutine(bool hasWin)
    {
        //Showing user turn information
        if (hasWin)
            frontalTextMessage.GetComponent<Text>().text = "Bravissimo!\nSequenza corretta";
        else
            frontalTextMessage.GetComponent<Text>().text = "Ops... sequenza sbagliata";

        panelMessage.SetActive(true);
        yield return new WaitForSeconds(MagicOrchestraUtils.generalTextTimeShow_long);
        panelMessage.SetActive(false);
        frontalTextMessage.GetComponent<Text>().text = "";
        yield return new WaitForSeconds(MagicOrchestraUtils.generalPauseTime_short);

        //Recalling correct function
        if (hasWin)
            Game2Controller.singleton.CorrectUserSequence();
        else
            Game2Controller.singleton.WrongUserSequence();

        //this.StopClassCoroutine();
    }

    public void StopClassCoroutine()
    {
        StopCoroutine(this.coroutine);
    }
}
