using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //Integers.
	public static int gameScore = 0;
    public static int multiplier = 1;
    public int leaderboardScore;

    //Floats.
    public static float timer = 300.0f;
    public static float resetTimer = 300.0f;

    //UI Elements.
    public Text score;
	public Text highscore;
	public Text scoreEnd;
	public Text highScoreEnd;
    public Text scoreMultiplier;
    public Text multi;
    public Button retryButton;
    public Button quitButton;
    public Button leaderboardButton;
    public Image multiplierTimer;

    //Booleans.
    bool highscoreFlash = false;
    bool newHighScore = false;
	string log;
    bool postScore = false;

    //Reference to player.
	public PlayerController player;
	public GameObject playerController;

	// Use this for initialization
	void Start () 
	{ 	
		multiplier = 1;
		gameScore = 0;
		retryButton.gameObject.SetActive (false);
		quitButton.gameObject.SetActive (false);
        leaderboardButton.gameObject.SetActive(false);
        playerController = GameObject.FindGameObjectWithTag ("Player");
		player = playerController.GetComponent<PlayerController> ();
        leaderboardScore = (int)PlayerPrefs.GetFloat("HighScore", 0);

        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            // Note: make sure to add 'using GooglePlayGames'
            PlayGamesPlatform.Instance.ReportScore(leaderboardScore,
                GPGSIds.leaderboard_highscore,
                (bool success) =>
                {
                    Debug.Log("Leaderboard update success: " + success);
                });
        }
    }
	
	// Update is called once per frame
	void Update () 
	{
		timer = timer - 1.0f;
		scoreTracking ();
		restart ();
		resetHighScore ();
		StartCoroutine ("initializeScores");
		endGame ();
		multiplierDegradation ();
        timerBarControl();
	}

	void scoreTracking ()
	{
		log = gameScore.ToString ();

		if (gameScore > PlayerPrefs.GetFloat ("HighScore", 0)) 
		{
			newHighScore = true;
			PlayerPrefs.SetFloat ("HighScore", gameScore);
			highscore.text = "HighScore : " + log;
		}
		multiplier = Mathf.Clamp (multiplier, 1, 10);
	}

	IEnumerator initializeScores ()
	{
		yield return new WaitForSeconds (4f);
		score.text = "score : " + log;
		highscore.text = "HighScore : " + PlayerPrefs.GetFloat("HighScore", 0).ToString();
		scoreMultiplier.text = multiplier + "x";
		multi.text = "Multiplier";
	}

	void restart()//code for restarting the scene.
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
            leaderboardButton.gameObject.SetActive(true);
            retryButton.gameObject.SetActive(true);
            quitButton.gameObject.SetActive(true);
            multi.gameObject.SetActive(false);
            multiplierTimer.gameObject.SetActive(false);
            if (postScore == false)
            {
                if (PlayGamesPlatform.Instance.localUser.authenticated)
                {
                    // Note: make sure to add 'using GooglePlayGames'
                    PlayGamesPlatform.Instance.ReportScore(leaderboardScore,
                        GPGSIds.leaderboard_highscore,
                        (bool success) =>
                        {
                            Debug.Log("Leaderboard update success: " + success);
                        });
                }
                postScore = true;
            }
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
		if (multiplier == 10 && timer <= 0.0f) 
		{
			multiplier = 1;
			timer = resetTimer;
		}
		if (multiplier == 9 && timer <= 0.0f) 
		{
			multiplier = 1;
			timer = resetTimer;
		}
		if (multiplier == 8 && timer <= 0.0f) 
		{
			multiplier = 1;
			timer = resetTimer;
		}
		if (multiplier == 7 && timer <= 0.0f) 
		{
			multiplier = 1;
			timer = resetTimer;
		}
		if (multiplier == 6 && timer <= 0.0f) 
		{
			multiplier = 1;
			timer = resetTimer;
		}
		if (multiplier == 5 && timer <= 0.0f) 
		{
			multiplier = 1;
			timer = resetTimer;
		}
		if (multiplier == 4 && timer <= 0.0f) 
		{
			multiplier = 1;
			timer = resetTimer;
		}
		if (multiplier == 3 && timer <= 0.0f) 
		{
			multiplier = 1;
			timer = resetTimer;
		}
		if (multiplier == 2 && timer <= 0.0f) 
		{
			multiplier = 1;
			timer = resetTimer;
		}
		if (multiplier == 1) 
		{
			timer = resetTimer;
		}
	}

    public void ShowLeaderboardUI()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI();
        }
        else
        {
            Debug.Log("Cannot show leaderboard: not authenticated");
        }
    }

    void timerBarControl()
    {
        float ratio = timer / resetTimer;
        multiplierTimer.rectTransform.localScale = new Vector2(ratio, 1);
        if (timer >= resetTimer)
        {
            timer = resetTimer;
        }
        if (multiplier == 1 || player.isDead == true)
        {
            multiplierTimer.gameObject.SetActive(false);
        }
        else
        {
            multiplierTimer.gameObject.SetActive(true);
        }
    }
}
