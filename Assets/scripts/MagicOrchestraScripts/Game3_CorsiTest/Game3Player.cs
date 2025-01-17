﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game3Player : MonoBehaviour
{
    //Singleton of the TesterMovement class
    public static Game3Player singleton = null;

    //Player parameter
    public bool isMovementEnabled = true;

    //Room parameters
    public Vector2 roomsize = new Vector2(2.74f, 2.88f);
    private Vector2 _AdapterRoomSize;
    private Transform tr;

    //Calibration parameters
    public float shiftX = 12.75f;
    public float shiftY = -1.53f;

    public float multX = 1.8f;
    public float multY = 3.2f;

    //Internal useful variables
    private bool lastGestureDetected = false;

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

        // Looking to the nearest skelpos
        KinectBodySkeleton skel = MagicOrchestraUtils.GetNearestSkeleton();

        if (skel == null)
        {
            Debug.Log("No MagicRoomKinectV2Manager founded");
            return;
        }

        Vector3 skelpos = skel.SpineBase;

        // Computing new Vector3 position
        //tr.position = new Vector3(skelpos.x * 8 * _AdapterRoomSize.x + shiftX, gameObject.transform.position.y, (-6 + shiftY) * -_AdapterRoomSize.y - skelpos.z * 3.5f * _AdapterRoomSize.y);
        tr.position = new Vector3(multX * skelpos.x * 8 * _AdapterRoomSize.x + shiftX, gameObject.transform.position.y, (-6 + shiftY) * -_AdapterRoomSize.y * multY - skelpos.z * 3.5f * _AdapterRoomSize.y * multY);

        // Moving pillar on the game
        gameObject.transform.position = tr.position; 
    }
}

