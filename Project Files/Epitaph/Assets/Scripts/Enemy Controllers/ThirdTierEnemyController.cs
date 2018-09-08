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

    //color variables
    Color hit = new Color(145f / 255f, 50f / 255f, 50f / 255f, 1);
    Color standard = Color.white;
    SpriteRenderer tierThreeSprite;


    // Use this for initialization
    void Start ()
    {
		//moveDown = (Random.value > 0.5f);
		//StartCoroutine ("Attack"); //starts a coroutine running for firing projectiles
		waveParent = transform.root.GetComponent<WaveEnd> ();
		randomChance = Random.Range (0, 21);
		pickupNumber = Random.Range (0, 2);

		delayBetweenProjectiles = 1.0f;
		ThirdTierWave waveScript = enemyWaveController.GetComponent<ThirdTierWave> ();//brings in script from standardwavecontroller.
		StartCoroutine ("Attack"); //starts a coroutine running for firing projectiles
        tierThreeSprite = GetComponent<SpriteRenderer>();
		theText = pointsText.GetComponent<TextMesh>();//calls textmesh.
	}
	
	// Update is called once per frame
	void Update ()
    {
		Status ();
	}

	void Status ()
    {
		if (health <= 0)
        {
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

	IEnumerator Attack ()
    {
		while (true)
        {
			if (cycleFire == false)
            {
				Instantiate (projectile1, frontEmitter.transform.position, Quaternion.identity);
				Instantiate (projectile1, topEmitter.transform.position, Quaternion.identity);
				Instantiate (projectile1, bottomEmitter.transform.position, Quaternion.identity);
				yield return new WaitForSeconds (delayBetweenProjectiles);
			}
			if (cycleFire == true)
            {
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
		if (other.tag == "PlayerProjectile")
        {
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
        tierThreeSprite.color = hit;
        yield return new WaitForSeconds(0.125f);
        tierThreeSprite.color = standard;
    }
}
