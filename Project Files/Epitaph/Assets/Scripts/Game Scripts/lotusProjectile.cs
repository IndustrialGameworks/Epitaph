using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lotusProjectile : MonoBehaviour {

	public float projectileSpeed = 10;
	public float delayBeforeDestroy = 5;
	public GameObject spawnOrigin; //assigned by instantiator upon instantiation
	Vector2 currentPosition;
	Vector2 storedCenterOfMass;

	// Use this for initialization
	void Start () {
		storedCenterOfMass = spawnOrigin.GetComponent<LotusEnemyController> ().centerOfMass;
		StartCoroutine ("DestroySelf");
	}
	
	// Update is called once per frame
	void Update () {
		currentPosition = this.transform.position;
		Movement ();
	}

	void Movement () {
		Vector2 newVector = currentPosition - storedCenterOfMass;
		transform.Translate (newVector * projectileSpeed * Time.deltaTime);
	}

	IEnumerator DestroySelf () {
		yield return new WaitForSeconds(delayBeforeDestroy);
		Destroy (gameObject);
	}
}
