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

	public Button playButton;
	public Button quitButton;
	public Button settingsButton;
	public Button backButton;
	public Image industrialLogo;


	public Text optionsText;
	public Image optionsBack;
	public Button selectCircle;
	public Button selectTriangle;
	public Button selectSquare;


	Color Unchecked;
	Color Checked = Color.gray;
	ColorBlock cb1 = ColorBlock.defaultColorBlock;
	ColorBlock cb2 = ColorBlock.defaultColorBlock;

	void Start ()
	{
		if (selectCircle != null && selectSquare != null && selectTriangle != null && backButton !=null)
		{
			optionsText.gameObject.SetActive (false);
			optionsBack.gameObject.SetActive (false);
			selectCircle.gameObject.SetActive (false);
			selectTriangle.gameObject.SetActive (false);
			selectSquare.gameObject.SetActive (false);
			backButton.gameObject.SetActive (false);
		}

		//PlayerPrefs.DeleteKey ("playerType");
		if (PlayerPrefs.GetInt ("playerType", 1) == 1) 
		{
			PlayerPrefs.SetInt ("playerType", 1);
		}
		cb2.normalColor = Checked;
		Debug.Log (PlayerPrefs.GetInt ("playerType", 1).ToString ());
		Debug.Log(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
		if (versionText != null) 
		{
			versionText.text = "Version: " + System.Reflection.Assembly.GetExecutingAssembly ().GetName ().Version.ToString ();
		}
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

	public void openSettings ()
	{
		settingsButton.gameObject.SetActive (false);
		playButton.gameObject.SetActive (false);
		quitButton.gameObject.SetActive (false);
		industrialLogo.gameObject.SetActive (false);
		backButton.gameObject.SetActive (true);
		optionsText.gameObject.SetActive (true);
		optionsBack.gameObject.SetActive (true);
	}

	public void back ()
	{
		settingsButton.gameObject.SetActive (true);
		playButton.gameObject.SetActive (true);
		quitButton.gameObject.SetActive (true);
		industrialLogo.gameObject.SetActive (true);
		backButton.gameObject.SetActive (false);
		selectCircle.gameObject.SetActive (false);
		selectTriangle.gameObject.SetActive (false);
		selectSquare.gameObject.SetActive (false);
		optionsText.gameObject.SetActive (false);
		optionsBack.gameObject.SetActive (false);
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
		if (selectCircle != null && selectSquare != null && selectTriangle != null && settingsButton.gameObject.activeSelf == false)
		{
			if (PlayerPrefs.GetInt ("playerType") == 1)
			{
//				selectCircle.gameObject.SetActive (false);
//				selectTriangle.gameObject.SetActive (true);
//				selectSquare.gameObject.SetActive (true);
				selectCircle.colors = cb2;
				selectCircle.gameObject.SetActive (false);
				selectTriangle.colors = cb1;
				selectSquare.colors = cb1;
				selectCircle.gameObject.SetActive (true);
				selectTriangle.gameObject.SetActive (true);
				selectSquare.gameObject.SetActive (true);
			}
			else if (PlayerPrefs.GetInt ("playerType") == 2) 
			{
				selectCircle.colors = cb1;
				selectTriangle.colors = cb2;
				selectTriangle.gameObject.SetActive (false);
				selectSquare.colors = cb1;
//				selectCircle.gameObject.SetActive (true);
//				selectTriangle.gameObject.SetActive (false);
//				selectSquare.gameObject.SetActive (true);
				selectCircle.gameObject.SetActive (true);
				selectTriangle.gameObject.SetActive (true);
				selectSquare.gameObject.SetActive (true);
			} 
			else if (PlayerPrefs.GetInt ("playerType") == 3) 
			{
				selectCircle.colors = cb1;
				selectTriangle.colors = cb1;
				selectSquare.colors = cb2;
				selectSquare.gameObject.SetActive (false);
//				selectCircle.gameObject.SetActive (true);
//				selectTriangle.gameObject.SetActive (true);
//				selectSquare.gameObject.SetActive (false);
				selectCircle.gameObject.SetActive (true);
				selectTriangle.gameObject.SetActive (true);
				selectSquare.gameObject.SetActive (true);
			}
		}
	}
}
