using System.Collections;
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

	//Attack Variables
	public float delayBetweenProjectiles = 1.5f;
	public GameObject projectile;

	// Use this for initialization
	void Start () {
		centerOfMass = origin.transform.position; //assigns center of object so that its projectiles can access it for their translation
		StartCoroutine ("Attack"); //starts a coroutine running for firing projectiles
	}
	
	// Update is called once per frame
	void Update () {
		Movement ();
		Status ();

		Debug.Log (spawnedProjectile.transform.position);
	}


	void Movement () {
		movementController.transform.Translate (Vector2.left * movementSpeed * Time.deltaTime); 
		transform.Rotate (Vector3.forward * rotationSpeed);
		centerOfMass = origin.transform.position;
	}

	void Status () {
		if (health <= 0) {
			Destroy (gameObject);
			GameController.gameScore += 10;
		}
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

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "PlayerProjectile") {
			health -= 50;
			Destroy (other.gameObject);
		}
	}
}
