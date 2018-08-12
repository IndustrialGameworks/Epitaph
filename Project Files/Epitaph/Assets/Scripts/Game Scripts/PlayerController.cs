using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	//Movement Variables
	public float controllerX;
	public float controllerY;
	public float controllerSpeed;

	//Attack Variables
	public float delayBetweenProjectiles = 1.5f;
	public GameObject frontEmitter;
	public GameObject topEmitter;
	public GameObject bottomEmitter;
	public GameObject projectile1;
	public GameObject projectile2;

	//Pickup Variables
	public float pickupLength = 5;
	bool singleEmitter = true;
	bool doubleEmitter = false;
	bool tripleEmitter = false;

	void Start () {
		controllerX = transform.position.x;
		controllerY = transform.position.y;

		StartCoroutine ("Attack"); //starts a coroutine running for firing projectiles
	}

	void Update () {
		Movement ();
		ControllerPosition ();
	}

	void Movement () {
		Vector3 MovementVector;
		MovementVector = new Vector3 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"), 0.0f);
		if (transform.position.x >= -10.5f && transform.position.x <= 10.5f && transform.position.y >= -4.2f && transform.position.y <= 4.2f) {
			transform.Translate (MovementVector.normalized * controllerSpeed * Time.deltaTime);
		} else if (transform.position.x <= -10.5f) {
			transform.position = new Vector2 (-10.49f, transform.position.y);
		} else if (transform.position.x >= 10.5f) {
			transform.position = new Vector2 (10.49f, transform.position.y);
		} else if (transform.position.y <= -4.2f) {
			transform.position = new Vector2 (transform.position.x, -4.1f);
		} else if (transform.position.y >= 4.2f) {
			transform.position = new Vector2 (transform.position.x, 4.1f);
		}

		//legacy movement control
		/*if (Input.GetKey (KeyCode.UpArrow)) {
			transform.Translate (Vector2.up * (controllerSpeed) * Time.deltaTime);
		} 
		else if (Input.GetKey (KeyCode.DownArrow)) {
			transform.Translate (Vector2.down * (controllerSpeed) * Time.deltaTime);
		} 
		else if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.Translate (Vector2.left * (controllerSpeed) * Time.deltaTime);
		}
		else if (Input.GetKey (KeyCode.RightArrow)) {
			transform.Translate (Vector2.right * (controllerSpeed) * Time.deltaTime);
		} */
	}

	IEnumerator PickupTimer () { //controls the length of time pickups are active for
		while (pickupLength > 0) {
			yield return new WaitForSeconds (1);
			pickupLength--;
			Debug.Log (pickupLength);
		}
		singleEmitter = true;
		StartCoroutine ("Attack");
		StopCoroutine ("DualAttack");
		StopCoroutine ("TripleAttack");
	}

	IEnumerator Attack () { //standard attack from main emitter
		while (singleEmitter == true) {
			StopCoroutine ("PickupTimer");
			Instantiate (projectile1, frontEmitter.transform.position, Quaternion.identity);
			yield return new WaitForSeconds (delayBetweenProjectiles);
		}
	}

	IEnumerator DualAttack () {
		while (doubleEmitter == true) { //starts firing from top and bottom emitters
			Debug.Log ("Dual blast activated, should be firing.");
			Instantiate (projectile1, topEmitter.transform.position, Quaternion.identity);
			Instantiate (projectile1, bottomEmitter.transform.position, Quaternion.identity);
			yield return new WaitForSeconds (delayBetweenProjectiles);
		}
	}

	IEnumerator TripleAttack () { //start firing from all emitters
		while (tripleEmitter == true) {
			Debug.Log ("Dual blast activated, should be firing.");
			Instantiate (projectile1, frontEmitter.transform.position, Quaternion.identity);
			Instantiate (projectile1, topEmitter.transform.position, Quaternion.identity);
			Instantiate (projectile1, bottomEmitter.transform.position, Quaternion.identity);
			yield return new WaitForSeconds (delayBetweenProjectiles);
		}
	}

	void ControllerPosition () { //gets the x and y of the controllers position
		controllerX = transform.position.x;
		controllerY = transform.position.y;


	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "EnemyProjectile" || other.tag == "Enemy") {
			Destroy (gameObject);
		}
		if (other.tag == "PickupDualBlast") {
			Debug.Log ("Dual blast picked up");
			doubleEmitter = true;
			singleEmitter = false;
			tripleEmitter = false;

			pickupLength = 5;
			StartCoroutine ("DualAttack"); //starts a coroutine running for firing projectiles
			StartCoroutine ("PickupTimer");
			StopCoroutine ("Attack");
			StopCoroutine ("TripleAttack");
		}
		if (other.tag == "PickupTripleBlast") {
			Debug.Log ("Triple blast picked up");
			tripleEmitter = true;
			doubleEmitter = false;
			singleEmitter = false;

			pickupLength = 5;
			StartCoroutine ("TripleAttack"); //starts a coroutine running for firing projectiles
			StartCoroutine ("PickupTimer");
			StopCoroutine ("Attack");
			StopCoroutine ("DualAttack");
		}
	}
}
