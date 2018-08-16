using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardEnemyWaveController : MonoBehaviour {

	//Arrays
	public GameObject[] enemiesInWave;
	public GameObject[] navigationPoints;
	public int navPointsComplete = 0;

	public Vector2 firstEnemyLocation;
	public Vector2 secondEnemyLocation;
	public Vector2 thirdEnemyLocation;

	//Movement
	public float waveSpeed = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Movement ();
	}

	void Movement () {
		
		firstEnemyLocation = enemiesInWave [0].transform.localPosition;
		secondEnemyLocation = enemiesInWave [1].transform.localPosition;
		thirdEnemyLocation = enemiesInWave [2].transform.localPosition;

	}

}
