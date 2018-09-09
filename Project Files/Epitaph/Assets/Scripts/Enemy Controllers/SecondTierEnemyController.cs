using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondTierEnemyController : MonoBehaviour {

	public int health = 200;

	//Attack Variables
	public float delayBetweenProjectiles = 1.5f;
	public GameObject frontEmitter;
	public GameObject topEmitter;
	public GameObject bottomEmitter;
	public GameObject projectile1;
	public GameObject projectile2;
	public GameObject enemyWaveController;

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
    SpriteRenderer tierTwoSprite;

    // Use this for initialization
    void Start ()
    {
		waveParent = transform.root.GetComponent<WaveEnd> ();
		randomChance = Random.Range (0, 21);
		pickupNumber = Random.Range (0, 2);

		delayBetweenProjectiles = 1.0f;
		SecondTierWave waveScript = enemyWaveController.GetComponent<SecondTierWave> ();//brings in script from standardwavecontroller.
		StartCoroutine ("Attack"); //starts a coroutine running for firing projectiles
        tierTwoSprite = GetComponent<SpriteRenderer>(); 
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
			GameController.gameScore += (20 * GameController.multiplier);
			theText.text = "+" + (20 * GameController.multiplier);
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
			Instantiate (projectile1, topEmitter.transform.position, Quaternion.identity);
			Instantiate (projectile1, bottomEmitter.transform.position, Quaternion.identity);
			yield return new WaitForSeconds (delayBetweenProjectiles);
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
        tierTwoSprite.color = hit;
        yield return new WaitForSeconds(0.125f);
        tierTwoSprite.color = standard;
    }
}
