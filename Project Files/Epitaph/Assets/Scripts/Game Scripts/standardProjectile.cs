using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class standardProjectile : MonoBehaviour {

	public bool isEnemy;
	public float projectileSpeed = 10;
	public float delayBeforeDestroy = 5;

	// Use this for initialization
	void Start () {
		StartCoroutine ("DestroySelf");
	}

	// Update is called once per frame
	void Update () {
		Movement ();
		TagControl ();
	}

	void Movement () {
		if (isEnemy == false) {
			transform.Translate (Vector2.right * projectileSpeed * Time.deltaTime);
		}
		if (isEnemy == true) {
			transform.Translate (Vector2.left * projectileSpeed * Time.deltaTime);
		}
	}

	void TagControl () {
		if (isEnemy == true) {
			gameObject.tag = "EnemyProjectile";
		} else {
			gameObject.tag = "PlayerProjectile";
		}
	}
		
	IEnumerator DestroySelf () {
		yield return new WaitForSeconds(delayBeforeDestroy);
		Destroy (gameObject);
	}
}
