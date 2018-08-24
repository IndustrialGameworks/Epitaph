using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimerBossController : MonoBehaviour {

	public bool secondStage = false;
	bool secondStageInitiated = false;

	//Arrays
	public GameObject [] turrets;

	//Movement
	public float movementSpeed = 2f;
	bool constructLocation; //if true, new target location will be constructed;
	float minConstraintX = 5;
	float maxConstraintX = 8;
	float minConstraintY = 1;
	float maxConstraintY = -1;
	public float currentLocationX; //current location X
	public float currentLocationY; //current location Y
	float randomX;
	float randomY;
	Vector2 randomVector;

	//Attack
	float delayBetweenProjectiles = 0.2f;
	public GameObject internalEmitter1;
	public GameObject secondStageEmitter1;
	public GameObject internalEmitter2;
	public GameObject secondStageEmitter2;
	public GameObject projectile1;
	public GameObject projectile2;
	GameObject spawnedProjectile;

	//Core door control
	public bool coreDoorOpen; //if true, door opens
	public float stageOneDoorDelay = 5f;
	public GameObject coreDoor; //get the door object
	public float coreDoorRotationSpeed = 5; //door rotation speed
	float coreDoorRotationValue; //current door rotation
	int nullElements;
	int numberOfEnemiesInArray;
	public GameObject core;


	// Use this for initialization
	void Start () {
		numberOfEnemiesInArray = turrets.Length;
		RandomGeneration ();
		StartCoroutine ("BuildRandomLocation");
		StartCoroutine ("firstStageAttack");
	}
	
	// Update is called once per frame
	void Update () {
		CoreDoorMovement ();
		Movement ();
		checkArray ();
		StartCoroutine ("destroyThis");
		if (constructLocation == true) {
			RandomGeneration ();
			constructLocation = false;
		}

		if (secondStage == true && secondStageInitiated == false) {
			StopCoroutine ("firstStageAttack");
			StartCoroutine ("secondStageAttack");
			secondStageInitiated = true;
		}
	}

	void Movement () {
		transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), randomVector, movementSpeed * Time.deltaTime);
		MovementChecks ();

	}
	
	void RandomGeneration () { //generates random x and y coords within bounds
		randomX = Random.Range (minConstraintX, maxConstraintX); //x constraints
		randomY = Random.Range (minConstraintY, maxConstraintY); //y constraints
		randomVector = new Vector2 (randomX, randomY); //constructs new random vector2
		}

	void CoreDoorMovement () {
		coreDoorRotationValue = coreDoor.transform.rotation.z;
		if (coreDoorOpen == true) {
			if (coreDoorRotationValue < 0.999) {
				coreDoor.transform.Rotate (0, 0, coreDoorRotationSpeed * Time.deltaTime);
			} 
			else {
				//do nothing
			}
		}
		if (coreDoorOpen == false) {
			if (coreDoorRotationValue > 0) {
				coreDoor.transform.Rotate (0, 0, -coreDoorRotationSpeed * Time.deltaTime);
			} 
			else {
				//do nothing ,door rotation = 0
			}
		}
	}

	IEnumerator destroyThis ()
	{
		if (core == null) 
		{
			yield return new WaitForSeconds (0.5f);
			Destroy (gameObject);
		}
	}

	void checkArray ()
	{
		nullElements = 0;
		for (int i = 0; i < numberOfEnemiesInArray; i++) 
		{
			if (turrets [i] == null) 
			{
				nullElements++;
				//Debug.Log (nullElements.ToString ());
			}
		}
		if (nullElements == numberOfEnemiesInArray) 
		{
			coreDoorOpen = true;
			secondStage = true;
		}
	}

	void MovementChecks () {
		currentLocationX = transform.position.x;
		currentLocationY = transform.position.y;
	}

	IEnumerator firstStageAttack () {
		while (true) {
			yield return new WaitForSeconds (stageOneDoorDelay);
			coreDoorOpen = true;
			yield return new WaitForSeconds (4);
			Instantiate (projectile1, internalEmitter1.transform.position, Quaternion.identity);
			Instantiate (projectile1, internalEmitter2.transform.position, Quaternion.identity);
			yield return new WaitForSeconds (delayBetweenProjectiles);
			Instantiate (projectile1, internalEmitter1.transform.position, Quaternion.identity);
			Instantiate (projectile1, internalEmitter2.transform.position, Quaternion.identity);
			yield return new WaitForSeconds (delayBetweenProjectiles);
			Instantiate (projectile1, internalEmitter1.transform.position, Quaternion.identity);
			Instantiate (projectile1, internalEmitter2.transform.position, Quaternion.identity);
			yield return new WaitForSeconds (delayBetweenProjectiles);
			Instantiate (projectile1, internalEmitter1.transform.position, Quaternion.identity);
			Instantiate (projectile1, internalEmitter2.transform.position, Quaternion.identity);
			yield return new WaitForSeconds (delayBetweenProjectiles);
			Instantiate (projectile1, internalEmitter1.transform.position, Quaternion.identity);
			Instantiate (projectile1, internalEmitter2.transform.position, Quaternion.identity);
			coreDoorOpen = false;
		}
	}


	IEnumerator secondStageAttack () {
		while (true) {
			yield return new WaitForSeconds (0.2f);
			//Debug.Log ("2nd stage initialized");

			spawnedProjectile = Instantiate (projectile2, internalEmitter1.transform.position, Quaternion.identity) as GameObject; //spawns projectile, returns gameObject
			spawnedProjectile.gameObject.GetComponent<primerSecondStageProjectile>().spawnOrigin = internalEmitter1.gameObject; //assigns this enemy instance to the projectile as point of origin
			spawnedProjectile = Instantiate (projectile2, secondStageEmitter1.transform.position, Quaternion.identity) as GameObject; //spawns projectile, returns gameObject
			spawnedProjectile.gameObject.GetComponent<primerSecondStageProjectile>().spawnOrigin = secondStageEmitter1.gameObject; //assigns this enemy instance to the projectile as point of origin


			spawnedProjectile = Instantiate (projectile2, internalEmitter2.transform.position, Quaternion.identity) as GameObject;
			spawnedProjectile.gameObject.GetComponent<primerSecondStageProjectile>().spawnOrigin = internalEmitter2.gameObject;
			spawnedProjectile = Instantiate (projectile2, secondStageEmitter2.transform.position, Quaternion.identity) as GameObject; //spawns projectile, returns gameObject
			spawnedProjectile.gameObject.GetComponent<primerSecondStageProjectile>().spawnOrigin = secondStageEmitter2.gameObject; //assigns this enemy instance to the projectile as point of origin
		}
	}

	IEnumerator BuildRandomLocation () { //waits a set amount of time, the signals to construct a new new target vector
		while (true) {
			yield return new WaitForSeconds (5);
			constructLocation = true;
		}
	}
}
