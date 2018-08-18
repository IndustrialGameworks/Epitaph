using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdTierWave : MonoBehaviour {

	public GameObject[] enemiesInWave;
	public GameObject[] navigationPoints;
	public int navPointsComplete1 = 0;
	public int navPointsComplete2 = 0;
	public int navPointsComplete3 = 0;

	public Vector2 navLocation1;
	public Vector2 navLocation2;
	public Vector2 navLocation3;

	public ThirdTierEnemyController enemyController3;

	public float waveSpeed;
	public GameObject pointsText;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		StartCoroutine ("destroyThis");
	}

	IEnumerator destroyThis ()
	{
		if (enemyController3.isDestroyed == true) 
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

