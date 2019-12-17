﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game1PanelManager : MonoBehaviour
{
	public void GamePressed()
	{
		Game1Parameters.Difficulty = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<DifficultySlider>().GetCurrentDifficulty();
		Game1Parameters.TimeInShowing = this.gameObject.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<TimeSlider>().GetCurrentTime();

        Game1Parameters.LogMe();
        
		//SceneManager.LoadScene("Instrumap");
	}
}