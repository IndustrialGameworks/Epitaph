using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamDiamondParent : MonoBehaviour 
{
	public BeamDiamondController beamDiamondController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		StartCoroutine ("destroyThis");
	}

	IEnumerator destroyThis ()
	{
		if (beamDiamondController.isDestroyed == true) 
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
