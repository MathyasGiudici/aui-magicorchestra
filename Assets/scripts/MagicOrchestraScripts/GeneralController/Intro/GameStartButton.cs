using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartButton : MonoBehaviour
{
    public void GameStartPressed()
    {
        // Putting context status
        MagicOrchestraParameters.IsContext = ContextSliderController.singleton.GetContextStatus();
        MagicOrchestraParameters.IsGuided = GuidedSliderController.singleton.GetGuidedStatus();

        //Checking which panel of settings to start
        if (MagicOrchestraParameters.IsContext && MagicOrchestraParameters.IsGuided)
        {
            //First game settings
            MagicOrchestraParameters.GameNumber = 1;
            CanvasManager.singleton.ShowGameSettingPanel("Game1SettingPanel");
        }
        else
        {
            //Retriving game number
            int gameNumber = IndexesSelecterController.singleton.GetGameNumber();
            MagicOrchestraParameters.GameNumber = gameNumber;

            //Composing game number panel name
            string panelName = "Game" + gameNumber.ToString() +"SettingPanel";
            CanvasManager.singleton.ShowGameSettingPanel(panelName);
        }
    }
}
