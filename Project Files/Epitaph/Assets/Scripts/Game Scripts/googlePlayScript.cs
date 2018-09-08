using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using UnityEngine;
using UnityEngine.UI;

//This script controls and activates google play services for this game.
public class googlePlayScript : MonoBehaviour
{
    //Ints.
    public int leaderboardScore;

    //UI.
    private Text signInButtonText;
    private Text authStatus;

    //Gameobjects.
    private GameObject leaderboardButton;

    //Scripts.
    public PlayScript playScript;

    // Use this for initialization
    void Start ()
    {
        signInButtonText = GameObject.Find("signInButton").GetComponentInChildren<Text>();//Calls the text component ofthe sign in button which is a child of the button.
        authStatus = GameObject.Find("authStatus").GetComponent<Text>();//Calls the text component of the authstatus text.

        leaderboardButton = GameObject.Find("leaderboardButton");//Gets the leaderboard button.

        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()

        .EnableSavedGames()// enables saving game progress.

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
        PlayGamesPlatform.DebugLogEnabled = true;//Enables google play debugging.
        PlayGamesPlatform.Activate();//Activate google play services.
        PlayGamesPlatform.Instance.Authenticate(SignInCallback, true);//silent sign in if the player has signed in previously.

        leaderboardScore = (int)PlayerPrefs.GetFloat("HighScore", 0);//Store the local player prefs highscore in a private int.

        if (PlayGamesPlatform.Instance.localUser.authenticated)//if the player is signed in to google play services.
        {
            PlayGamesPlatform.Instance.ReportScore(leaderboardScore,GPGSIds.leaderboard_highscore,(bool success) =>//Push the local player prefs highscore to the leaderboard (if its higher than the score on google play leaderboard it will automatically replace that score).
                {
                    Debug.Log("Leaderboard update success: " + success);
                });
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated && playScript.enableLeadeboard == true)//If the user is signed in and the enable leaderboard bool from the playscript is true.
        {
            leaderboardButton.gameObject.SetActive(true);//set the leaderboard button to be visible.
        }
        else
        {
            leaderboardButton.gameObject.SetActive(false);//set the leaderboard button to be false.
        }
    }

    public void SignIn()//this is called when you click the sign in button.
    {

        if (!PlayGamesPlatform.Instance.localUser.authenticated)//if the user is nopt signed in.
        {
            // Sign in with Play Game Services, showing the consent dialog
            // by setting the second parameter to isSilent=false.
            Social.localUser.Authenticate((bool success) => {
                SignInCallback(success); //pushes returned bool to be handled by SignInCallback
            }); ;
        }
        else//f the user is signed in.
        {
            PlayGamesPlatform.Instance.SignOut();// Sign out of play games.
            signInButtonText.text = "Sign In";//set the text of the sign in button.
            authStatus.text = "";//set the text of the auth status.
        }
    }

    public void SignInCallback(bool success)
    {
        if (success)//if true.
        {
            Debug.Log("Signed in!");
            signInButtonText.text = "Sign out";// Change sign-in button text
            authStatus.text = "Signed in as: " + Social.localUser.userName;// Show the user's name
        }
        else//if false.
        {
            Debug.Log("Sign-in failed...");
            signInButtonText.text = "Sign in";// Show failure message
            authStatus.text = "Sign-in failed";
        }
    }

    public void ShowLeaderboard()//called when you click the laderboard button.
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)//if the user is signed in.
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI();//open the google play games leaderboard.
        }
        else//if the user isnt signed in.
        {
            Debug.Log("Cannot show leaderboard: not authenticated");
        }
    }
}
