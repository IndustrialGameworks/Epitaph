using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lotusParent : MonoBehaviour 
{
	public LotusEnemyController lotusController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		StartCoroutine ("destroyThis");
	}

	IEnumerator destroyThis ()
	{
		if (lotusController.isDestroyed == true) 
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
