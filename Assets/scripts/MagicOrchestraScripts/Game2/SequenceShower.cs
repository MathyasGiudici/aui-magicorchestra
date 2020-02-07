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
    public void Show(int[] sequence, float showTime)
    {
        this.coroutine = StartCoroutine(this.SequenceRoutine(sequence, showTime));
    }

    public void ShowUserMessage(bool isReverse)
    {
        StopClassCoroutine();
        this.coroutine = StartCoroutine(this.SequenceUserPanelRoutine(isReverse));
    }

    public void ShowEndMessage(bool hasWin)
    {
        StopClassCoroutine();
        this.coroutine = StartCoroutine(this.FinalCoroutine(hasWin));
    }

    private IEnumerator SequenceRoutine(int[] sequence, float showTime)
    {
        panelMessage.SetActive(true);

        //Showing starting information
        if (MagicOrchestraParameters.IsContext)
        {
            frontalTextMessage.GetComponent<Text>().text = MagicOrchestraUtils.beginGame2SequenceMessage_context;
            frontalTextMessage.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(MagicOrchestraUtils.pathToTextMessages + "beginGame2SequenceMessage_context");
            frontalTextMessage.GetComponent<AudioSource>().Play();
        }   
        else
        {
            frontalTextMessage.GetComponent<Text>().text = MagicOrchestraUtils.beginGame2SequenceMessage;
            frontalTextMessage.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(MagicOrchestraUtils.pathToTextMessages + "beginGame2SequenceMessage");
            frontalTextMessage.GetComponent<AudioSource>().Play();
        }
        yield return new WaitForSeconds(MagicOrchestraUtils.generalTextTimeShow_long);
        panelMessage.SetActive(false);
        frontalTextMessage.GetComponent<Text>().text = "";
        yield return new WaitForSeconds(MagicOrchestraUtils.generalPauseTime_short);

        bool showHints = MagicOrchestraParameters.IsContext && Game2Parameters.IsHintMode;

        //Showing the sequence
        SequenceContextManager.singleton.SetActiveDisplayedNumber(true);
        if (showHints)
        {
            Game2HitImageController.singleton.SetActiveDisplayedHints(true);
        }
            

        foreach (int number in sequence)
        {
            if (number >= 1 && number <= 9)
            {
                //Checking for hints
                if(showHints)
                    Game2HitImageController.singleton.ChangeImage(number);

                // Changing displayed number
                SequenceContextManager.singleton.ChangeDisplyedNumber(number.ToString());
                // Playing audio                
                gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(MagicOrchestraUtils.pathToTextOfNumbers + "number" + number.ToString());
                gameObject.GetComponent<AudioSource>().Play();

                yield return new WaitForSeconds(showTime);
                SequenceContextManager.singleton.ChangeDisplyedNumber("");
            }
        }

        SequenceContextManager.singleton.SetActiveDisplayedNumber(false);
        if (showHints)
            Game2HitImageController.singleton.SetActiveDisplayedHints(false);

        Game2Controller.singleton.EndFrontalPhase();

        this.StopClassCoroutine();
    }

    private IEnumerator SequenceUserPanelRoutine(bool isReverse)
    {
        panelMessage.SetActive(true);

        //Showing information about seqence 
        if (!isReverse)
        {
            frontalTextMessage.GetComponent<Text>().text = MagicOrchestraUtils.repeatSequenceMessage;
            frontalTextMessage.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(MagicOrchestraUtils.pathToTextMessages + "repeatSequenceMessage");
            frontalTextMessage.GetComponent<AudioSource>().Play();
        }   
        else
        {
            frontalTextMessage.GetComponent<Text>().text = MagicOrchestraUtils.repeatSequenceReverseMessage;
            frontalTextMessage.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(MagicOrchestraUtils.pathToTextMessages + "repeatSequenceReverseMessage");
            frontalTextMessage.GetComponent<AudioSource>().Play();
        }
        yield return new WaitForSeconds(MagicOrchestraUtils.generalTextTimeShow_long);
        panelMessage.SetActive(false);
        frontalTextMessage.GetComponent<Text>().text = "";
        yield return new WaitForSeconds(MagicOrchestraUtils.generalPauseTime_short);

        this.StopClassCoroutine();
    }

    private IEnumerator FinalCoroutine(bool hasWin)
    {
        panelMessage.SetActive(true);

        float waitTime = MagicOrchestraUtils.generalTextTimeShow_long;

        //Showing user turn information
        if (hasWin)
        {
            // Message
            frontalTextMessage.GetComponent<Text>().text = MagicOrchestraUtils.correctSequenceMessage;
            // Light feedback
            MagicOrchestraUtils.PositiveLightFeedback();
            // First sound
            if(MagicOrchestraParameters.IsContext)
                frontalTextMessage.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(MagicOrchestraUtils.pathToSoundMessages + "sequence_correct_context");
            else
                frontalTextMessage.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(MagicOrchestraUtils.pathToSoundMessages + "sequence_correct");
            frontalTextMessage.GetComponent<AudioSource>().Play();
            waitTime -= frontalTextMessage.GetComponent<AudioSource>().clip.length;
            yield return new WaitForSeconds(frontalTextMessage.GetComponent<AudioSource>().clip.length);
            // Second sound
            frontalTextMessage.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(MagicOrchestraUtils.pathToTextMessages + "correctSequenceMessage");
            frontalTextMessage.GetComponent<AudioSource>().Play();
            waitTime -= frontalTextMessage.GetComponent<AudioSource>().clip.length;
            yield return new WaitForSeconds(frontalTextMessage.GetComponent<AudioSource>().clip.length);
        }
        else
        {
            // Message
            frontalTextMessage.GetComponent<Text>().text = MagicOrchestraUtils.wrongSequenceMessage;
            // Light feedback
            MagicOrchestraUtils.NegativeLightFeedback();
            // First sound
            frontalTextMessage.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(MagicOrchestraUtils.pathToSoundMessages + "sequence_wrong");
            frontalTextMessage.GetComponent<AudioSource>().Play();
            waitTime -= frontalTextMessage.GetComponent<AudioSource>().clip.length;
            yield return new WaitForSeconds(frontalTextMessage.GetComponent<AudioSource>().clip.length);
            // Second sound
            frontalTextMessage.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(MagicOrchestraUtils.pathToTextMessages + "wrongSequenceMessage");
            frontalTextMessage.GetComponent<AudioSource>().Play();
            waitTime -= frontalTextMessage.GetComponent<AudioSource>().clip.length;
            yield return new WaitForSeconds(frontalTextMessage.GetComponent<AudioSource>().clip.length);
        }
        
        yield return new WaitForSeconds(waitTime);
        panelMessage.SetActive(false);
        frontalTextMessage.GetComponent<Text>().text = "";
        yield return new WaitForSeconds(MagicOrchestraUtils.generalPauseTime_short);
        MagicOrchestraUtils.SwitchOffLightFeedback();

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
