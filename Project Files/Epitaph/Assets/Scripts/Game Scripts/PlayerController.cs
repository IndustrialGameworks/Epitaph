using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	//Movement Variables
	public float controllerX;
	public float controllerY;
	public float controllerSpeed;

	//Attack Variables
	public float delayBeforeFirstAttack = 3.8f;
	public float delayBetweenProjectiles = 1.5f;
	public GameObject frontEmitter;
	public GameObject topEmitter;
	public GameObject bottomEmitter;
	public GameObject projectile1;
	public GameObject projectile2;
	public bool isDead = false;

	//Pickup Variables
	public float pickupLength = 5;
	public bool singleEmitter = true;
	public bool doubleEmitter = false;
	public bool tripleEmitter = false;

	//screen bounds
	Vector2 bottomCorner;
	Vector2 topCorner;
	private float minX, maxX, minY, maxY;

    //renderer and color variables.
    public SpriteRenderer topEmitterSprite;
    public SpriteRenderer bottomEmitterSprite;
    public SpriteRenderer centerEmitterSprite;
    public GameObject deathParticle;

	void Start ()
    {
        if (topEmitterSprite != null && bottomEmitterSprite != null)
        {
            topEmitterSprite.gameObject.SetActive(false);
            bottomEmitterSprite.gameObject.SetActive(false);
        }
        centerEmitterSprite = gameObject.GetComponent<SpriteRenderer>();
        bottomCorner = Camera.main.ViewportToWorldPoint(new Vector2(0,0));//sets the top and bottom corner vector to equal loaction of top and bottom corners. 
        topCorner = Camera.main.ViewportToWorldPoint(new Vector2(1,1));
		StartCoroutine ("Initialization"); //starts a coroutine running for firing projectiles
	}

	void Update ()
    {
		setBounds ();
		ControllerPosition ();
		Movement ();
	}

	void setBounds ()
	{
        //takes the minimum and maximum x and y values from the top and bottom corner vectors.
        minX = bottomCorner.x;
		maxX = topCorner.x;
		minY = bottomCorner.y;
		maxY = topCorner.y;
	}

	void Movement ()
    {
		Vector3 MovementVector;
		MovementVector = new Vector3 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"), 0.0f);//movement for WASD and UP/DOWN/LEFT/RIGHT keys.
		if (transform.position.x >= minX && transform.position.x <= maxX && transform.position.y >= minY && transform.position.y <= maxY) //checks if the player is within the area constraints andthen allows them to move.
		{
			transform.Translate (MovementVector.normalized * controllerSpeed * Time.deltaTime);
		}
        //if the player goes outside the minimum or maximum x or y move them back in.
        else if (transform.position.x <= minX) 
		{
			controllerX = minX + 0.001f;
			transform.position = new Vector2 (controllerX, controllerY);
		} 
		//else if (transform.position.x >= maxX) 
		//{
		//	controllerX = maxX - 0.001f;
		//	transform.position = new Vector2 (controllerX, controllerY);
		//} 
		else if (transform.position.y <= minY) 
		{
			controllerY = minY + 0.001f;
			transform.position = new Vector2 (controllerX, controllerY);
		} 
		else if (transform.position.y >= maxY) 
		{
			controllerY = maxY - 0.001f;
			transform.position = new Vector2 (controllerX, controllerY);
		}
	}

	IEnumerator Initialization()
	{
        //wait before first attack.
        yield return new WaitForSeconds (delayBeforeFirstAttack);
		StartCoroutine ("Attack");
	}

	IEnumerator PickupTimer ()
    { 
        //controls the length of time pickups are active for.
		while (pickupLength > 0)
        {
			yield return new WaitForSeconds (1);
			pickupLength--;
		}
		singleEmitter = true;//once pickup timer runs out revert back to standard attack coroutine.
		StartCoroutine ("Attack");
		StopCoroutine ("DualAttack");
		StopCoroutine ("TripleAttack");
	}

	IEnumerator Attack ()
    { 
        //standard attack from main emitter.
		while (singleEmitter == true)
        {
			StopCoroutine ("PickupTimer");
            if (topEmitterSprite != null && bottomEmitterSprite != null)
            {
                topEmitterSprite.gameObject.SetActive(false);
                bottomEmitterSprite.gameObject.SetActive(false);
            }
            Instantiate (projectile1, frontEmitter.transform.position, Quaternion.identity);
			yield return new WaitForSeconds (delayBetweenProjectiles);
		}
	}

	IEnumerator DualAttack ()
    {
		while (doubleEmitter == true)
        {
            //starts firing from top and bottom emitters.
            if (topEmitterSprite != null && bottomEmitterSprite != null)
            {
                topEmitterSprite.gameObject.SetActive(true);
                bottomEmitterSprite.gameObject.SetActive(true);
            }
            Instantiate (projectile1, topEmitter.transform.position, Quaternion.identity);
			Instantiate (projectile1, bottomEmitter.transform.position, Quaternion.identity);
			yield return new WaitForSeconds (delayBetweenProjectiles);
		}
	}

	IEnumerator TripleAttack ()
    { 
        //start firing from all emitters.
		while (tripleEmitter == true)
        {
            if (topEmitterSprite != null && bottomEmitterSprite != null)
            {
                topEmitterSprite.gameObject.SetActive(true);
                bottomEmitterSprite.gameObject.SetActive(true);
            }
            Instantiate (projectile1, frontEmitter.transform.position, Quaternion.identity);
			Instantiate (projectile1, topEmitter.transform.position, Quaternion.identity);
			Instantiate (projectile1, bottomEmitter.transform.position, Quaternion.identity);
			yield return new WaitForSeconds (delayBetweenProjectiles);
		}
	}

	void ControllerPosition ()
    { 
        //gets the x and y of the controllers position.
		controllerX = transform.position.x;
		controllerY = transform.position.y;
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "EnemyProjectile" || other.tag == "Enemy")//if collided with an enemy or enemy projectile.
        {
			isDead = true;//destroy this game object and its parent and set isDead bool to true.
            if (deathParticle != null)
            {
                Instantiate(deathParticle, transform.position, Quaternion.identity);
            }
            Destroy (gameObject);
            Destroy(transform.parent.gameObject);
		}
		if (other.tag == "PickupDualBlast")//if the pickup is a dual blast.
        {
			doubleEmitter = true;//enables / disables certain emmiters.
            singleEmitter = false;
			tripleEmitter = false;

			pickupLength = 5;
            StopCoroutine("DualAttack");//stops the dual attack and timer if they pick up two in a row.
            StopCoroutine("PickupTimer");
            StartCoroutine ("DualAttack"); //starts a coroutine running for firing projectiles
			StartCoroutine ("PickupTimer");//starts coroutine to time the pickup.
            StopCoroutine ("Attack");//stops attack and triple attack coroutines.
            StopCoroutine ("TripleAttack");
		}
		if (other.tag == "PickupTripleBlast")//if the pickup is a tri blast.
        {
			tripleEmitter = true;//enables / disables certain emmiters.
            doubleEmitter = false;
			singleEmitter = false;

			pickupLength = 5;
            StopCoroutine("TripleAttack");//stops the tri attack and timer if they pick up two in a row.
            StopCoroutine("PickupTimer");
            StartCoroutine ("TripleAttack"); //starts a coroutine running for firing projectiles
			StartCoroutine ("PickupTimer");//starts coroutine to time the pickup.
            StopCoroutine ("Attack");//stops attack and dual attack coroutines.
            StopCoroutine ("DualAttack");
		}
	}
}
