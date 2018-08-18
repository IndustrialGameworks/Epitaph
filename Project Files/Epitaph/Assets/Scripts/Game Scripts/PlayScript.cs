using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[assembly: System.Reflection.AssemblyVersion("0.0.2.1")]

public class PlayScript : MonoBehaviour 
{
	public GameController game;
	public Text versionText;

	void Start ()
	{
		Debug.Log(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
		if (versionText != null) 
		{
			versionText.text = "Version: " + System.Reflection.Assembly.GetExecutingAssembly ().GetName ().Version.ToString ();
		}
	}

	public void startGame()
	{
		SceneManager.LoadScene ("Test");
	}

	public void restart()
	{
		SceneManager.LoadScene ("Test");
	}

	public void exitGame()
	{
		Application.Quit ();
	}
}
