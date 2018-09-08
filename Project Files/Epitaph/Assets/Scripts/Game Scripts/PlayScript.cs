using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[assembly: System.Reflection.AssemblyVersion("0.1.2.10")]

//This script is used to manipulate UI in the main menu and in the game scenes.

public class PlayScript : MonoBehaviour 
{
    //UI.
	public GameController game;
	public Text versionText;
    public Text authText;
    public Text optionsText;
    public Text loadingText1;
    public Button playButton;
	public Button quitButton;
	public Button settingsButton;
	public Button backButton;
    public Button signIntoPlay;
    public Button showLeaderboard;
    public Button selectCircle;
    public Button selectTriangle;
    public Button selectSquare;
    public Button selectFox;
    public Image industrialLogo;
    public Image optionsBack;
    public Image loadingImage;

    //Booleans.
    public bool enableLeadeboard = true;

    //Color.
	Color Unchecked;
	Color Checked = Color.gray;
	ColorBlock cb1 = ColorBlock.defaultColorBlock;
	ColorBlock cb2 = ColorBlock.defaultColorBlock;

	void Start ()
	{
		if (selectCircle != null && selectSquare != null && selectTriangle != null && selectFox != null && backButton !=null)//if these game objects exist (checks so that it doesnt throw an error as this script is used in both scenes).
		{
			optionsText.gameObject.SetActive (false);//set these game objects to be false.
			optionsBack.gameObject.SetActive (false);
			selectCircle.gameObject.SetActive (false);
			selectTriangle.gameObject.SetActive (false);
			selectSquare.gameObject.SetActive (false);
			selectFox.gameObject.SetActive (false);
			backButton.gameObject.SetActive (false);
		}
		if (PlayerPrefs.GetInt ("playerType", 1) == 1) //checks if the player type player pref is equal to one (if it is non existant for example on first start up it will auto return 1).
		{
			PlayerPrefs.SetInt ("playerType", 1);//set this int to equal 1.
		}
		cb2.normalColor = Checked;//changes the normal color of the cb2 colorblock to be grey.
		Debug.Log(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());//shows the version number which is set at the top of this screen.
		if (versionText != null) //if this game object exists.
		{
			versionText.text = "Version: " + System.Reflection.Assembly.GetExecutingAssembly ().GetName ().Version.ToString ();//set the text of version text.
		}
	}

	void Update ()
	{
		enable ();//run this void.
	}

	public void startGame()//This runs when the player clicks the start button.
	{
        if (loadingImage != null && loadingText1 != null)//if these gameobjects exist.
        {
            loadingImage.gameObject.SetActive(true);//set these gameobjects to be active.
            loadingText1.gameObject.SetActive(true);
        }
        SceneManager.LoadScene ("Test");//load the game scene.
	}

	public void restart()//this runs when the player clicks the restart button.
	{
		SceneManager.LoadScene ("Test");//load the game scene.
	}

	public void exitGame()//runs when the player clicks the exit button.
	{
		Application.Quit ();//quit the game.
	}

    public void exitToMainMenu()//when the player clicks the main menu button in the game scene.
    {
        SceneManager.LoadScene("MainMenu");//load the main menu scene.
    }

	public void openSettings ()//runs when the player clicks the settings button.
	{
        enableLeadeboard = false;//disables the leaderboard button when the options menu is open.

        settingsButton.gameObject.SetActive (false);//disable these game objects.
        signIntoPlay.gameObject.SetActive(false);
        showLeaderboard.gameObject.SetActive(false);
        authText.gameObject.SetActive(false);
		playButton.gameObject.SetActive (false);
		quitButton.gameObject.SetActive (false);
		industrialLogo.gameObject.SetActive (false);
		backButton.gameObject.SetActive (true);//enable these game objects.
		optionsText.gameObject.SetActive (true);
		optionsBack.gameObject.SetActive (true);
	}

