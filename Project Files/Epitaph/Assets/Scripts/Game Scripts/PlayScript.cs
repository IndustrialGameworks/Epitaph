using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[assembly: System.Reflection.AssemblyVersion("0.1.2.16")]

//This script is used to manipulate UI in the main menu and in the game scenes.

public class PlayScript : MonoBehaviour 
{
    //UI.
	public GameController game;
	public Text versionText;
    public Text optionsText;
    public Text loadingText1;
	public Button settingsButton;
	public Button backButton;
    public Button selectShip;
    public Image loadingImage;

    public GameObject homePanel;
    public GameObject optionsPanel;
    public GameObject loadingPanel;

    public Slider musicSlider;
    public Slider SFXSlider;

    //Booleans.
    public bool enableLeadeboard = true;

    //floats
    public float musicSliderVolume;
    public float SFXSliderVolume;

    //Color.
	Color Unchecked;
	Color Checked = Color.gray;
	ColorBlock cb1 = ColorBlock.defaultColorBlock;
	ColorBlock cb2 = ColorBlock.defaultColorBlock;

	void Start ()
	{
        PlayerPrefs.SetInt("playerType", 1);
        if (selectShip != null && backButton !=null)//if these game objects exist (checks so that it doesnt throw an error as this script is used in both scenes).
		{
            optionsPanel.gameObject.SetActive(false);
		}
		if (PlayerPrefs.GetInt ("playerType", 1) == 1) //checks if the player type player pref is equal to one (if it is non existant for example on first start up it will auto return 1).
		{
			PlayerPrefs.SetInt ("playerType", 1);//set this int to equal 1.
		}
        if (PlayerPrefs.GetFloat("musicVolume", 1) == 1)
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
        }
        if (PlayerPrefs.GetFloat("SFXVolume", 1) == 1)
        {
            PlayerPrefs.SetFloat("SFXVolume", 1);
        }
        if (musicSlider != null && SFXSlider != null)
        {
            musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 1);
            SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1);
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
        if (musicSlider != null && SFXSlider != null)
        {
            musicSliderVolume = musicSlider.value;
            SFXSliderVolume = SFXSlider.value;
            PlayerPrefs.SetFloat("musicVolume", musicSliderVolume);
            PlayerPrefs.SetFloat("SFXVolume", SFXSliderVolume);
        }
    }

	public void startGame()//This runs when the player clicks the start button.
	{
        if (loadingImage != null && loadingText1 != null)//if these gameobjects exist.
        {
            loadingPanel.gameObject.SetActive(true);
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

        homePanel.gameObject.SetActive(false);
        optionsPanel.gameObject.SetActive(true);
    }

	public void back ()//runs when the player clicks the bakc button in the options menu.
	{
        enableLeadeboard = true;//enables the leaderboard button if the player is also signed into goole play services.

        homePanel.gameObject.SetActive(true);
        optionsPanel.gameObject.SetActive(false);
    }

	public void SpawnShip()//when you click the circle button in the options menu.
	{
		PlayerPrefs.SetInt ("playerType", 1);//set this int in player prefs to equal 1.
		Debug.Log ((PlayerPrefs.GetInt ("playerType").ToString ()));
	}

	public void enable()
	{
		if (selectShip !=null && settingsButton.gameObject.activeSelf == false)//if these game objects exist and the settings button is disabled.
		{//modifies the colors of the different buttons and also enables and disables them depending on your selection.
			if (PlayerPrefs.GetInt ("playerType") == 1)
			{
				selectShip.colors = cb2;
                selectShip.GetComponent<Button>().interactable = false;
			}
		}
	}
}
