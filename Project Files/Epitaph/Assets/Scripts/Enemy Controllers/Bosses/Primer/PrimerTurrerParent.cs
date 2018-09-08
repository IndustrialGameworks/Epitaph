using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimerTurrerParent : MonoBehaviour
{

	public PrimerTurret turretController;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		StartCoroutine ("destroyThis");
	}

	IEnumerator destroyThis ()
	{
		if (turretController.isDestroyed == true) 
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
