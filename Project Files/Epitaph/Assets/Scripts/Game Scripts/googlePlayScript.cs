using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using UnityEngine;
using UnityEngine.UI;

public class googlePlayScript : MonoBehaviour
{
    private Text signInButtonText;
    private Text authStatus;

    private GameObject leaderboardButton;

    public PlayScript playScript;

    public int leaderboardScore;

    // Use this for initialization
    void Start ()
    {
        signInButtonText = GameObject.Find("signInButton").GetComponentInChildren<Text>();
        authStatus = GameObject.Find("authStatus").GetComponent<Text>();

        leaderboardButton = GameObject.Find("leaderboardButton");

        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        // enables saving game progress.
        .EnableSavedGames()
        // registers a callback to handle game invitations received while the game is not running.
        //.WithInvitationDelegate(< callback method >)
        // registers a callback for turn based match notifications received while the
        // game is not running.
        //.WithMatchDelegate(< callback method >)
        // requests the email address of the player be available.
        // Will bring up a prompt for consent.
        //.RequestEmail()
        // requests a server auth code be generated so it can be passed to an
        //  associated back end server application and exchanged for an OAuth token.
        //.RequestServerAuthCode(false)
        // requests an ID token be generated.  This OAuth token can be used to
        //  identify the player to other services such as Firebase.
        //.RequestIdToken()
        .Build();

        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.Instance.Authenticate(SignInCallback, true);//silent sign in if the player has signed in previously

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
        if (PlayGamesPlatform.Instance.localUser.authenticated && playScript.enableLeadeboard == true)
        {
            leaderboardButton.gameObject.SetActive(true);
        }
        else
        {
            leaderboardButton.gameObject.SetActive(false);
        }
    }

    public void SignIn()
    {

        if (!PlayGamesPlatform.Instance.localUser.authenticated)
        {
            // Sign in with Play Game Services, showing the consent dialog
            // by setting the second parameter to isSilent=false.
            Social.localUser.Authenticate((bool success) => {
                SignInCallback(success); //pushes returned bool to be handled by SignInCallback
            }); ;
        }
        else
        {
            // Sign out of play games
            PlayGamesPlatform.Instance.SignOut();


            // Reset UI
            signInButtonText.text = "Sign In";
            authStatus.text = "";
        }
    }

    public void SignInCallback(bool success)
    {
        if (success)
        {
            Debug.Log("Signed in!");

            // Change sign-in button text
            signInButtonText.text = "Sign out";

            // Show the user's name
            authStatus.text = "Signed in as: " + Social.localUser.userName;
        }
        else
        {
            Debug.Log("Sign-in failed...");

            // Show failure message
            signInButtonText.text = "Sign in";
            authStatus.text = "Sign-in failed";
        }
    }

    public void ShowLeaderboard()
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
}
