using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstruMapsController : MonoBehaviour
{
    // Singleton of the InstruMapsController class
    public static InstruMapsController singleton = null;

    // To know id the game is started or not
    private bool isGameStarted = false;

    // Protected parameters that contains the main custom parameters of the game
    float timeInShowing = 5;
    bool isGestureMode = false;

    /* <summary>
     * The function is called when the component is instantiated
     * </summary>
     */
    private void Awake()
    {
        if((singleton != null) && (singleton != this)){
            Destroy(this);
            return;
        }
        singleton = this;
    }
       

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starting InstruMaps execution...");

        this.timeInShowing = Game1Parameters.TimeInShowing;
        //new WaitForSeconds(5.0f);
    }
    
    
    /// <summary>
    /// Start the game with the first phase in which the user has to visualize and memorize
    /// the disposition of the objects in the arena.
    /// </summary>
    public void WatchArenaPhase()
    {
        Game1PhasesManager.singleton.ShowArenaDisposition(Game1Parameters.Difficulty);
    }


    /// <summary>
    /// Stop the watch arena phase
    /// </summary>
    public void EndWatchArenaPhase()
    {
        InstruMapsCanvasController.singleton.DecisionPoint();
    }


    /// <summary>
    /// Start the user phase in which he/she has to reorder the items inside the arena.
    /// </summary>
    public void StartUserPhase()
    {
        Game1PhasesManager.singleton.ReorderItems();
    }

    
}
