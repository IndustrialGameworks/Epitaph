using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotusEnemyController : MonoBehaviour {

	public int health = 100;
	public GameObject [] emitters;
	public GameObject origin;
	public GameObject movementController; //parent that doesn't change rotation, so can still use world vectors
	public float rotationSpeed = 5;

	GameObject spawnedProjectile;
	public Vector2 centerOfMass;

	//Movement Variables
	public float movementSpeed = 3;

	bool constructLocation = true;

	int randomX;
	int randomY;
	Vector2 randomVector;

	//text variables
	public GameObject pointsText;
	TextMesh theText;
	public bool isDestroyed;

	//pickup variables
	public GameObject [] pickups;
	int randomChance;
	int pickupNumber;

	//Attack Variables
	public float delayBetweenProjectiles = 1.5f;
	public GameObject projectile;

    //color variables
    Color hit = new Color(145f / 255f, 50f / 255f, 50f / 255f, 1);
    Color standard = Color.white;
    SpriteRenderer lotusSprite;
    public GameObject deathParticle;

    // Use this for initialization
    void Start () {
		RandomGeneration ();
		randomChance = Random.Range (0, 21);
		pickupNumber = Random.Range (0, 2);

		centerOfMass = origin.transform.position; //assigns center of object so that its projectiles can access it for their translation
		StartCoroutine ("BuildRandomLocation");
		StartCoroutine ("Attack"); //starts a coroutine running for firing projectiles
        lotusSprite = GetComponent<SpriteRenderer>();
		theText = pointsText.GetComponent<TextMesh>();//calls textmesh.
	}
	
	// Update is called once per frame
	void Update () {
		Movement ();
		Status ();

		if (constructLocation == true) {
			RandomGeneration ();
			constructLocation = false;
		}
	}


	void Movement () {
		movementController.transform.position = Vector2.MoveTowards(new Vector2(movementController.transform.position.x, movementController.transform.position.y), randomVector, 3f * Time.deltaTime);
		//movementController.transform.Translate (randomVector * movementSpeed * Time.deltaTime); 
		transform.Rotate (Vector3.forward * rotationSpeed);
		centerOfMass = origin.transform.position;
	}

	void Status () {
		if (health <= 0) {
			
			GameController.gameScore += (25 * GameController.multiplier);
			theText.text = "+" + (25 * GameController.multiplier);
			isDestroyed = true;
			GameController.multiplier += 1;
			GameController.timer = 180.0f;
			if (randomChance == 10) 
			{
				Instantiate (pickups [pickupNumber], gameObject.transform.position, Quaternion.identity);
			}
            if (deathParticle != null)
            {
                Instantiate(deathParticle, transform.position, Quaternion.identity);
            }
            Destroy (gameObject);
		}
	}

	void RandomGeneration () { //generates random x and y coords within bounds
		randomX = Random.Range (-5, 10); //x constraints
		randomY = Random.Range (-4, 4); //y constraints
		randomVector = new Vector2 (randomX, randomY); //constructs new random vector2
	}

	IEnumerator Attack () {
		while (true) {
			spawnedProjectile = Instantiate (projectile, emitters [0].transform.position, Quaternion.identity) as GameObject; //spawns projectile, returns gameObject
			spawnedProjectile.gameObject.GetComponent<lotusProjectile>().spawnOrigin = gameObject; //assigns this enemy instance to the projectile as point of origin

			spawnedProjectile = Instantiate (projectile, emitters [1].transform.position, Quaternion.identity) as GameObject;
			spawnedProjectile.gameObject.GetComponent<lotusProjectile>().spawnOrigin = gameObject;

			spawnedProjectile = Instantiate (projectile, emitters [2].transform.position, Quaternion.identity) as GameObject;
			spawnedProjectile.gameObject.GetComponent<lotusProjectile>().spawnOrigin = gameObject;

			spawnedProjectile = Instantiate (projectile, emitters [3].transform.position, Quaternion.identity) as GameObject;
			spawnedProjectile.gameObject.GetComponent<lotusProjectile>().spawnOrigin = gameObject;

			spawnedProjectile = Instantiate (projectile, emitters [4].transform.position, Quaternion.identity) as GameObject;
			spawnedProjectile.gameObject.GetComponent<lotusProjectile>().spawnOrigin = gameObject;

			yield return new WaitForSeconds (delayBetweenProjectiles);
		}
	}

	IEnumerator BuildRandomLocation () {
		while (true) {
			yield return new WaitForSeconds (10);
			constructLocation = true;
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "PlayerProjectile") {
			health -= 50;
			Destroy (other.gameObject);
            StartCoroutine("changeColor");
        }
	}

	void OnBecameInvisible ()
	{
		isDestroyed = true;
		Destroy (gameObject);
	}

    IEnumerator changeColor()
    {
        lotusSprite.color = hit;
        yield return new WaitForSeconds(0.125f);
        lotusSprite.color = standard;
    }
}
