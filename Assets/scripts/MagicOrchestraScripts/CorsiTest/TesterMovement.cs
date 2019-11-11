using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesterMovement : MonoBehaviour
{
    private const float EPSILON = 0;

    //Singleton TesterMovement
    public static TesterMovement singleton = null;


    public float speed = 3;
    public bool isMovementEnabled = true;

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
    }

    // Update is called once per frame
    void Update()
    {
        //Tester Pillar movement
        if(isMovementEnabled)
        {
            this.ManageMovement();
            this.ManageGesture();
        }
    }

    private void ManageMovement()
    {
        //Horizontal Translation
        Vector3 direction = new Vector3(0, 0, 0);
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (System.Math.Abs(horizontal) > EPSILON)
            direction.x = horizontal;
        if (System.Math.Abs(vertical) > EPSILON)
            direction.z = vertical;

        //Time.deltaTime serve per gestire la velocità dei frame in base alla potenza di calcolo
        transform.Translate(direction * (speed * Time.deltaTime));
    }

    private void ManageGesture()
    {
        if (Input.GetKeyUp(KeyCode.Space) == true)
        {
            //
        }
    }
}
