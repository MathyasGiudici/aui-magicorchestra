using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinectLeftHand : MonoBehaviour
{
    public Camera cam;
    private bool isRaycasterEnabled = true;
    private bool isDrag = false;

    GameObject target;
    Vector3 screenPosition;
    Vector3 offset;

    //Room parameters
    public Vector2 roomsize = new Vector2(2.74f, 2.88f);
    private Vector2 _AdapterRoomSize;
    private Transform tr;

    //Calibration parameters
    public float shiftX = 0f;
    public float shiftZ = 1f;

    public float multX = 1f;
    public float multZ = 1f;

    // Singleton of the KinectRightHand class
    public static KinectLeftHand singleton = null;

    /* <summary>
     * The function is called when the component is instantiated
     * </summary>
     */
    private void Awake()
    {
        if ((singleton != null) && (singleton != this))
        {
            Destroy(this);
            return;
        }
        singleton = this;
        tr = GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // roomsize = new Vector2(GameSettings.instance.screencofiguration.floorsize_X, GameSettings.instance.screencofiguration.floorsize_Y);
        MagicRoomKinectV2Manager.instance.setUpKinect(5, 1);
        MagicRoomKinectV2Manager.instance.startSamplingKinect(KinectSamplingMode.Streaming);
        _AdapterRoomSize = new Vector2(2.74f / roomsize.x, 2.88f / roomsize.y);
        DisableRaycaster();
    }

    // Update is called once for frame
    void Update()
    {
        if (this.isRaycasterEnabled)
        {
            // Looking to the nearest skelpos
            KinectBodySkeleton skel = MagicOrchestraUtils.GetNearestSkeleton();

            if (skel == null)
            {
                Debug.Log("No MagicRoomKinectV2Manager founded");
                return;
            }

            // Computing new Vector3 position
            // tr.position = new Vector3(skelpos.x * 8 * _AdapterRoomSize.x + shiftX, gameObject.transform.position.y, (-6 + shiftY) * -_AdapterRoomSize.y - skelpos.z * 3.5f * _AdapterRoomSize.y);
            tr.position = new Vector3(multX * skel.HandLeft.x * _AdapterRoomSize.x + shiftX, gameObject.transform.position.y, multZ * skel.HandLeft.z + shiftZ);

            // Moving pillar on the game
            gameObject.transform.position = tr.position;

            // Select the object to drag
            if (skel.isLeftHandClosed())
            {
                RaycastHit hitInfo;
                this.target = GetHitTargetObject(out hitInfo);

                if (this.target != null)
                {
                    this.isDrag = true;
                    // Debug.Log("target position : " + this.target.transform.position);

                    this.target.transform.position = new Vector3(this.target.transform.position.x, 0.7f, this.target.transform.position.z);

                    this.screenPosition = cam.WorldToScreenPoint(this.target.transform.position);
                    this.offset = this.target.transform.position - cam.ScreenToWorldPoint(new Vector3(tr.position.x, tr.position.y, this.screenPosition.z));

                    // Debug.Log("screen position is : " + this.screenPosition);
                    // Debug.Log("offst is : " + this.offset);
                }
            }

            // Drop the object
            if (!skel.isLeftHandClosed())
            {
                this.isDrag = false;
                if (this.target != null)
                {
                    this.target.transform.position = new Vector3(this.target.transform.position.x, 0, this.target.transform.position.z);
                }
            }

            // Dragging the object
            if (this.isDrag)
            {
                // Track mouse position
                Vector3 currentScreenSpace = new Vector3(tr.position.x, tr.position.y, this.screenPosition.z);

                // Convert screen position to world position with offset changes.
                Vector3 currentPosition = cam.ScreenToWorldPoint(currentScreenSpace) + this.offset;

                // It will update target gameobject's current postion.
                this.target.transform.position = currentPosition;
            }
        }
    }

    /// <summary>
    /// Retrieve the hit object when a ray is cast.
    /// </summary>
    /// <param name="hit"></param>
    /// <returns> GameObject that has been hit </returns>
    public GameObject GetHitTargetObject(out RaycastHit hit)
    {
        GameObject hitTarget = null;

        Ray lastRay = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(lastRay, out hit))
        {
            // Debug.Log("Collided with " + hit.collider.gameObject.name);
            hitTarget = hit.collider.gameObject;
        }
        return hitTarget;
    }

    /// <summary>
    /// Enables the possibility to drag objects.
    /// </summary>
    public void EnableRaycaster()
    {
        this.isRaycasterEnabled = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    /// <summary>
    /// Disable the dragging function.
    /// </summary>
    public void DisableRaycaster()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        this.isRaycasterEnabled = false;
    }
}
