using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public static int gameScore = 0;
	public Text score;
	public Text highscore;
	public Text scoreEnd;
	public Text highScoreEnd;
	public static int multiplier = 1;
	bool newHighScore = false;
	public Text scoreMultiplier;
	string log;
	public PlayerController player;
	public Button retryButton;
	public Button quitButton;
	bool highscoreFlash = false;


	public static float timer = 180.0f;

	// Use this for initialization
	void Start () 
	{ 	
		multiplier = 1;
		gameScore = 0;
		retryButton.gameObject.SetActive (false);
		quitButton.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () 
	{
//		DisplayScore ();
//		displayMultiplier ();
		timer = timer - 1.0f;
		Debug.Log (timer.ToString ());
		scoreTracking ();
		restart ();
		resetHighScore ();
		StartCoroutine ("initializeScores");
		endGame ();
		multiplierDegradation ();
	}

//	void DisplayScore () 
//	{
//		Debug.Log ("Score " + gameScore);
//	}

	void scoreTracking ()
	{
		log = gameScore.ToString ();

		if (gameScore > PlayerPrefs.GetFloat ("HighScore", 0)) 
		{
			newHighScore = true;
			PlayerPrefs.SetFloat ("HighScore", gameScore);
			highscore.text = "HighScore : " + log;
		}
		multiplier = Mathf.Clamp (multiplier, 1, 5);
	}

	IEnumerator initializeScores ()
	{
		yield return new WaitForSeconds (4f);
		score.text = "score : " + log;
		highscore.text = "HighScore : " + PlayerPrefs.GetFloat("HighScore", 0).ToString();
		scoreMultiplier.text = multiplier.ToString();
	}

	void restart()
	{
		if (Input.GetKey (KeyCode.R))
		{
			SceneManager.LoadScene ("Test");
			gameScore = 0;
		}
		if (Input.GetKey (KeyCode.Escape)) 
		{
			Application.Quit ();
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
			if (newHighScore == true) 
			{
				scoreEnd.text = "New HighScore";
				if (highscoreFlash == false)
				{
					StartCoroutine ("flashNewHighScore");
					highscoreFlash = true;
				}
				score.gameObject.SetActive (false);
				highScoreEnd.text = highscore.text;
				highscore.gameObject.SetActive (false);
				scoreMultiplier.gameObject.SetActive (false);
			}
			else 
			{
				scoreEnd.text = score.text;
				score.gameObject.SetActive (false);
				highScoreEnd.text = highscore.text;
				highscore.gameObject.SetActive (false);
				scoreMultiplier.gameObject.SetActive (false);
			}
		}
	}

	IEnumerator flashNewHighScore()
	{
		while (true) 
		{
			yield return new WaitForSeconds (1f);
			scoreEnd.gameObject.SetActive (false);
			yield return new WaitForSeconds (0.5f);
			scoreEnd.gameObject.SetActive (true);
		}
	}

	void multiplierDegradation ()
	{
		if (multiplier == 5 && timer <= 0.0f) 
		{
			multiplier = 1;
			timer = 180.0f;
		}
		if (multiplier == 4 && timer <= 0.0f) 
		{
			multiplier = 1;
			timer = 180.0f;
		}
		if (multiplier == 3 && timer <= 0.0f) 
		{
			multiplier = 1;
			timer = 180.0f;
		}
		if (multiplier == 2 && timer <= 0.0f) 
		{
			multiplier = 1;
			timer = 180.0f;
		}
		if (multiplier == 1) 
		{
			timer = 180.0f;
		}
	}

//	void displayMultiplier ()
//	{
//		Debug.Log ("multiplier: " + multiplier);
//	}
}
