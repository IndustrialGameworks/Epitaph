using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script controls the primer turret.It makes the turret rotate depending on the location of the player in the scene and also emmits projectiles.

public class PrimerTurret : MonoBehaviour
{
    //Ints.
	public int health = 1000;
    
    //Floats.
	public float rotationSpeed = 5;
	public float volleySeperation = 0.3f;
	public float delayBetweenProjectiles = 4;
	public float delayBeforeStart = 0.5f;

    //Gameobjects.
    public GameObject emitter;
    public GameObject origin;
    public GameObject player;
    public GameObject pointsText;
    public GameObject projectile;
    public GameObject turretParent;
    GameObject spawnedProjectile;

    //Vector2.
    public Vector2 centerOfMass;

	//text variables
	TextMesh theText;

    //Booleans.
	public bool isDestroyed;
	
	//color variables
	Color hit = new Color (145f/255f, 50f/255f, 50f/255f, 1);
	Color standard = Color.white;
	SpriteRenderer turretSprite;

	// Use this for initialization
	void Start ()
    {
		turretSprite = GetComponent<SpriteRenderer> ();//gets the sprite renderer for this gameobject.
		theText = pointsText.GetComponent<TextMesh>();//gets the text mesh of this text object.
		StartCoroutine ("Attack");//start the attack coroutine for the start.
	}
	
	// Update is called once per frame
	void Update ()
    {
		player = GameObject.FindGameObjectWithTag ("Player"); //has to be be called on update during testing, as there is no player at the time this instance starts.
		RotateToTarget();
		centerOfMass = origin.transform.position;
		Status ();


	}

	void RotateToTarget ()
    { //code to rotate to look at target
		if (player != null) //checks if a player exists.
		{
			Vector3 vectorToTarget = player.transform.position - transform.position;
			float angle = Mathf.Atan2 (-vectorToTarget.y, -vectorToTarget.x) * Mathf.Rad2Deg; //take out the minuses if you want to invert the side that tracks the player
			Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
			transform.rotation = Quaternion.Slerp (transform.rotation, q, Time.deltaTime * rotationSpeed);//rotates the turret towards the player.
		}
	}

	void OnTriggerEnter2D (Collider2D other)//when another collider enter this gameobjects collider.
	{
		if (other.tag == "PlayerProjectile")//if its a player projectile.
        {
			health -= 50;//take away some health from this game object.
			Destroy (other.gameObject);//destroy the player projectile.
			StartCoroutine ("changeColor");//run this co routine.
		}
	}

	IEnumerator Attack ()
    {
		while (true)
        {
			yield return new WaitForSeconds (delayBeforeStart);//wait for this amount of time.
			spawnedProjectile = Instantiate (projectile, emitter.transform.position, Quaternion.identity) as GameObject; //spawns projectile, returns gameObject
			spawnedProjectile.gameObject.GetComponent<primerProjectile>().spawnOrigin = gameObject; //assigns this enemy instance to the projectile as point of origin

			yield return new WaitForSeconds (volleySeperation);

			spawnedProjectile = Instantiate (projectile, emitter.transform.position, Quaternion.identity) as GameObject; //spawns projectile, returns gameObject
			spawnedProjectile.gameObject.GetComponent<primerProjectile>().spawnOrigin = gameObject; //assigns this enemy instance to the projectile as point of origin

			yield return new WaitForSeconds (volleySeperation);

			spawnedProjectile = Instantiate (projectile, emitter.transform.position, Quaternion.identity) as GameObject; //spawns projectile, returns gameObject
			spawnedProjectile.gameObject.GetComponent<primerProjectile>().spawnOrigin = gameObject; //assigns this enemy instance to the projectile as point of origin

			yield return new WaitForSeconds (volleySeperation);

			spawnedProjectile = Instantiate (projectile, emitter.transform.position, Quaternion.identity) as GameObject; //spawns projectile, returns gameObject
			spawnedProjectile.gameObject.GetComponent<primerProjectile>().spawnOrigin = gameObject; //assigns this enemy instance to the projectile as point of origin

			yield return new WaitForSeconds (volleySeperation);

			spawnedProjectile = Instantiate (projectile, emitter.transform.position, Quaternion.identity) as GameObject; //spawns projectile, returns gameObject
			spawnedProjectile.gameObject.GetComponent<primerProjectile>().spawnOrigin = gameObject; //assigns this enemy instance to the projectile as point of origin

			yield return new WaitForSeconds (volleySeperation);

			spawnedProjectile = Instantiate (projectile, emitter.transform.position, Quaternion.identity) as GameObject; //spawns projectile, returns gameObject
			spawnedProjectile.gameObject.GetComponent<primerProjectile>().spawnOrigin = gameObject; //assigns this enemy instance to the projectile as point of origin

			yield return new WaitForSeconds (delayBetweenProjectiles);
		}
	}

	void Status ()
    {
		if (health <= 0)
        {

			GameController.gameScore += (250 * GameController.multiplier);//returns score to the game controller.
			theText.text = "+" + (250 * GameController.multiplier);//edits the text of this textmesh.
            //pointsText.transform.SetParent (turretParent.transform);
            isDestroyed = true;
			GameController.multiplier += 1;//adds 1 to the games multiplier and also resets the timer before the multiplier degrades.
			GameController.timer = GameController.resetTimer;
			Destroy (gameObject);//destroy this gameobject.
		}
	}

	IEnumerator changeColor ()//changes the color of the sprite renderer on hit.
	{
		turretSprite.color = hit;//change to red.
		yield return new WaitForSeconds (0.125f);//wait this long.
		turretSprite.color = standard;//return to normal.
	}
}
