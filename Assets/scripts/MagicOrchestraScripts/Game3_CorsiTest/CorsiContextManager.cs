using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorsiContextManager : MonoBehaviour
{
    //Singleton of the CorsiContextManager class
    public static CorsiContextManager singleton = null;

    public GameObject plane;
    public GameObject generalCanvas;

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
        if (MagicOrchestraParameters.IsContext)
        {
            PutContextCurtain();
        }
    }

    private void PutContextCurtain()
    {
        generalCanvas.SetActive(true);
        if(!Game3Parameters.ShowPlane)
            plane.GetComponent<MeshRenderer>().enabled = false;
    }
}
