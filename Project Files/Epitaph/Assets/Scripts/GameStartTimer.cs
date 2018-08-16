using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartTimer : MonoBehaviour {

	public float delayBeforeCountdown;
	public Text Countdown;

	public AudioClip timer;
	AudioSource audioSource;

	// Use this for initialization
	void Start () {
		Countdown.gameObject.SetActive (false);
		StartCoroutine ("Timer");
		audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator Timer () { //code for the countdown
		yield return new WaitForSeconds (delayBeforeCountdown);
		audioSource.PlayOneShot (timer, 0.7f);
		Countdown.gameObject.SetActive (true);
		Countdown.text = "3";
		yield return new WaitForSeconds (1);
		Countdown.text = "2";
		yield return new WaitForSeconds (1);
		Countdown.text = "1";
		yield return new WaitForSeconds (1);
		Countdown.text = "GO!";
		yield return new WaitForSeconds (1);
		Countdown.gameObject.SetActive (false);
		Destroy (gameObject); //destroys countdown, as no longer need until scene is reloaded
	}
}
