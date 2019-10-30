using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinectMapper : MonoBehaviour {

    public static KinectMapper instance;
    public bool isbinded = false;
    /// <summary>
    /// reference to the star representers of the player position
    /// </summary>
    public GameObject[] pointers;
    /// <summary>
    /// reference of the follower loading bar
    /// </summary>
    public followme[] follower;
    /// <summary>
    /// dimension of the roo to adjust the mapping on the room
    /// </summary>
    public Vector2 roomsize;
    /// <summary>
    /// Sizes to refer to standard measures (test space is 1:1 with adapted sizes, otherzise is the proportion)
    /// </summary>
    Vector2 _AdaptedRoomSize;
    /// <summary>
    /// adjustment masures to cope with disallinment of the kinect
    /// </summary>
    public float shiftX, shiftY;

    // Use this for initialization
    void Start () {
        instance = this;
        if (GameSettings.instance != null) {
            roomsize = new Vector2(GameSettings.instance.screencofiguration.floorsize_X, GameSettings.instance.screencofiguration.floorsize_Y);
            shiftX = GameSettings.instance.screencofiguration.disallignmentX;
            shiftY = GameSettings.instance.screencofiguration.correctionfactorY;
        }


        MagicRoomKinectV2Manager.instance.setUpKinect(5, 1);
        MagicRoomKinectV2Manager.instance.startSamplingKinect(KinectSamplingMode.Streaming);
        //MagicRoomKinectV2Manager.instance.startSamplingKinect(KinectSamplingMode.Polling);
        _AdaptedRoomSize = new Vector2(2.74f/roomsize.x, 2.88f /roomsize.y);


    }
	
	// Update is called once per frame
	void Update () {
        if (!isbinded)
        {
            isbinded = MagicRoomKinectV2Manager.instance.MagicRoomKinectV2Manager_active;
        }
        else {
            //MagicRoomKinectV2Manager.instance.readLastSamplingKinect(KinectReadMode.Single);
            for(int i = 0; i < MagicRoomKinectV2Manager.instance.skeletons.Length; i++)
            {
                if (MagicRoomKinectV2Manager.instance.skeletons[i] != null)
                {
                    Vector3 t = MagicRoomKinectV2Manager.instance.skeletons[i].SpineBase;
                    if (t == Vector3.zero)
                    {
                        if (MagicRoomKinectV2Manager.instance.skeletons[i].FootLeft == Vector3.zero && MagicRoomKinectV2Manager.instance.skeletons[i].HandLeft == Vector3.zero)
                        {
                            pointers[i].SetActive(false);
                        }
                        else
                        {
                            pointers[i].SetActive(true);
                        }
                    }
                    else
                    {
                        pointers[i].SetActive(true);
                    }
                    //map the position from the kinect to the dimension in the real room to the screen coordinates
                    pointers[i].transform.position = new Vector3(t.x * 8 * _AdaptedRoomSize.x + shiftX, 0.1f, -((-6 + shiftY) * _AdaptedRoomSize.y + t.z * 3.5f * _AdaptedRoomSize.y));
                }
            }
        }
	}

    /// <summary>
    /// set thefillamount on the respective loading bar
    /// </summary>
    /// <param name="pointer"></param>
    /// <param name="percentage"></param>
    public void setfillup(GameObject pointer, float percentage) {
        for (int i = 0; i < pointers.Length; i++) {
            if (pointer == pointers[i]) {
                follower[i].insidethecard(percentage);
            }
        }
    }
    /// <summary>
    /// reset the fill of loading bar in the respective bar
    /// </summary>
    /// <param name="pointer"></param>
    public void exitfillup(GameObject pointer)
    {
        for (int i = 0; i < pointers.Length; i++)
        {
            if (pointer == pointers[i])
            {
                follower[i].exitthecard();
            }
        }
    }
}
