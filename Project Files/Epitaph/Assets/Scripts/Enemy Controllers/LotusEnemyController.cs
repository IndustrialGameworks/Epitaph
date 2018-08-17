﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotusEnemyController : MonoBehaviour {

	public int health = 100;
	public GameObject [] emitters;
	public GameObject origin;
	public GameObject movementController; //parent that doesn't change rotation, so can still use world vectors
	public float rotationSpeed = 5;

	GameObject spawnedProjectile;
	public Vector2 centerOfMass;

	//Movement Variables
	public float movementSpeed = 3;

	bool constructLocation = true;

	int randomX;
	int randomY;
	Vector2 randomVector;

	//Attack Variables
	public float delayBetweenProjectiles = 1.5f;
	public GameObject projectile;

	// Use this for initialization
	void Start () {
		RandomGeneration ();
		centerOfMass = origin.transform.position; //assigns center of object so that its projectiles can access it for their translation
		StartCoroutine ("Attack"); //starts a coroutine running for firing projectiles
		StartCoroutine ("BuildRandomLocation");
	}
	
	// Update is called once per frame
	void Update () {
		Movement ();
		Status ();

		if (constructLocation == true) {
			RandomGeneration ();
			constructLocation = false;
		}
	}


	void Movement () {
		movementController.transform.position = Vector2.MoveTowards(new Vector2(movementController.transform.position.x, movementController.transform.position.y), randomVector, 3f * Time.deltaTime);
		//movementController.transform.Translate (randomVector * movementSpeed * Time.deltaTime); 
		transform.Rotate (Vector3.forward * rotationSpeed);
		centerOfMass = origin.transform.position;
	}

	void Status () {
		if (health <= 0) {
			Destroy (gameObject);
			GameController.gameScore += (10 * GameController.multiplier);
			GameController.multiplier += 1;
			GameController.timer = 180.0f;
		}
	}

	void RandomGeneration () { //generates random x and y coords within bounds
		randomX = Random.Range (-5, 10); //x constraints
		randomY = Random.Range (-4, 4); //y constraints
		randomVector = new Vector2 (randomX, randomY); //constructs new random vector2
	}

	IEnumerator Attack () {
		while (true) {
			spawnedProjectile = Instantiate (projectile, emitters [0].transform.position, Quaternion.identity) as GameObject; //spawns projectile, returns gameObject
			spawnedProjectile.gameObject.GetComponent<lotusProjectile>().spawnOrigin = gameObject; //assigns this enemy instance to the projectile as point of origin

			spawnedProjectile = Instantiate (projectile, emitters [1].transform.position, Quaternion.identity) as GameObject;
			spawnedProjectile.gameObject.GetComponent<lotusProjectile>().spawnOrigin = gameObject;

			spawnedProjectile = Instantiate (projectile, emitters [2].transform.position, Quaternion.identity) as GameObject;
			spawnedProjectile.gameObject.GetComponent<lotusProjectile>().spawnOrigin = gameObject;

			spawnedProjectile = Instantiate (projectile, emitters [3].transform.position, Quaternion.identity) as GameObject;
			spawnedProjectile.gameObject.GetComponent<lotusProjectile>().spawnOrigin = gameObject;

			spawnedProjectile = Instantiate (projectile, emitters [4].transform.position, Quaternion.identity) as GameObject;
			spawnedProjectile.gameObject.GetComponent<lotusProjectile>().spawnOrigin = gameObject;

			yield return new WaitForSeconds (delayBetweenProjectiles);
		}
	}

	IEnumerator BuildRandomLocation () {
		while (true) {
			yield return new WaitForSeconds (10);
			constructLocation = true;
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "PlayerProjectile") {
			health -= 50;
			Destroy (other.gameObject);
		}
	}
}
