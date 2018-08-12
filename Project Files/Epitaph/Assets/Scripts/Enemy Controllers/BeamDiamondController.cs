﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamDiamondController : MonoBehaviour {

	public int health = 10;

	public float movementSpeed = 5;
	public float indicatorFireDelay = 1.25f;
	public float beamFireDuration = 2;
	public GameObject indicatorBeam;
	public GameObject fullBeam;
	public GameObject playerToTrack;
	float playerAxisY;
	float thisAxisY;
	float axisDifference;
	bool isFiring;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Movement ();
		Status ();
	}

	void Movement () {
		if (playerToTrack != null) {
			playerAxisY = playerToTrack.transform.position.y;
			thisAxisY = gameObject.transform.position.y;
			axisDifference = playerAxisY - thisAxisY;
			if (axisDifference > 0.25 && isFiring == false) {
				transform.Translate (Vector2.up * movementSpeed * Time.deltaTime);
			} else if (axisDifference < -0.25 && isFiring == false) {
				transform.Translate (Vector2.down * movementSpeed * Time.deltaTime);
			} else {
				StartCoroutine ("Attack");
			}
		}
	}

	void Status () {
		if (health <= 0) {
			Destroy (gameObject);
			GameController.gameScore += 100;
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "PlayerProjectile") 
		{
			health -= 50;
			Destroy (other.gameObject);
		}
	}


	IEnumerator Attack () {
		isFiring = true;
		indicatorBeam.gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		yield return new WaitForSeconds (indicatorFireDelay);
		indicatorBeam.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		fullBeam.gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		fullBeam.gameObject.GetComponent<BoxCollider2D> ().enabled = true;
		yield return new WaitForSeconds (beamFireDuration);
		fullBeam.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		fullBeam.gameObject.GetComponent<BoxCollider2D> ().enabled = false;
		isFiring = false;
		StopCoroutine ("Attack");
	}
}
