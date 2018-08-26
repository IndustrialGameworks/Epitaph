using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEmitter : MonoBehaviour 
{

	//calls the game controller script
	public GameController controller;

	//bools
	public bool canSpawnBoss = true;
	public bool bossSpawned = false;

	//floats
	public float bossCounter = 1;
	public float secondsBetweenEnemies;
	public float secondsBetweenSpecialEnemies;
	public float secondsBetweenPickups = 1f;
	public float secondsBeforeBegin = 4f;

	//integers
	int randomSelector = 0;
	int specialEnemyArraySize;
	int currentRandomEnemy;
	int currentRandomSpecialEnemy;
	int pickupArraySize;
	int currentRandomPickup;
	int emitterArraySize = 0;
	int topEmitterArraySize;
	int randomEmitterNumber;
	int topBottomRandomEmitterNumber;
	int randomEmitterSpawn;

	//Array that holds enemy gameobject prefab references
	public GameObject [] waves;//array for the waves of enemies.
	public GameObject [] enemy;
	public GameObject [] specialEnemy;
	public GameObject [] pickup;
	public GameObject boss1;
	public GameObject currentBoss;

	//Holds the location of all emitters in an array for easy referencing
	public GameObject [] emitters;
	public GameObject [] topEmitters;
	public GameObject [] bottomEmitters;
	GameObject currentEmitter = null;
	GameObject secondCurrentEmitter;
	GameObject specialCurrentEmitter;


	// Use this for initialization
	void Start () 
	{
		GetArrayDetails ();
		currentEmitter = emitters [1]; //needs to be assigned on start
		secondCurrentEmitter = emitters [2]; //needs to be assigned on start
		StartCoroutine ("Initialization");
	}
	
	// Update is called once per frame
	void Update () 
	{
		RandomGenerator ();
		boss ();
	}

	//gets the details of array length at startup and then saves them to variables for use by the random generators
	void GetArrayDetails () 
	{
		emitterArraySize = emitters.Length ;
		topEmitterArraySize = topEmitters.Length;
		pickupArraySize = pickup.Length;
		specialEnemyArraySize = specialEnemy.Length;
	}

	//generates random numbers to call items from the arrays
	void RandomGenerator () 
	{//if statements to check the gamescore and spawn harder enemies
		if (GameController.gameScore < 5000)
		{
		randomSelector = Random.Range (0, 5); //selects for waves!!!
		}
		else if (GameController.gameScore >= 10000 && GameController.gameScore < 15000)
		{
			randomSelector = Random.Range (0, 10); //selects for waves!!!
		}	
		else if (GameController.gameScore >= 15000)
		{
			randomSelector = Random.Range (0, 15); //selects for waves!!!
		}

		//generates randoms	
		topBottomRandomEmitterNumber = Random.Range (0, topEmitterArraySize);
		currentRandomPickup =  Random.Range (0, pickupArraySize);
		currentRandomSpecialEnemy = Random.Range (0, specialEnemyArraySize);
		randomEmitterSpawn = Random.Range (0, 2);

		//controls where the special enemies spawn
		if (randomEmitterSpawn == 0) 
		{
			specialCurrentEmitter = topEmitters [topBottomRandomEmitterNumber];
		}
		if (randomEmitterSpawn == 1) 
		{
			specialCurrentEmitter = bottomEmitters [topBottomRandomEmitterNumber];
		}
	}

	IEnumerator Initialization () 
	{
		yield return new WaitForSeconds (secondsBeforeBegin);
		StartCoroutine ("SpawnPickup");
		StartCoroutine ("SpawnWaves");
		StartCoroutine ("SpawnSpecialEnemy");
	}

	//rebuilt for waves!!!
	IEnumerator SpawnWaves () 
	{
		while (true) 
		{
			if (randomSelector == 0) 
			{
				Instantiate (waves [0], emitters [4].transform.position, Quaternion.identity);
				Instantiate (waves [1], emitters [4].transform.position, Quaternion.identity);
			} 
			else if (randomSelector == 1) 
			{
				Instantiate (waves [2], emitters [4].transform.position, Quaternion.identity);
				Instantiate (waves [3], emitters [4].transform.position, Quaternion.identity);
			}
			else if (randomSelector == 2) 
			{
				Instantiate (waves [4], emitters [4].transform.position, Quaternion.identity);
				Instantiate (waves [5], emitters [4].transform.position, Quaternion.identity);
			}
			else if (randomSelector == 3) 
			{
				Instantiate (waves [6], emitters [4].transform.position, Quaternion.identity);
				Instantiate (waves [7], emitters [4].transform.position, Quaternion.identity);
			}
			else if (randomSelector == 4) 
			{
				Instantiate (waves [8], emitters [4].transform.position, Quaternion.identity);
				Instantiate (waves [9], emitters [4].transform.position, Quaternion.identity);
			}
			else if (randomSelector == 5) 
			{
				Instantiate (waves [10], emitters [4].transform.position, Quaternion.identity);
				Instantiate (waves [11], emitters [4].transform.position, Quaternion.identity);
			}
			else if (randomSelector == 6) 
			{
				Instantiate (waves [12], emitters [4].transform.position, Quaternion.identity);
				Instantiate (waves [13], emitters [4].transform.position, Quaternion.identity);
			}
			else if (randomSelector == 7) 
			{
				Instantiate (waves [14], emitters [4].transform.position, Quaternion.identity);
				Instantiate (waves [15], emitters [4].transform.position, Quaternion.identity);
			}
			else if (randomSelector == 8) 
			{
				Instantiate (waves [16], emitters [4].transform.position, Quaternion.identity);
				Instantiate (waves [17], emitters [4].transform.position, Quaternion.identity);
			}
			else if (randomSelector == 9) 
			{
				Instantiate (waves [18], emitters [4].transform.position, Quaternion.identity);
				Instantiate (waves [19], emitters [4].transform.position, Quaternion.identity);
			}
			else if (randomSelector == 10) 
			{
				Instantiate (waves [20], emitters [4].transform.position, Quaternion.identity);
				Instantiate (waves [21], emitters [4].transform.position, Quaternion.identity);
			}
			else if (randomSelector == 11) 
			{
				Instantiate (waves [22], emitters [4].transform.position, Quaternion.identity);
				Instantiate (waves [23], emitters [4].transform.position, Quaternion.identity);
			}
			else if (randomSelector == 12) 
			{
				Instantiate (waves [24], emitters [4].transform.position, Quaternion.identity);
				Instantiate (waves [25], emitters [4].transform.position, Quaternion.identity);
			}
			else if (randomSelector == 13) 
			{
				Instantiate (waves [26], emitters [4].transform.position, Quaternion.identity);
				Instantiate (waves [27], emitters [4].transform.position, Quaternion.identity);
			}
			else if (randomSelector == 14) 
			{
				Instantiate (waves [28], emitters [4].transform.position, Quaternion.identity);
				Instantiate (waves [29], emitters [4].transform.position, Quaternion.identity);
			}
				
			yield return new WaitForSeconds (secondsBetweenEnemies);
		}
	}

	//Coroutine that handles spawning enemies
	IEnumerator SpawnSpecialEnemy () 
	{
		while (true) 
		{
			yield return new WaitForSeconds (secondsBetweenSpecialEnemies);
			Instantiate (specialEnemy [currentRandomSpecialEnemy], specialCurrentEmitter.transform.position , Quaternion.identity);
			yield return new WaitForSeconds(secondsBetweenSpecialEnemies); //for testing multiple enemies
		}
	}
	 
	//Coroutine that handles pickup spawning
	IEnumerator SpawnPickup () 
	{
		while (true) 
		{
			Instantiate (pickup [currentRandomPickup], secondCurrentEmitter.transform.position , Quaternion.identity);
			yield return new WaitForSeconds(secondsBetweenPickups);
		}
	}

	void boss ()
	{
		if (GameController.gameScore >= (bossCounter * 15000) && canSpawnBoss == true) 
		{
			StopCoroutine ("SpawnWaves");
			StopCoroutine ("SpawnSpecialEnemy");
			currentBoss = Instantiate (boss1, emitters [4].transform.position, Quaternion.identity) as GameObject;
			canSpawnBoss = false;
			bossSpawned = true;
			bossCounter++;
		}
		if (currentBoss == null && bossSpawned == true) 
		{
			StartCoroutine ("SpawnWaves");
			StartCoroutine ("SpawnSpecialEnemy");
			bossSpawned = false;
			canSpawnBoss = true;
		}
	}
}
