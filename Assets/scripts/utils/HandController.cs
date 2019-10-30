using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandController : MonoBehaviour {
    /// <summary>
    /// reference to the camera on which interact
    /// </summary>
    public Camera frontcamera;
    /// <summary>
    /// reference to the animator for the hand
    /// </summary>
    Animator anim;
    /// <summary>
    /// reference to the collider of the hand
    /// </summary>
    BoxCollider coll;
    /// <summary>
    /// flag to determine if the hand has been closed
    /// </summary>
    public bool handclosed;
    /// <summary>
    /// flag to determine if the hand is the right one or the left one
    /// </summary>
    public bool leftright;

    /// <summary>
    /// reference to the last taken object, so that there is a retainment of the information and can be corrclty managed the flickering of the hand due to kinectimprecisions
    /// </summary>
    public GameObject lasttaken = null;

    /// <summary>
    /// window tostore 20 position of the hand and place it over the movile median position
    /// </summary>
    public Vector3[] positions = new Vector3[20];
    /// <summary>
    /// index of the last updated lement to use the array as a crcular array
    /// </summary>
    int counter = 0;
    /// <summary>
    /// last value read from the kinect
    /// </summary>
    Vector3 lastread;

	// Use this for initialization
	void Start () {
        handclosed = false;
        anim = transform.GetChild(0).GetComponent<Animator>();
        coll = transform.GetComponent<BoxCollider>();
        coll.enabled = false;
        MagicRoomKinectV2Manager.instance.setUpKinect(5, 1);
        MagicRoomKinectV2Manager.instance.startSamplingKinect(KinectSamplingMode.Streaming);
    }
	
	// Update is called once per frame
	void Update () {
        transform.GetChild(0).GetComponent<Image>().color = Color.white;
        if ((Input.GetKeyUp(KeyCode.PageUp) || HttpListenerForMagiKRoom.skip)){
            //force the comamnd of release in the correct position
            if (transform.childCount > 1)
            {
                //the last object taken is the child of the hand
                lasttaken = null;
            }
            else {
                //the last object taken is saved into lasttaken but no morechild
                lasttaken = null;
            }
            HttpListenerForMagiKRoom.skip = false;
        }

        if (HttpListenerForMagiKRoom.next) {
            //force the comamnd of release in the correct position with reward
            if (transform.childCount > 1)
            {
                //force the behaviour on the last selected object which is still the child
            }
            else {
                if (lasttaken != null)
                {
                    //force the command but the last selected object is no more the child
                    lasttaken = null;
                }
            }
            HttpListenerForMagiKRoom.next = false;
        }

        if ((Input.GetKeyUp(KeyCode.PageDown)) && handclosed) 
        {
            //forcibly revert the hand position
            anim.SetTrigger("open");
            handclosed = false;
            coll.enabled = false;
            if (transform.childCount > 1 && transform.position.y > -2)
            {
                //open forcibly the hand and reset the children
                StartCoroutine(waitforgetting());
            }
        }
        if (Input.GetKeyUp(KeyCode.PageDown) && !handclosed)
        {
            //forcibly revert the hand position
            anim.SetTrigger("close");
            handclosed = true;
            coll.enabled = true;
        }
        if (!MagicRoomKinectV2Manager.instance.MagicRoomKinectV2Manager_active)
        {
            //use the mouse thìo mime the right hand and switch off the left one
            if (leftright)
            {
                gameObject.SetActive(false);
            }
            transform.position = frontcamera.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 10; ;
            if (Input.GetMouseButtonUp(0))
            {
                if (handclosed)
                {
                    anim.SetTrigger("open");
                    handclosed = false;
                    coll.enabled = false;
                    if (transform.childCount > 1 && transform.position.y > -2)
                    {
                        StartCoroutine(waitforgetting());
                    }
                    else {
                        lasttaken = null;
                    }
                }
                else
                {
                    anim.SetTrigger("close");
                    handclosed = true;
                    coll.enabled = true;
                }
            }
        }
        else {
            //use the kienct to read the position of the hands
            for (int i = 0; i < MagicRoomKinectV2Manager.instance.skeletons.Length; i++)
            {
                //avoid the null skeletons
                if (MagicRoomKinectV2Manager.instance.skeletons[i].SpineBase != Vector3.zero)
                {
                    if (leftright)
                    {
                        //update the circualr array
                        positions[counter] = MagicRoomKinectV2Manager.instance.skeletons[i].HandLeft;
                        counter = (counter + 1) % positions.Length;

                        Vector3 lefthand = Vector3.zero;
                        for (int k = 0; k < positions.Length; k++) {
                            lefthand += positions[k];
                        }
                        lefthand = lefthand / positions.Length;
                        //avoid that the hand is too low below the screen (basically useless, is in revision to make better adaptation)
                        if (lefthand.y > -1000000)
                        {
                            transform.position = convertSkeletonToWorldPosition(lefthand) + Vector3.forward * 100;
                            if (!MagicRoomKinectV2Manager.instance.skeletons[i].isLeftHandClosed())
                            {
                                //hand has been closed
                                if (handclosed)
                                {
                                    anim.SetTrigger("open");
                                    handclosed = false;
                                    coll.enabled = false;
                                    if (transform.childCount > 1 && transform.position.y > -2)
                                    {
                                        StartCoroutine(waitforgetting());
                                    }
                                }
                            }
                            else
                            {
                                //open the hand
                                if (!handclosed)
                                {
                                    anim.SetTrigger("close");
                                    handclosed = true;
                                    coll.enabled = true;
                                }
                            }
                        }
                        else {
                            //rset things because the hand has been to outse the border (suposedly an error in the system)
                            Debug.Log("lefthand out of border");
                            anim.SetTrigger("open");
                            anim.ResetTrigger("open");
                            anim.ResetTrigger("close");
                            coll.enabled = false;
                            handclosed = false;
                            coll.enabled = false;

                            if (transform.childCount > 1)
                            {
                                transform.GetChild(1).GetComponent<Image>().color = Color.clear;
                                StartCoroutine(waitforgetting());
                            }
                            else {
                                transform.GetChild(0).GetComponent<Image>().color = Color.clear;
                            }
                        }
                    }
                    else
                    {
                        //do the same thing fort he right hand
                        lastread = MagicRoomKinectV2Manager.instance.skeletons[i].HandRight;
                        positions[counter] = MagicRoomKinectV2Manager.instance.skeletons[i].HandRight;
                        counter = (counter + 1) % positions.Length;

                        Vector3 righthand = Vector3.zero;
                        for (int k = 0; k < positions.Length; k++)
                        {
                            righthand += positions[k];
                        }
                        righthand = righthand / positions.Length;
                        if (righthand.y > -1000000)//MagicRoomKinectV2Manager.instance.skeletons[i].SpineBase.y)
                        {
                            transform.position = convertSkeletonToWorldPosition(righthand) + Vector3.forward * 100;

                            if (!MagicRoomKinectV2Manager.instance.skeletons[i].isRightHandClosed())
                            {
                                if (handclosed)
                                {
                                    anim.SetTrigger("open");
                                    handclosed = false;
                                    coll.enabled = false;
                                    if (transform.childCount > 1 && transform.position.y > -2)
                                    {
                                        StartCoroutine(waitforgetting());
                                    }
                                }
                            }
                            else
                            {
                                if (!handclosed)
                                {
                                    anim.SetTrigger("close");
                                    handclosed = true;
                                    coll.enabled = true;
                                }
                            }
                        }
                        else
                        {
                            Debug.Log("righthand out of border");
                            anim.SetTrigger("open");
                            anim.ResetTrigger("open");
                            anim.ResetTrigger("close");
                            coll.enabled = false;
                            handclosed = false;
                            coll.enabled = false;
                            if (transform.childCount > 1)
                            {
                                transform.GetChild(1).GetComponent<Image>().color = Color.clear;
                                StartCoroutine(waitforgetting());
                            }
                            else
                            {
                                transform.GetChild(0).GetComponent<Image>().color = Color.clear;
                            }
                        }
                    }
                    break;
                }
            }


            
        }
	}

    /// <summary>
    /// reset the animators
    /// </summary>
    public void resetTrigger() {

        anim.ResetTrigger("open");
        anim.ResetTrigger("close");
    }

    /// <summary>
    /// perform the conversion between screen and kinect position
    /// </summary>
    /// <param name="skeletonposition"></param>
    /// <returns></returns>
    Vector3 convertSkeletonToWorldPosition(Vector3 skeletonposition) {
        Vector3 result = new Vector3();
        result.x = skeletonposition.x * GameSettings.instance.screencofiguration.magnfier_X;
        result.y = skeletonposition.y * GameSettings.instance.screencofiguration.magnfier_Y - GameSettings.instance.screencofiguration.correctionfactorforscreenplacement;
        result.z = 0;
        return result;
    }
    /// <summary>
    /// reset the colour to white
    /// </summary>
    /// <returns></returns>
    IEnumerator waitofflight() {
        yield return new WaitForSeconds(3);
        Color c = Color.white;
        c.a = GameSettings.instance.screencofiguration.brighnessmax / 255f;
        MagicRoomLightManager.instance.sendColour(c);
    }
    /// <summary>
    /// reset the last taken object to avoid mistakenly select a thing as correctted
    /// </summary>
    /// <returns></returns>
    IEnumerator waitforgetting()
    {
        yield return new WaitForSeconds(3);
        lasttaken = null;
    }
}
