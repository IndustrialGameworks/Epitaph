using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class primerProjectile : MonoBehaviour {

	public float projectileSpeed = 10;
	public float delayBeforeDestroy = 5;
	public GameObject spawnOrigin; //reference to the instantiator, assigned by instantiator upon instantiation
	Vector2 currentPosition; //position of this projectile
	Vector2 storedCenterOfMass; //center of mass of instantiator upon instantiation

	// Use this for initialization
	void Start () {
		storedCenterOfMass = spawnOrigin.GetComponent<PrimerTurret> ().centerOfMass; //gets the center of mass from the reference to its originator upon creation, otherwise it would be a non-satic value
	}
	
	// Update is called once per frame
	void Update () {
		currentPosition = this.transform.position;
		Movement ();
	}

	void Movement () {
		Vector2 newVector = currentPosition - storedCenterOfMass;
		newVector.Normalize ();
		transform.Translate (newVector * projectileSpeed * Time.deltaTime);
	}

	void OnBecameInvisible ()
	{
		Destroy (gameObject);
	}
}
