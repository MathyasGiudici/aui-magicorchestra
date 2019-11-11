using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZenithCubeController : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(gameObject.name + "collision with: " + collision.gameObject.name);
    }
}
