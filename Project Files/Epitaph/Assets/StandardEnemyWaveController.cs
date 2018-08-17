using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardEnemyWaveController : MonoBehaviour {

	//Arrays
	public GameObject[] enemiesInWave;
	public GameObject[] navigationPoints;
	public int navPointsComplete1 = 0;
	public int navPointsComplete2 = 0;
	public int navPointsComplete3 = 0;

	public Vector2 navLocation1;
	public Vector2 navLocation2;
	public Vector2 navLocation3;

	public ControlledStandardEnemyController enemyController;


	//Movement
	public float waveSpeed;
	public GameObject pointsText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Movement ();
		StartCoroutine ("destroyThis");
	}

	void Movement () {

	}

	IEnumerator destroyThis ()
	{
		if (enemyController.isDestroyed == true) 
		{
			yield return new WaitForSeconds (0.5f);
			Destroy (gameObject);
		}
	}

//	IEnumerator destroytext()
//	{
//		if (enemyController.isDestroyed == true) {
//			yield return new WaitForSeconds (0.5f);
//			Destroy (pointsText);
//		}
//	}

	void OnBecameInvisible ()
	{
		Destroy (gameObject);
	}
}
