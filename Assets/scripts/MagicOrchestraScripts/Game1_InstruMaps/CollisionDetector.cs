using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private bool isColliderActive = false;
    private GameObject targetSlice;
    private Vector3 wrongDragPosition;
    private Vector3 correctDragPosition;

    private GameObject firstCollision;

    /// <summary>
    /// Enables the detection of collisions
    /// </summary>
    public void EnableCollisionDetector()
    {
        this.isColliderActive = true;
    }

    /// <summary>
    /// Disable the detection of collisions
    /// </summary>
    public void DisableCollisionDetector()
    {
        this.isColliderActive = false;
    }

    /// <summary>
    /// Retrieve the slice assigned to this object
    /// </summary>
    public void AssignTheTargetSlice()
    {
        foreach (ObjectSliceCouple couple in ArenaObjectsHandler.singleton.objectSliceCouples)
        {
            if (couple.arenaObject == gameObject)
            {
                this.targetSlice = couple.slice;
                this.wrongDragPosition = couple.dragAndDropPosition;
                this.correctDragPosition = couple.arenaPosition;
                break;
            }
        }
    }

    /// <summary>
    /// Detect the collision between the object and the slice
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        if (this.isColliderActive)
        {
            if(this.isFirstCollision(other.gameObject))
            {
                if (this.targetSlice == other.gameObject)
                {
                    Debug.Log(gameObject.name + " collided with the CORRECT slice");

                    gameObject.transform.position = this.correctDragPosition;
                    this.DisableCollisionDetector();

                    // Disable raycast on this object changing the layer
                    gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                    Game1PhasesManager.singleton.UpdateCurrentScore();
                    firstCollision = null;
                }
                else
                {
                    Debug.Log(gameObject.name + " collided with the WRONG slice");
                    gameObject.transform.position = this.wrongDragPosition;
                }
            }
        }            
    }

    /// <summary>
    /// Check if the collision is the first  
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    private bool isFirstCollision(GameObject other)
    {
        if (firstCollision == null)
        {
            firstCollision = other;
            return true;
        }
        else
        {
            return false;
        }
    }


    /// <summary>
    /// Set firstCollision to null to avoid multiple collision
    /// </summary>
    public void StopDragOnThis()
    {
        firstCollision = null;
    }

}
