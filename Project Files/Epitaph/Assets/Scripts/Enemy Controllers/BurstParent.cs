using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstParent : MonoBehaviour {

	public BurstEnemyController BurstController;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		StartCoroutine ("destroyThis");
	}

	IEnumerator destroyThis ()
	{
		if (BurstController.isDestroyed == true) 
		{
			yield return new WaitForSeconds (0.5f);
			Destroy (gameObject);
		}
	}

	void OnBecameInvisible ()
	{
		Destroy (gameObject);
	}
}
