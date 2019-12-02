using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartButton : MonoBehaviour
{
    public void GameStartPressed()
    {
        // Putting context status
        MagicOrchestraParameters.IsContext = ContextSliderController.singleton.GetContextStatus();
        MagicOrchestraParameters.IsGuided = IndexGuidedSliderController.singleton.GetGuidedStatus();
        MagicOrchestraParameters.GameNumber = IndexesSelecterController.singleton.GetGameNumber();

        if (MagicOrchestraParameters.IsContext && MagicOrchestraParameters.IsGuided)
        {
            Debug.Log("Starting Game 1 - Guided");            
        }
        else
        {
            Debug.Log("Starting Game" + MagicOrchestraParameters.GameNumber.ToString());
        }

    }
}
