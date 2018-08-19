using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	//Movement Variables
	public float controllerX;
	public float controllerY;
	public float controllerSpeed;

	//Attack Variables
	public float delayBeforeFirstAttack = 3.8f;
	public float delayBetweenProjectiles = 1.5f;
	public GameObject frontEmitter;
	public GameObject topEmitter;
	public GameObject bottomEmitter;
	public GameObject projectile1;
	public GameObject projectile2;
	public bool isDead = false;

	//Pickup Variables
	public float pickupLength = 5;
	bool singleEmitter = true;
	bool doubleEmitter = false;
	bool tripleEmitter = false;

	//screen bounds
	Vector2 bottomCorner;
	Vector2 topCorner;
	private float minX, maxX, minY, maxY;

	void Start () {
		//controllerX = transform.position.x;
		//controllerY = transform.position.y;
		bottomCorner = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
		topCorner = Camera.main.ViewportToWorldPoint(new Vector2(1,1));
		StartCoroutine ("Initialization"); //starts a coroutine running for firing projectiles
	}

	void Update () {
		setBounds ();
		ControllerPosition ();
		Movement ();
	}

	void setBounds ()
	{
		minX = bottomCorner.x;
		maxX = topCorner.x;
		minY = bottomCorner.y;
		maxY = topCorner.y;
		//Debug.Log ("constraints: " + minX + " minimumX, " + maxX + " maximumX, " + minY + " minimumy, " + maxY + " maximumy.");
	}

	void Movement () {
		Vector3 MovementVector;
		MovementVector = new Vector3 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"), 0.0f);
		if (transform.position.x >= minX && transform.position.x <= maxX && transform.position.y >= minY && transform.position.y <= maxY) 
		{
			transform.Translate (MovementVector.normalized * controllerSpeed * Time.deltaTime);
		} 
		else if (transform.position.x <= minX) 
		{
			controllerX = minX + 0.001f; //= new Vector2 (minX + 0.001f, transform.position.y);
			transform.position = new Vector2 (controllerX, controllerY);
		} 
		else if (transform.position.x >= maxX) 
		{
			controllerX = maxX - 0.001f; // = new Vector2 (maxX - 0.001f , transform.position.y);
			transform.position = new Vector2 (controllerX, controllerY);
		} 
		else if (transform.position.y <= minY) 
		{
			controllerY = minY + 0.001f; // = new Vector2 (transform.position.x, minY + 0.001f);
			transform.position = new Vector2 (controllerX, controllerY);
		} 
		else if (transform.position.y >= maxY) 
		{
			controllerY = maxY - 0.001f; //new Vector2 (transform.position.x, maxY - 0.001f);
			transform.position = new Vector2 (controllerX, controllerY);
		}









//		//legacy movement control
//		if (Input.GetKey (KeyCode.UpArrow)) {
//			transform.Translate (Vector2.up * (controllerSpeed) * Time.deltaTime);
//		} 
//		else if (Input.GetKey (KeyCode.DownArrow)) {
//			transform.Translate (Vector2.down * (controllerSpeed) * Time.deltaTime);
//		} 
//		else if (Input.GetKey (KeyCode.LeftArrow)) {
//			transform.Translate (Vector2.left * (controllerSpeed) * Time.deltaTime);
//		}
//		else if (Input.GetKey (KeyCode.RightArrow)) {
//			transform.Translate (Vector2.right * (controllerSpeed) * Time.deltaTime);
//		} 
	}

	IEnumerator Initialization()
	{
		yield return new WaitForSeconds (delayBeforeFirstAttack);
		StartCoroutine ("Attack");
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
			isDead = true;
			Destroy (gameObject);
			Destroy(transform.parent.gameObject);
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
