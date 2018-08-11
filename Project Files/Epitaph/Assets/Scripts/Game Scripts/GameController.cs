using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public static int gameScore = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		DisplayScore ();
	}

	void DisplayScore () {
		Debug.Log ("Score " + gameScore);
	}
}
