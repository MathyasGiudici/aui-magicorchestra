using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //Singleton of the GameController class
    public static GameController singleton = null;

    //Private setting variables
    private int[] sequence = { 5, 8, 2 };
    private float showTime = 3.0f;
    private bool isReverse = true;

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
     * Start is called before the first frame update
     * </summary>
     */
    void Start()
    {
        Debug.Log("Game2 is on fire!");
    }

    /* <summary>
    * Update is called once per frame
    * </summary>
    */
    void Update()
    {
        //Instruction to Start Sequence
        if (Input.GetKeyUp(KeyCode.S) == true)
        {
            singleton.StartFrontalPhase(this.sequence,this.showTime,this.isReverse);
        }
    }

    /* <summary>
    * StartFrontalSequence manage the first phase of the game
    * </summary>
    *
    * <param name="isReverse"> bool that represents if the user has to repeat the sequence in the reverse mode or not </param>
    * <param name="showTime"> time must be a float number that represents the number of seconds the number is shown </param>
    * <param name="sequence"> sequence is an array of numbers representing the numbers' sequence </param>
    * <remarks> sequence must contain the numbers and NOT as indexes </remarks>
    */
    public void StartFrontalPhase(int[] sequence, float showTime, bool isReverse)
    {
        SequenceShower.singleton.Show(sequence, showTime, isReverse);
    }

    /* <summary>
     * EndFrontalSequence is called when the first phase is ended
     * </summary>
     */
    public void EndFrontalPhase(int[] sequence, bool isReverse)
    {
        Debug.Log("End of frontal sequence");
        gameObject.AddComponent<UserDigitSpan>();
        UserDigitSpan.singleton.Init(sequence, isReverse);
    }

    public void CorrectUserSequence()
    {
        Debug.Log("Sequence reproduced by the user correctly");
        SequenceShower.singleton.StopClassCoroutine();
    }

    public void WrongUserSequence()
    {
        Debug.Log("Sequence reproduced by the user NOT correctly");
        SequenceShower.singleton.StopClassCoroutine();
        singleton.StartFrontalPhase(this.sequence, this.showTime, this.isReverse);
    }
}
