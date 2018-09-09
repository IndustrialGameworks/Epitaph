using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardEnemyWaveController : MonoBehaviour {

	//Arrays
	public GameObject[] enemiesInWave;
	public ControlledStandardEnemyController enemyController;
	public GameObject pointsText;

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
		if (enemyController.isDestroyed == true) 
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
