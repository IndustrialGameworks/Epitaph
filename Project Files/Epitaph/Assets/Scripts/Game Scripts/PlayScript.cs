﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayScript : MonoBehaviour 
{
		public void startGame()
		{
			SceneManager.LoadScene ("Test");
		}

		public void exitGame()
		{
			Application.Quit ();
		}
}
