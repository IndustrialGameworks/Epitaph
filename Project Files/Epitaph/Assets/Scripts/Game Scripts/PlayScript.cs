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

	public Button selectCircle;
	public Button selectTriangle;
	public Button selectSquare;

	void Start ()
	{
		Debug.Log(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
		if (versionText != null) 
		{
			versionText.text = "Version: " + System.Reflection.Assembly.GetExecutingAssembly ().GetName ().Version.ToString ();
		}
		PlayerPrefs.GetInt ("playerType", 1);
	}

	void Update ()
	{
		enable ();
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

	public void SpawnCircle()
	{
		PlayerPrefs.SetInt ("playerType", 1);
		Debug.Log ((PlayerPrefs.GetInt ("playerType").ToString ()));
	}

	public void SpawnTriangle ()
	{
		PlayerPrefs.SetInt ("playerType", 2);
		Debug.Log ((PlayerPrefs.GetInt ("playerType").ToString ()));
	}

	public void SpawnSquare ()
	{
		PlayerPrefs.SetInt ("playerType", 3);
		Debug.Log ((PlayerPrefs.GetInt ("playerType").ToString ()));
	}

	public void enable()
	{
		if (selectCircle != null && selectSquare != null && selectTriangle != null)
		{
			if (PlayerPrefs.GetInt ("playerType") == 1)
			{
				selectCircle.gameObject.SetActive (false);
				selectTriangle.gameObject.SetActive (true);
				selectSquare.gameObject.SetActive (true);
			}
			else if (PlayerPrefs.GetInt ("playerType") == 2) 
			{
				selectCircle.gameObject.SetActive (true);
				selectTriangle.gameObject.SetActive (false);
				selectSquare.gameObject.SetActive (true);
			} 
			else if (PlayerPrefs.GetInt ("playerType") == 3) 
			{
				selectCircle.gameObject.SetActive (true);
				selectTriangle.gameObject.SetActive (true);
				selectSquare.gameObject.SetActive (false);
			}
		}
	}
}
