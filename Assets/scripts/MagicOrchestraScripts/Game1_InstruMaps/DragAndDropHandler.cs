﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropHandler : MonoBehaviour
{
    public Camera cam;
    private bool isRaycasterEnabled = true;
    private bool isMouseDrag = false;

    GameObject target;
    Vector3 screenPosition;
    Vector3 offset;
    Vector3 startingPoint;

    Rect screenRect = new Rect(0, 0, Screen.width, Screen.height);

    private Vector3 dragScale = new Vector3(1.5f, 1.5f, 1.5f);
    private Vector3 oldScale;

    // Singleton of the DragAndDropHandler class
    public static DragAndDropHandler singleton = null;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        DisableRaycaster();
    }

    // Update is called once for frame
    void Update()
    { 
        if (this.isRaycasterEnabled)
        {
            // Select the object to drag
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hitInfo;
                this.target = GetHitTargetObject(out hitInfo);

                if (Game1Parameters.ConfirmSound)
                    this.transform.parent.gameObject.GetComponent<AudioSource>().Play();

                if (this.target != null)
                {
                    this.isMouseDrag = true;
                    this.CallTargetScript();
                    // Debug.Log("target position : " + this.target.transform.position);

                    this.target.transform.position = new Vector3(this.target.transform.position.x, 1.5f, this.target.transform.position.z);
                    
                    this.screenPosition = cam.WorldToScreenPoint(this.target.transform.position);
                    this.offset = this.target.transform.position - cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.screenPosition.z));

                    //this.oldScale = this.target.transform.localScale;
                    //this.target.transform.localScale = this.dragScale;

                    // Debug.Log("screen position is : " + this.screenPosition);
                    // Debug.Log("offst is : " + this.offset);
                }
            }

            // Drop the object
            if (Input.GetMouseButtonUp(0))
            {
                this.isMouseDrag = false;
                if(this.target != null)
                {
                    this.target.transform.position = new Vector3(this.target.transform.position.x, 0, this.target.transform.position.z);
                    //this.target.transform.localScale = this.oldScale;
                }
            }

            // Dragging the object
            if (this.isMouseDrag)
            {
                if (!screenRect.Contains(Input.mousePosition))
                {
                    this.target.transform.position = this.startingPoint;
                    this.target = null;
                }
                else
                {
                    // Track mouse position
                    Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.screenPosition.z);

                    // Convert screen position to world position with offset changes.
                    Vector3 currentPosition = cam.ScreenToWorldPoint(currentScreenSpace) + this.offset;

                    // It will update target gameobject's current postion.
                    this.target.transform.position = currentPosition;
                }
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

        if(hitTarget != null)
            this.startingPoint = hitTarget.transform.position;

        return hitTarget;
    }

    /// <summary>
    /// Enables the possibility to drag objects.
    /// </summary>
    public void EnableRaycaster()
    {
        this.isRaycasterEnabled = true;
    }

    /// <summary>
    /// Disable the dragging function.
    /// </summary>
    public void DisableRaycaster()
    {
        this.isRaycasterEnabled = false;
    }

    /// <summary>
    /// Notify the target that it is no more the dragged object and set the firstCollision to null, to avoid multiple collisions
    /// </summary>
    private void CallTargetScript()
    {
        CollisionDetector script;
        script = (CollisionDetector) this.target.GetComponent(typeof(CollisionDetector));
        script.StopDragOnThis();
    }
}
