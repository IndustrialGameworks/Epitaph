using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlledStandardEnemyController : MonoBehaviour {

	int health = 50;

	//Movement Variables
	public float movementSpeed = 3;

	//Attack Variables
	public float delayBetweenProjectiles;
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
	public float distance = 1;
	public int navPointsComplete = 0;
	public float waveSpeed = 3;

	//text variables
	public GameObject pointsText;
	TextMesh theText;
	public bool isDestroyed = false;

	//pickup variables
	public GameObject [] pickups;
	int randomChance;
	int pickupNumber;

	//editable variables
	public WaveEnd waveParent;
    //public GameObject root;

    //color variables
    Color hit = new Color(145f / 255f, 50f / 255f, 50f / 255f, 1);
    Color standard = Color.white;
    SpriteRenderer tierOneSprite;

    // Use this for initialization
    void Start () {
		waveParent = transform.root.GetComponent<WaveEnd> ();
		randomChance = Random.Range (0, 21);
		pickupNumber = Random.Range (0, 2);

		delayBetweenProjectiles = 1.0f;
		StandardEnemyWaveController waveScript = enemyWaveController.GetComponent<StandardEnemyWaveController> ();//brings in script from standardwavecontroller.
		StartCoroutine ("Attack"); //starts a coroutine running for firing projectiles
        tierOneSprite = GetComponent<SpriteRenderer>();
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
			GameController.gameScore += (10 * GameController.multiplier);
			theText.text = "+" + (10 * GameController.multiplier);
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
			Instantiate (projectile1, frontEmitter.transform.position, Quaternion.identity);
			yield return new WaitForSeconds (delayBetweenProjectiles);
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "PlayerProjectile")
        {
            StartCoroutine("changeColor");
            health -= 50;
			Destroy (other.gameObject);
        }
	}

	void OnBecameInvisible ()
	{
		isDestroyed = true;
		Destroy (gameObject);
	}

    IEnumerator changeColor()
    {
        tierOneSprite.color = hit;
        yield return new WaitForSeconds(0.125f);
        tierOneSprite.color = standard;
    }
}
