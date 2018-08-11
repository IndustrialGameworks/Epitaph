using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

	public float movementSpeed = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Movement (); 
	}

	void Movement () {
		transform.Translate (Vector2.left * movementSpeed * Time.deltaTime);
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player") {
			Destroy (gameObject);
		}
	}
}
