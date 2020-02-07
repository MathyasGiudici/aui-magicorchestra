using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstruMapsController : MonoBehaviour
{
    // Singleton of the InstruMapsController class
    public static InstruMapsController singleton = null;

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
    }
    
    
    /// <summary>
    /// Start the game with the first phase in which the user has to visualize and memorize
    /// the disposition of the objects in the arena.
    /// </summary>
    public void WatchArenaPhase()
    {
        Game1PhasesManager.singleton.ShowArenaDisposition();
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


    /// <summary>
    /// Pause the game: this will allow the therapist to better control the game phases 
    /// </summary>
    public void PauseGame()
    {
        Game1PhasesManager.singleton.PauseGame();
    }

    /// <summary>
    /// Resume the game from a pause state
    /// </summary>
    public void ResumeGame()
    {
        Game1PhasesManager.singleton.ResumeGame();
    }


    /// <summary>
    /// Decision point after the end of the game
    /// </summary>
    public void EndGame()
    {
        if (MagicOrchestraParameters.GuidedOnPlay)
        {
            MagicOrchestraParameters.LastGamePlayed += 1;
        }

        SceneManager.LoadScene("MagicOrchestra");
    }

    
}
