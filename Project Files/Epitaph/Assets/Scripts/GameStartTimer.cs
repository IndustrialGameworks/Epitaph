using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartTimer : MonoBehaviour {

	public float delayBeforeCountdown;
	public GameObject gfx3;
	public GameObject gfx2;
	public GameObject gfx1;
	public GameObject gfxGO;

	public AudioClip timer;
	AudioSource audioSource;

	// Use this for initialization
	void Start () {
		StartCoroutine ("Timer");
		audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator Timer () { //code for the countdown
		yield return new WaitForSeconds (delayBeforeCountdown);
		audioSource.PlayOneShot (timer, 0.7f);
		gfx3.gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		yield return new WaitForSeconds (1);
		gfx3.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		gfx2.gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		yield return new WaitForSeconds (1);
		gfx2.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		gfx1.gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		yield return new WaitForSeconds (1);
		gfx1.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		gfxGO.gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		yield return new WaitForSeconds (1);
		Destroy (gameObject); //destroys countdown, as no longer need until scene is reloaded
	}
}
