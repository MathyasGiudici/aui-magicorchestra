﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game1PanelManager : MonoBehaviour
{
	public void GamePressed()
	{
		Game1Parameters.Difficulty = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<DifficultyGame1>().GetCurrentDifficulty();
		Game1Parameters.TimeInShowing = this.gameObject.transform.GetChild(2).GetChild(0).GetChild(1).GetComponent<TimeSlider>().GetCurrentTime();

		SceneManager.LoadScene("InstruMaps");
	}
}