using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script controls whatt the pickup does once it collides with a player collider.

public class Pickup : MonoBehaviour
{
    //Floats.
	public float movementSpeed = 10;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		Movement (); 
	}

	void Movement ()
    {//moves the pickup across the screen.
		transform.Translate (Vector2.left * movementSpeed * Time.deltaTime);
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player" || other.tag == "PlayerTouchController")//checks if the pickup has collided with an object with the tag player or playerTouchController.
        {
			Destroy (gameObject);//destroys this gameObject.
		}
	}
}
