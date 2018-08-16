using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlledStandardEnemyController : MonoBehaviour {

	public int health = 100;

	//Movement Variables
	public float movementSpeed = 3;

	//Attack Variables
	public float delayBetweenProjectiles = 1.5f;
	public GameObject frontEmitter;
	public GameObject projectile1;
	public GameObject projectile2;

	// Use this for initialization
	void Start () {
		StartCoroutine ("Attack"); //starts a coroutine running for firing projectiles
	}
	
	// Update is called once per frame
	void Update () {
		Movement ();
		Status ();
	}


	void Movement () {
		
	}

	void Status () {
		if (health <= 0) {
			Destroy (gameObject);
			GameController.gameScore += 10;
		}
	}

	IEnumerator Attack () {
		while (true) {
			Instantiate (projectile1, frontEmitter.transform.position, Quaternion.identity);
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

	void OnBecameInvisible ()
	{
		Destroy (gameObject);
	}
}
