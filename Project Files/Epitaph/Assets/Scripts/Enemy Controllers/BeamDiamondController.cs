using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamDiamondController : MonoBehaviour {

	public int health = 10;

	public float movementSpeed = 5;
	public float indicatorFireDelay = 1.25f;
	public float beamFireDuration = 2;
	public GameObject indicatorBeam;
	public GameObject fullBeam;
	public GameObject playerToTrack;
	float playerAxisY;
	float thisAxisY;
	float axisDifference;
	bool isFiring;

	public GameObject BeamDiamondParent;
	public GameObject pointsText;
	TextMesh theText;
	public bool isDestroyed = false;

	//pickup variables
	public GameObject [] pickups;
	int randomChance;
	int pickupNumber;

	// Use this for initialization
	void Start () {
		randomChance = Random.Range (0, 21);
		pickupNumber = Random.Range (0, 2);
		playerToTrack = GameObject.FindWithTag ("Player");
		theText = pointsText.GetComponent<TextMesh> ();
	}
	
	// Update is called once per frame
	void Update () {
		Movement ();
		Status ();
	}

	void Movement () {
		if (playerToTrack != null) {
			playerAxisY = playerToTrack.transform.position.y;
			thisAxisY = gameObject.transform.position.y;
			axisDifference = playerAxisY - thisAxisY;
			if (axisDifference > 0.25 && isFiring == false) {
				transform.Translate (Vector2.up * movementSpeed * Time.deltaTime);
			} else if (axisDifference < -0.25 && isFiring == false) {
				transform.Translate (Vector2.down * movementSpeed * Time.deltaTime);
			} else {
				StartCoroutine ("Attack");
			}
		}
	}

	void Status () {
		if (health <= 0) {
			
			GameController.gameScore += (25 * GameController.multiplier);
			theText.text = "+" + (25 * GameController.multiplier);
			pointsText.transform.SetParent (BeamDiamondParent.transform);
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

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "PlayerProjectile") 
		{
			health -= 50;
			Destroy (other.gameObject);
		}
	}


	IEnumerator Attack () {
		isFiring = true;
		indicatorBeam.gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		yield return new WaitForSeconds (indicatorFireDelay);
		indicatorBeam.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		fullBeam.gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		fullBeam.gameObject.GetComponent<BoxCollider2D> ().enabled = true;
		yield return new WaitForSeconds (beamFireDuration);
		fullBeam.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		fullBeam.gameObject.GetComponent<BoxCollider2D> ().enabled = false;
		isFiring = false;
		StopCoroutine ("Attack");
	}

	void OnBecameInvisible ()
	{
		isDestroyed = true;
		Destroy (gameObject);
	}
}
