using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimerTurret : MonoBehaviour {

	public int health = 1000;

	public GameObject emitter;
	public GameObject origin;
	public GameObject player;
	public float rotationSpeed = 5;
	public float volleySeperation = 0.3f;
	public float delayBetweenProjectiles = 4;
	public float delayBeforeStart = 0.5f;

	GameObject spawnedProjectile;
	public Vector2 centerOfMass;

	public GameObject projectile;

	//text variables
	public GameObject pointsText;
	TextMesh theText;
	public bool isDestroyed;
	public GameObject turretParent;

	//color variables
	Color hit = new Color (145f/255f, 50f/255f, 50f/255f, 1);
	Color standard = Color.white;
	SpriteRenderer turretSprite;

	// Use this for initialization
	void Start () {
		turretSprite = GetComponent<SpriteRenderer> ();
		theText = pointsText.GetComponent<TextMesh>();
		StartCoroutine ("Attack");
	}
	
	// Update is called once per frame
	void Update () {
		player = GameObject.FindGameObjectWithTag ("Player"); //has to be be called on update during testing, as there is no player at the time this instance starts
		RotateToTarget();
		centerOfMass = origin.transform.position;
		Status ();


	}

	void RotateToTarget () { //code to rotate to look at target
		if (player != null) 
		{
			Vector3 vectorToTarget = player.transform.position - transform.position;
			float angle = Mathf.Atan2 (-vectorToTarget.y, -vectorToTarget.x) * Mathf.Rad2Deg; //take out the minuses if you want to invert the side that tracks the player
			Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
			transform.rotation = Quaternion.Slerp (transform.rotation, q, Time.deltaTime * rotationSpeed);
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "PlayerProjectile") {
			health -= 50;
			Destroy (other.gameObject);
			StartCoroutine ("changeColor");
		}
	}

	IEnumerator Attack () {
		while (true) {
			yield return new WaitForSeconds (delayBeforeStart);
			spawnedProjectile = Instantiate (projectile, emitter.transform.position, Quaternion.identity) as GameObject; //spawns projectile, returns gameObject
			spawnedProjectile.gameObject.GetComponent<primerProjectile>().spawnOrigin = gameObject; //assigns this enemy instance to the projectile as point of origin

			yield return new WaitForSeconds (volleySeperation);

			spawnedProjectile = Instantiate (projectile, emitter.transform.position, Quaternion.identity) as GameObject; //spawns projectile, returns gameObject
			spawnedProjectile.gameObject.GetComponent<primerProjectile>().spawnOrigin = gameObject; //assigns this enemy instance to the projectile as point of origin

			yield return new WaitForSeconds (volleySeperation);

			spawnedProjectile = Instantiate (projectile, emitter.transform.position, Quaternion.identity) as GameObject; //spawns projectile, returns gameObject
			spawnedProjectile.gameObject.GetComponent<primerProjectile>().spawnOrigin = gameObject; //assigns this enemy instance to the projectile as point of origin

			yield return new WaitForSeconds (volleySeperation);

			spawnedProjectile = Instantiate (projectile, emitter.transform.position, Quaternion.identity) as GameObject; //spawns projectile, returns gameObject
			spawnedProjectile.gameObject.GetComponent<primerProjectile>().spawnOrigin = gameObject; //assigns this enemy instance to the projectile as point of origin

			yield return new WaitForSeconds (volleySeperation);

			spawnedProjectile = Instantiate (projectile, emitter.transform.position, Quaternion.identity) as GameObject; //spawns projectile, returns gameObject
			spawnedProjectile.gameObject.GetComponent<primerProjectile>().spawnOrigin = gameObject; //assigns this enemy instance to the projectile as point of origin

			yield return new WaitForSeconds (volleySeperation);

			spawnedProjectile = Instantiate (projectile, emitter.transform.position, Quaternion.identity) as GameObject; //spawns projectile, returns gameObject
			spawnedProjectile.gameObject.GetComponent<primerProjectile>().spawnOrigin = gameObject; //assigns this enemy instance to the projectile as point of origin

			yield return new WaitForSeconds (delayBetweenProjectiles);
		}
	}

	void Status () {
		if (health <= 0) {

			GameController.gameScore += (250 * GameController.multiplier);
			theText.text = "+" + (250 * GameController.multiplier);
			//pointsText.transform.SetParent (turretParent.transform);
			isDestroyed = true;
			GameController.multiplier += 1;
			GameController.timer = 180.0f;
			Destroy (gameObject);
		}
	}

	IEnumerator changeColor ()
	{
		turretSprite.color = hit;
		yield return new WaitForSeconds (0.125f);
		turretSprite.color = standard;
	}
}
