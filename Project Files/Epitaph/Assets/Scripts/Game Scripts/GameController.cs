using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public static int gameScore = 0;
	public Text score;
	public Text highscore;
	string log;

	// Use this for initialization
	void Start () 
	{
		highscore.text = "HighScore : " + PlayerPrefs.GetFloat("HighScore", 0).ToString(); 	
	}
	
	// Update is called once per frame
	void Update () 
	{
		DisplayScore ();
		score.text = "score : " + log;
		scoreTracking ();
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
}
