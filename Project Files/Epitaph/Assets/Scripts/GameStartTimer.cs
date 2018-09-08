using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script controls the timer that runs at the start of the game scene.
public class GameStartTimer : MonoBehaviour
{
    //Floats.
	public float delayBeforeCountdown;

    //UI.
    public Text Countdown;

    //Audio.
	public AudioClip timer;
	AudioSource audioSource;

	// Use this for initialization
	void Start ()
    {
		Countdown.gameObject.SetActive (false);//Sets the countdown text object to be false.
		StartCoroutine ("Timer");
		audioSource = GetComponent<AudioSource> ();//Gets this audio component.
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

	IEnumerator Timer ()
    { //code for the countdown
		yield return new WaitForSeconds (delayBeforeCountdown);//wait for this amount of time before starting this coroutine.
		audioSource.PlayOneShot (timer, 0.7f);//Plays this audio clip once with a 0.7 volume scale.
		Countdown.gameObject.SetActive (true);//Sets the text to be visible.
		Countdown.text = "3";//Change the text objects text.
		yield return new WaitForSeconds (1);
		Countdown.text = "2";
		yield return new WaitForSeconds (1);
		Countdown.text = "1";
		yield return new WaitForSeconds (1);
		Countdown.text = "GO!";
		yield return new WaitForSeconds (1);
		Countdown.gameObject.SetActive (false);//Sets the countdown text object to be false.
        Destroy (gameObject); //destroys countdown, as no longer need until scene is reloaded
	}
}
