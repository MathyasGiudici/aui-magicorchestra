using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private bool isColliderActive = false;
    private ArrayList targetSlices = new ArrayList();
    private Vector3 dragAndDropPosition;
    private Vector3 arenaPosition;

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
                this.targetSlices.Add(couple.slice);
                this.dragAndDropPosition = couple.dragAndDropPosition;
                this.arenaPosition = couple.arenaPosition;
            }
        }

        foreach (GameObject slice in targetSlices)
        {
            Debug.Log(gameObject.name + "'s slice is: " + slice.name);
        }
        Debug.Log(gameObject.name + "'s drag and drop position is: " + this.dragAndDropPosition);
        Debug.Log(gameObject.name + "'s arena position is: " + this.arenaPosition);
    }


    /// <summary>
    /// Check if the hit slice is in the target slice arraylist
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    private bool isSliceTarget(GameObject other)
    {
        foreach (GameObject slice in this.targetSlices)
        {
            if (other.gameObject == slice)
            {
                return true;
            }
        }
        return false;
    }


    /// <summary>
    /// Set firstCollision to null to avoid multiple collision
    /// </summary>
    public void StopDragOnThis()
    {
        firstCollision = null;
    }


    /// <summary>
    /// The method is called when a collision is detected. It records the first collision happened (from the smaller slices to the bigger)
    /// and check if the slice is correct or not, handling the following steps.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (this.isColliderActive)
        {
            if (this.firstCollision == null)
            {
                this.firstCollision = other.gameObject;

                Debug.Log(gameObject.name + " collided with " + other.gameObject.name);

                if (this.isSliceTarget(other.gameObject))
                {
                    // Debug.Log(gameObject.name + " collided with the CORRECT slice");

                    gameObject.transform.position = this.arenaPosition;
                    this.DisableCollisionDetector();

                    StartCoroutine(correctAnswerCoroutine());

                    // Disable raycast on this object changing the layer
                    gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                    Game1PhasesManager.singleton.UpdateCurrentScore();
                }
                else
                {
                    // Debug.Log(gameObject.name + " collided with the WRONG slice");
                    gameObject.transform.position = this.dragAndDropPosition;
                    StartCoroutine(wrongAnswerCoroutine());
                }
            } 
        }
    }


    IEnumerator correctAnswerCoroutine()
    {
        MagicOrchestraUtils.PositiveLightFeedback();
        yield return new WaitForSeconds(0.5f);
        MagicOrchestraUtils.SwitchOffLightFeedback();
    }

    IEnumerator wrongAnswerCoroutine()
    {
        MagicOrchestraUtils.NegativeLightFeedback();
        yield return new WaitForSeconds(0.5f);
        MagicOrchestraUtils.SwitchOffLightFeedback();
    }


}
