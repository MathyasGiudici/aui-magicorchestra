using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesterMovement : MonoBehaviour
{
    //Singleton of the TesterMovement class
    public static TesterMovement singleton = null;

    //Constants
    private const float EPSILON = 0; 

    //Player parameter
    public float speed = 3;
    public bool isMovementEnabled = true;

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
     * Start is called before the first frame update
     * </summary>
     */
    void Start()
    {
    }

    /* <summary>
     * Update is called once per frame
     * </summary>
     */
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
            CorsiController.singleton.UserGesture();
        }
    }
}
