using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game1ContextManager : MonoBehaviour
{
    //Singleton of the CorsiContextManager class
    public static Game1ContextManager singleton = null;

    public GameObject contextizeObject;
    public GameObject zenithCanvas;

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
        contextizeObject.SetActive(true);
        zenithCanvas.SetActive(false);
    }
}
