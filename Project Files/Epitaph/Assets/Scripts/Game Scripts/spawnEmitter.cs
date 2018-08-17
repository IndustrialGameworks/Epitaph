using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEmitter : MonoBehaviour {

	//Array that holds enemy gameobject prefab references
	int enemyArraySize;
	int specialEnemyArraySize;
	public GameObject [] enemy;
	public GameObject [] specialEnemy;
	int currentRandomEnemy;
	int currentRandomSpecialEnemy;

	//Array that holds pickup gameobject prefab references
	int pickupArraySize;
	public GameObject [] pickup;
	int currentRandomPickup;

	//Holds the location of all emitters in an array for easy referencing
	int emitterArraySize;
	int topEmitterArraySize;
	public GameObject [] emitters;
	public GameObject [] topEmitters;
	public GameObject [] bottomEmitters;
	GameObject currentEmitter;
	GameObject secondCurrentEmitter;
	GameObject specialCurrentEmitter;
	int randomEmitterNumber;
	int topBottomRandomEmitterNumber;

	int randomEmitterSpawn;

	public float secondsBetweenEnemies;
	public float secondsBetweenSpecialEnemies;
	public float secondsBetweenPickups = 1f;
	public float secondsBeforeBegin = 4f;

	// Use this for initialization
	void Start () {
		GetArrayDetails ();
		currentEmitter = emitters [1]; //needs to be assigned on start
		secondCurrentEmitter = emitters [2]; //needs to be assigned on start
		StartCoroutine ("Initialization");
	}
	
	// Update is called once per frame
	void Update () {
		RandomGenerator ();
	}

	//gets the details of array length at startup and then saves them to variables for use by the random generators
	void GetArrayDetails () {
		emitterArraySize = emitters.Length;
		topEmitterArraySize = topEmitters.Length;
		pickupArraySize = pickup.Length;
		enemyArraySize = enemy.Length;
		specialEnemyArraySize = specialEnemy.Length;
//		Debug.Log (emitterArraySize);
//		Debug.Log (enemyArraySize);
//		Debug.Log (pickupArraySize);
	}

	//generates random numbers to call items from the arrays
	void RandomGenerator () {
		randomEmitterNumber = Random.Range (0, emitterArraySize);
		topBottomRandomEmitterNumber = Random.Range (0, topEmitterArraySize);
		currentRandomPickup =  Random.Range (0, pickupArraySize);
		currentRandomEnemy = Random.Range (0, enemyArraySize);
		currentRandomSpecialEnemy = Random.Range (0, specialEnemyArraySize);

		randomEmitterSpawn = Random.Range (0, 1);

		currentEmitter = emitters [randomEmitterNumber];
		//secondCurrentEmitter = emitters [secondRandomEmitterNumber];


		//these control whether to use th
		if (randomEmitterSpawn == 0) {
			specialCurrentEmitter = topEmitters [topBottomRandomEmitterNumber];
		}
		if (randomEmitterSpawn == 1) {
			specialCurrentEmitter = bottomEmitters [topBottomRandomEmitterNumber];
		}
	}

	IEnumerator Initialization () {
		yield return new WaitForSeconds (secondsBeforeBegin);
		StartCoroutine ("SpawnPickup");
		StartCoroutine ("SpawnEnemy");
		StartCoroutine ("SpawnSpecialEnemy");
	}

	//Coroutine that handles spawning enemies
	IEnumerator SpawnEnemy () {
		while (true) {
			GameObject multispawn = currentEmitter; //hold the spawn emitter number until enmy group is out
			Instantiate (enemy [0], multispawn.transform.position , Quaternion.identity);
			yield return new WaitForSeconds(secondsBetweenEnemies); //for testing multiple enemies
//			Instantiate (enemy [currentRandomEnemy], multispawn.transform.position , Quaternion.identity); //for testing multiple enemies
//			yield return new WaitForSeconds(0.25f); //for testing multiple enemies
//			Instantiate (enemy [currentRandomEnemy], multispawn.transform.position , Quaternion.identity); //for testing multiple enemies
//			yield return new WaitForSeconds(0.25f); //for testing multiple enemies
//			Instantiate (enemy [currentRandomEnemy], multispawn.transform.position , Quaternion.identity); //for testing multiple enemies
//			yield return new WaitForSeconds(0.25f); //for testing multiple enemies
//			Instantiate (enemy [currentRandomEnemy], multispawn.transform.position , Quaternion.identity); //for testing multiple enemies
//			yield return new WaitForSeconds(secondsBetweenEnemies);
		}
	}

	//Coroutine that handles spawning enemies
	IEnumerator SpawnSpecialEnemy () {
		while (true) {
			yield return new WaitForSeconds (secondsBetweenSpecialEnemies);
			Instantiate (specialEnemy [currentRandomSpecialEnemy], specialCurrentEmitter.transform.position , Quaternion.identity);
			yield return new WaitForSeconds(secondsBetweenSpecialEnemies); //for testing multiple enemies
		}
	}
	 
	//Coroutine that handles pickup spawning
	IEnumerator SpawnPickup () {
		while (true) {
			Instantiate (pickup [currentRandomPickup], secondCurrentEmitter.transform.position , Quaternion.identity);
			yield return new WaitForSeconds(secondsBetweenPickups);
		}
	}

}