	public void back ()//runs when the player clicks the bakc button in the options menu.
	{
        enableLeadeboard = true;//enables the leaderboard button if the player is also signed into goole play services.

        settingsButton.gameObject.SetActive (true);//enable these game objects.
        signIntoPlay.gameObject.SetActive(true);
        showLeaderboard.gameObject.SetActive(true);
        authText.gameObject.SetActive(true);
        playButton.gameObject.SetActive (true);
		quitButton.gameObject.SetActive (true);
		industrialLogo.gameObject.SetActive (true);
		backButton.gameObject.SetActive (false);//disable these game objects.
		selectCircle.gameObject.SetActive (false);
		selectTriangle.gameObject.SetActive (false);
		selectSquare.gameObject.SetActive (false);
		selectFox.gameObject.SetActive (false);
		optionsText.gameObject.SetActive (false);
		optionsBack.gameObject.SetActive (false);
	}

	public void SpawnCircle()//when you click the circle button in the options menu.
	{
		PlayerPrefs.SetInt ("playerType", 1);//set this int in player prefs to equal 1.
		Debug.Log ((PlayerPrefs.GetInt ("playerType").ToString ()));
	}

	public void SpawnTriangle ()//when you click the triangle button in the options menu.
    {
		PlayerPrefs.SetInt ("playerType", 2);//set this int in player prefs to equal 2.
        Debug.Log ((PlayerPrefs.GetInt ("playerType").ToString ()));
	}

	public void SpawnSquare ()//when you click the square button in the options menu.
    {
		PlayerPrefs.SetInt ("playerType", 3);//set this int in player prefs to equal 3.
        Debug.Log ((PlayerPrefs.GetInt ("playerType").ToString ()));
	}

	public void SpawnFox ()//when you click the fox button in the options menu.
    {
		PlayerPrefs.SetInt ("playerType", 4);//set this int in player prefs to equal 4.
        Debug.Log ((PlayerPrefs.GetInt ("playerType").ToString ()));
	}

	public void enable()
	{
		if (selectCircle != null && selectSquare != null && selectTriangle != null && selectFox !=null && settingsButton.gameObject.activeSelf == false)//if these game objects exist and the settings button is disabled.
		{//modifies the colors of the different buttons and also enables and disables them depending on your selection.
			if (PlayerPrefs.GetInt ("playerType") == 1)
			{
				selectCircle.colors = cb2;
				selectCircle.gameObject.SetActive (false);
				selectTriangle.colors = cb1;
				selectSquare.colors = cb1;
				selectFox.colors = cb1;
				selectCircle.gameObject.SetActive (true);
				selectTriangle.gameObject.SetActive (true);
				selectSquare.gameObject.SetActive (true);
				selectFox.gameObject.SetActive (true);
			}
			else if (PlayerPrefs.GetInt ("playerType") == 2) 
			{
				selectCircle.colors = cb1;
				selectTriangle.colors = cb2;
				selectTriangle.gameObject.SetActive (false);
				selectSquare.colors = cb1;
				selectFox.colors = cb1;
				selectCircle.gameObject.SetActive (true);
				selectTriangle.gameObject.SetActive (true);
				selectSquare.gameObject.SetActive (true);
				selectFox.gameObject.SetActive (true);
			} 
			else if (PlayerPrefs.GetInt ("playerType") == 3) 
			{
				selectCircle.colors = cb1;
				selectTriangle.colors = cb1;
				selectSquare.colors = cb2;
				selectFox.colors = cb1;
				selectSquare.gameObject.SetActive (false);

				selectCircle.gameObject.SetActive (true);
				selectTriangle.gameObject.SetActive (true);
				selectSquare.gameObject.SetActive (true);
				selectFox.gameObject.SetActive (true);
			}

			else if (PlayerPrefs.GetInt ("playerType") == 4) 
			{
				selectCircle.colors = cb1;
				selectTriangle.colors = cb1;
				selectSquare.colors = cb1;
				selectFox.colors = cb2;
				selectFox.gameObject.SetActive (false);
				selectCircle.gameObject.SetActive (true);
				selectTriangle.gameObject.SetActive (true);
				selectSquare.gameObject.SetActive (true);
				selectFox.gameObject.SetActive (true);
			}
		}
	}
}
