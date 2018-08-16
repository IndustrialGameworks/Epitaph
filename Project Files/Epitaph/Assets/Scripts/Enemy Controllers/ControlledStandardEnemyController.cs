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
	public GameObject enemyWaveController;

	//movement variables
	public Vector2 navLocation1;
	public Vector2 navLocation2;
	public Vector2 navLocation3;
	public Vector2 navLocation4;
	public Vector2 navLocation5;
	public int navPointsComplete = 0;
	public float waveSpeed = 10;

	// Use this for initialization
	void Start () {
		StandardEnemyWaveController waveScript = enemyWaveController.GetComponent<StandardEnemyWaveController> ();
		navLocation1 = waveScript.navigationPoints [0].transform.localPosition;
		navLocation2 = waveScript.navigationPoints [1].transform.localPosition;
		navLocation3 = waveScript.navigationPoints [2].transform.localPosition;
		navLocation4 = waveScript.navigationPoints [3].transform.localPosition;
		navLocation5 = waveScript.navigationPoints [4].transform.localPosition;
		StartCoroutine ("Attack"); //starts a coroutine running for firing projectiles
	}
	
	// Update is called once per frame
	void Update () {
		Movement ();
		Status ();
	}


	void Movement () 
	{
		if (navPointsComplete == 0) {
			if (gameObject.transform.localPosition.x > navLocation1.x)
			{
				gameObject.transform.Translate (-waveSpeed * Time.deltaTime, 0, 0);
			}
			else 
			{
				navPointsComplete = 1;
			}
		} 
		else if (navPointsComplete == 1) 
		{
			if (gameObject.transform.localPosition.y > navLocation2.y) 
			{
				gameObject.transform.Translate (0, -waveSpeed * Time.deltaTime, 0);
			} 
			else 
			{
				navPointsComplete = 2;
			}
		} else if (navPointsComplete == 2) 
		{
			if (gameObject.transform.localPosition.x < navLocation3.x) 
			{
				gameObject.transform.Translate (waveSpeed * Time.deltaTime, 0, 0);
			} 
			else 
			{
				navPointsComplete = 3;
			}
		} 
		else if (navPointsComplete == 3) 
		{
			if (gameObject.transform.localPosition.y < navLocation4.y) 
			{
				gameObject.transform.Translate (0, waveSpeed * Time.deltaTime, 0);
			} 
			else 
			{
				navPointsComplete = 4;
			}
		} 
		else if (navPointsComplete == 4) 
		{
			if (gameObject.transform.localPosition.x > navLocation5.x) 
			{
				gameObject.transform.Translate (-waveSpeed * Time.deltaTime, 0, 0);
			} 
			else 
			{
				navPointsComplete = 5;
			}
		}
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
		Destroy(transform.parent.gameObject);
		Destroy (gameObject);
	}
}
