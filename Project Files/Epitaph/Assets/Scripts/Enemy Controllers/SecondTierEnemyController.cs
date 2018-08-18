using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondTierEnemyController : MonoBehaviour {

	public int health = 200;

	//Movement Variables
	public float movementSpeed = 3;
	//public bool edgeBounce = false; //no longer in use
	bool moveDown; // currently assigns random boolean to whether enemy starts out moving up or down.

	//Attack Variables
	public float delayBetweenProjectiles = 1.5f;
	public GameObject frontEmitter;
	public GameObject topEmitter;
	public GameObject bottomEmitter;
	public GameObject projectile1;
	public GameObject projectile2;

	// Use this for initialization
	void Start () {
		moveDown = (Random.value > 0.5f);
		StartCoroutine ("Attack"); //starts a coroutine running for firing projectiles
	}
	
	// Update is called once per frame
	void Update () {
		Movement ();
		Status ();
	}


	void Movement () {
		transform.Translate (Vector2.left * movementSpeed * Time.deltaTime);

//		if (edgeBounce == true) { //legacy code no longer in use
//
//			if (transform.position.y <= -4.2f) {
//				moveDown = false;
//			}
//			if (transform.position.y >= 4.2f) {
//				moveDown = true;
//			}
//
//			if (moveDown == true) {
//				transform.Translate (Vector2.down * movementSpeed * Time.deltaTime);
//			}
//			if (moveDown == false) {
//				transform.Translate (Vector2.up * movementSpeed * Time.deltaTime);
//			}
//		}
	}

	void Status () {
		if (health <= 0) {
			Destroy (gameObject);
			GameController.gameScore += 10;
		}
	}

	IEnumerator Attack () {
		while (true) {
			Instantiate (projectile1, topEmitter.transform.position, Quaternion.identity);
			Instantiate (projectile1, bottomEmitter.transform.position, Quaternion.identity);
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
