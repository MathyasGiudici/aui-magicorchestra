using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game3Player : MonoBehaviour
{
    //Singleton of the TesterMovement class
    public static Game3Player singleton = null;

    //Constants
    private const float EPSILON = 0;

    //Player parameter
    public bool isMovementEnabled = true;

    public float speed = 5.0f;
    public Vector2 roomsize = new Vector2(2.74f, 2.88f);
    private float shiftX = 12.75f, shiftY = 0;
    private Vector2 _AdapterRoomSize;
    private Transform tr;

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
        tr = GetComponent<Transform>();
    }

    /* <summary>
     * Start is called before the first frame update
     * </summary>
     */
    void Start()
    {
        // roomsize = new Vector2(GameSettings.instance.screencofiguration.floorsize_X, GameSettings.instance.screencofiguration.floorsize_Y);
        MagicRoomKinectV2Manager.instance.setUpKinect(5, 1);
        MagicRoomKinectV2Manager.instance.startSamplingKinect(KinectSamplingMode.Streaming);
        _AdapterRoomSize = new Vector2(2.74f / roomsize.x, 2.88f / roomsize.y);
    }

    /* <summary>
     * Update is called once per frame
     * </summary>
     */
    void Update()
    {
        // If movement not on, i do not care about position
        if (!isMovementEnabled)
            return;

        // Looking for skeletons
        if (MagicRoomKinectV2Manager.instance.MagicRoomKinectV2Manager_active)
        {
            // User position part
            Vector3 skelpos = Vector3.zero;
            foreach (KinectBodySkeleton skel in MagicRoomKinectV2Manager.instance.skeletons)
            {
                // Position part
                if (skel != null && skel.SpineBase != Vector3.zero && (skelpos.z == 0 || skelpos.z > skel.SpineBase.z))
                {
                    skelpos = skel.SpineBase;

                    // Gesture part
                    if (skel.FootLeft != Vector3.zero && skel.FootRight != Vector3.zero)
                    {
                        // Retrieving measures
                        float deltaRight = Mathf.Abs(skel.HandRight.z) - Mathf.Abs(skel.FootRight.z);
                        float deltaLeft = Mathf.Abs(skel.HandLeft.z) - Mathf.Abs(skel.FootLeft.z);
                        float deltaFeetHands = Mathf.Max(deltaLeft, deltaRight);

                        Debug.Log("deltaFeetHands: " + deltaFeetHands);

                        // Checking if player is trying a gesture
                        return;
                        if (deltaFeetHands < 0.1)
                        {
                            Debug.Log("User gesture detected");
                            CorsiController.singleton.UserGesture();
                        }   
                    }     
                }
            }
            tr.position = new Vector3(skelpos.x * 8 * _AdapterRoomSize.x + shiftX, gameObject.transform.position.y, (-6 + shiftY) * -_AdapterRoomSize.y - skelpos.z * 3.5f * _AdapterRoomSize.y);

            // Moving pillar on the game
            gameObject.transform.position = tr.position;
        }
    }
}

