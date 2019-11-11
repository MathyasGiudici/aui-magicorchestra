using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorsiController : MonoBehaviour
{
    public static CorsiController singleton;

    string color = "#ff0000";
    float time = 2;
    int[] sequence = { 2, 4 };

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

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Corsi Test is on fire!");
    }

    // Update is called once per frame
    void Update()
    {
        //Instruction to Start Sequence
        if (Input.GetKeyUp(KeyCode.S) == true)
        {
            StartFrontalSequence();
        }
    }

    public void StartFrontalSequence()
    {
        this.DisablePlayerMovement();
        SequenceLighter.singleton.StartSequence(color, time, sequence);
    }

    public void EndFrontalSequence()
    {
        Debug.Log("End of frontal sequence");
        EnablingPlayerMovement();
    }

    private void EnablingPlayerMovement()
    {
        TesterMovement.singleton.isMovementEnabled = true;
    }

    private void DisablePlayerMovement()
    {
        TesterMovement.singleton.isMovementEnabled = false;
    }
}
