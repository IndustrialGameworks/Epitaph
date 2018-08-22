using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Use this script to set locations to move the "center of mass" and control where projectiles are repelled from. Just add nav points in the inspector!
//Must be attached to center of mass!


public class FiringPatternControl : MonoBehaviour {

	public bool setNewNav = false;
	public Vector2[] navPoints;
	public float speed = 2;
	Vector2 startLocation;
	public int randomInt = 0;
	int arraySize = 0;

	// Use this for initialization
	void Start () {
		startLocation = transform.localPosition;
		StartCoroutine ("Movement");
		arraySize = navPoints.Length;
	}
	
	// Update is called once per frame
	void Update () {
		GenerateRandoms ();
	}

	void GenerateRandoms () { //gets new random to select from the navPoints array
		if (setNewNav == true) {
			randomInt = (Random.Range (0, arraySize));
			setNewNav = false;
		}
	}

	IEnumerator Movement () {
		while (true) {
			transform.position = (Vector2.MoveTowards (new Vector2 (transform.position.x, transform.position.y), navPoints [randomInt], speed * Time.deltaTime)); //moves to navPoint
			yield return new WaitForSeconds (0.1f);
			transform.position = (Vector2.MoveTowards (new Vector2 (transform.position.x, transform.position.y), startLocation, speed * Time.deltaTime));	//moves back to original vector
			setNewNav = true;
			yield return new WaitForSeconds (0.1f);
		}
	}

}
