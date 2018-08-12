using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public static int gameScore = 0;
	public Text score;
	public Text highscore;
	string log;
	public PlayerController player;
	public Button retryButton;
	public Button quitButton;

	// Use this for initialization
	void Start () 
	{ 	
		gameScore = 0;
		retryButton.gameObject.SetActive (false);
		quitButton.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		DisplayScore ();
		scoreTracking ();
		restart ();
		resetHighScore ();
		StartCoroutine ("initializeScores");
		endGame ();
	}

	void DisplayScore () 
	{
		Debug.Log ("Score " + gameScore);
	}

	void scoreTracking ()
	{
		log = gameScore.ToString ();

		if (gameScore > PlayerPrefs.GetFloat ("HighScore", 0)) 
		{
			PlayerPrefs.SetFloat ("HighScore", gameScore);
			highscore.text = "HighScore : " + log;
		}

	}

	IEnumerator initializeScores ()
	{
		yield return new WaitForSeconds (4f);
		score.text = "score : " + log;
		highscore.text = "HighScore : " + PlayerPrefs.GetFloat("HighScore", 0).ToString(); 	
	}

	void restart()
	{
		if (Input.GetKey (KeyCode.R))
		{
			SceneManager.LoadScene ("Test");
			gameScore = 0;
		}
	}

	void resetHighScore()
	{
		if (Input.GetKey (KeyCode.O))
		{
			PlayerPrefs.SetFloat ("HighScore", 0);
		}
	}

	void endGame()
	{
		if (player.isDead == true)
		{
			retryButton.gameObject.SetActive (true);
			quitButton.gameObject.SetActive (true);
		}
	}
}
