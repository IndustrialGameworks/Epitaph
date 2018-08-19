using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdTierEnemyController : MonoBehaviour {

	public int health = 300;

	//Movement Variables
	public float movementSpeed = 2;
	//public bool edgeBounce = false; //no longer in use
	bool moveDown; // currently assigns random boolean to whether enemy starts out moving up or down.

	//Attack Variables
	public bool cycleFire = false;
	public float delayBetweenProjectiles = 2f;
	public float timeBetweenCycling = 0.2f;
	public GameObject frontEmitter;
	public GameObject topEmitter;
	public GameObject bottomEmitter;
	public GameObject projectile1;
	public GameObject projectile2;
	public GameObject enemyWaveController;

	//movement variables
	public Vector2 navLocation1;
	public Vector2 navLocation2;
	public Vector2 navLocation3;
	public Vector2 navLocation4;
	public Vector2 navLocation5;
	public float distance = 1;
	public int navPointsComplete = 0;
	public float waveSpeed = 10;

	//editable variables
	public WaveEnd waveParent;

	//text variables
	public GameObject pointsText;
	TextMesh theText;
	public bool isDestroyed = false;

	//pickup variables
	public GameObject [] pickups;
	int randomChance;
	int pickupNumber;


	// Use this for initialization
	void Start () {
		//moveDown = (Random.value > 0.5f);
		//StartCoroutine ("Attack"); //starts a coroutine running for firing projectiles
		waveParent = transform.root.GetComponent<WaveEnd> ();
		randomChance = Random.Range (0, 21);
		pickupNumber = Random.Range (0, 2);

		delayBetweenProjectiles = waveParent.attackspeed;
		ThirdTierWave waveScript = enemyWaveController.GetComponent<ThirdTierWave> ();//brings in script from standardwavecontroller.
		navLocation1 = waveScript.navigationPoints [0].transform.position;//these call the positions of the 5 nav points.
		navLocation2 = waveScript.navigationPoints [1].transform.position;
		navLocation3 = waveScript.navigationPoints [2].transform.position;
		navLocation4 = waveScript.navigationPoints [3].transform.position;
		navLocation5 = waveScript.navigationPoints [4].transform.position;
		StartCoroutine ("Attack"); //starts a coroutine running for firing projectiles
		theText = pointsText.GetComponent<TextMesh>();//calls textmesh.
	}
	
	// Update is called once per frame
	void Update () {
		Movement ();
		Status ();
	}


	void Movement () {
		//transform.Translate (Vector2.left * movementSpeed * Time.deltaTime);

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
		float dist = Vector2.Distance(gameObject.transform.position, navLocation1);
		if (navPointsComplete == 0) 
		{//check if the enemies position is less than/ greater than the position of the nav point and a specific axis.
			//Debug.Log("navpointscomplete 0");
			if (dist > distance)
			{
				//Debug.Log ("dis1>distance");
				//transform.Translate (-waveSpeed * Time.deltaTime, 0, 0);//moves enemy towards that navpoint.
				transform.position = (Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), navLocation1, waveSpeed * Time.deltaTime));
			}
			else 
			{
				navPointsComplete = 1;//set navpoints complete to +1 so the enemy can move on to the next nav point.
			}
		} 
		else if (navPointsComplete == 1) 
		{
			dist = Vector2.Distance(gameObject.transform.position, navLocation2);
			if (dist > distance)
			{
				//Debug.Log ("dis1>distance");
				//transform.Translate (-waveSpeed * Time.deltaTime, 0, 0);//moves enemy towards that navpoint.
				transform.position = (Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), navLocation2, waveSpeed * Time.deltaTime));
			}
			else 
			{
				navPointsComplete = 2;//set navpoints complete to +1 so the enemy can move on to the next nav point.
			}
		} 
		else if (navPointsComplete == 2) 
		{
			dist = Vector2.Distance(gameObject.transform.position, navLocation3);
			if (dist > distance)
			{
				//Debug.Log ("dis1>distance");
				//transform.Translate (-waveSpeed * Time.deltaTime, 0, 0);//moves enemy towards that navpoint.
				transform.position = (Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), navLocation3, waveSpeed * Time.deltaTime));
			}
			else 
			{
				navPointsComplete = 3;//set navpoints complete to +1 so the enemy can move on to the next nav point.
			}
		} 
		else if (navPointsComplete == 3) 
		{
			dist = Vector2.Distance(gameObject.transform.position, navLocation4);
			if (dist > distance)
			{
				//Debug.Log ("dis1>distance");
				//transform.Translate (-waveSpeed * Time.deltaTime, 0, 0);//moves enemy towards that navpoint.
				transform.position = (Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), navLocation4, waveSpeed * Time.deltaTime));
			}
			else 
			{
				navPointsComplete = 4;//set navpoints complete to +1 so the enemy can move on to the next nav point.
			}
		} 
		else if (navPointsComplete == 4) 
		{
			dist = Vector2.Distance(gameObject.transform.position, navLocation5);
			if (dist > distance)
			{
				//Debug.Log ("dis1>distance");
				//transform.Translate (-waveSpeed * Time.deltaTime, 0, 0);//moves enemy towards that navpoint.
				transform.position = (Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), navLocation5, waveSpeed * Time.deltaTime));
			}
			else 
			{
				navPointsComplete = 5;//set navpoints complete to +1 so the enemy can move on to the next nav point.
			}
		} 
	}

	void Status () {
		if (health <= 0) {
			GameController.gameScore += (30 * GameController.multiplier);
			theText.text = "+" + (30 * GameController.multiplier);
			pointsText.transform.SetParent (enemyWaveController.transform);
			isDestroyed = true;
			GameController.multiplier += 1;
			GameController.timer = 180.0f; 
			if (randomChance == 10) 
			{
				Instantiate (pickups [pickupNumber], gameObject.transform.position, Quaternion.identity);
			}
			Destroy (gameObject);
		}
	}

	IEnumerator Attack () {
		while (true) {
			if (cycleFire == false) {
				Instantiate (projectile1, frontEmitter.transform.position, Quaternion.identity);
				Instantiate (projectile1, topEmitter.transform.position, Quaternion.identity);
				Instantiate (projectile1, bottomEmitter.transform.position, Quaternion.identity);
				yield return new WaitForSeconds (delayBetweenProjectiles);
			}
			if (cycleFire == true) {
				Instantiate (projectile1, frontEmitter.transform.position, Quaternion.identity);
				yield return new WaitForSeconds (timeBetweenCycling);
				Instantiate (projectile1, topEmitter.transform.position, Quaternion.identity);
				yield return new WaitForSeconds (timeBetweenCycling);
				Instantiate (projectile1, bottomEmitter.transform.position, Quaternion.identity);
				yield return new WaitForSeconds (delayBetweenProjectiles);
			}
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
		isDestroyed = true;
		Destroy (gameObject);
	}
}
