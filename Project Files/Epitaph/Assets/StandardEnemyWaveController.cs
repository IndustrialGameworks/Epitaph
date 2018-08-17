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

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Movement ();
		StartCoroutine ("destroyThis");
	}

	void Movement () {
//		if (navPointsComplete1 == 0) {
//			if (enemiesInWave [0].transform.localPosition.x > navigationPoints [0].transform.localPosition.x) {
//				enemiesInWave [0].transform.Translate (-waveSpeed * Time.deltaTime, 0, 0);
//			} else {
//				navPointsComplete1 = 1;
//			}
//		} else if (navPointsComplete1 == 1) {
//			if (enemiesInWave [0].transform.localPosition.y > navigationPoints [1].transform.localPosition.y) {
//				enemiesInWave [0].transform.Translate (0, -waveSpeed * Time.deltaTime, 0);
//			} else {
//				navPointsComplete1 = 2;
//			}
//		} else if (navPointsComplete1 == 2) {
//			if (enemiesInWave [0].transform.localPosition.x < navigationPoints [2].transform.localPosition.x) {
//				enemiesInWave [0].transform.Translate (waveSpeed * Time.deltaTime, 0, 0);
//			} else {
//				navPointsComplete1 = 3;
//			}
//		} else if (navPointsComplete1 == 3) {
//			if (enemiesInWave [0].transform.localPosition.y < navigationPoints [3].transform.localPosition.y) {
//				enemiesInWave [0].transform.Translate (0, waveSpeed * Time.deltaTime, 0);
//			} else {
//				navPointsComplete1 = 4;
//			}
//		} else if (navPointsComplete1 == 4) {
//			if (enemiesInWave [0].transform.localPosition.x > navigationPoints [4].transform.localPosition.x) {
//				enemiesInWave [0].transform.Translate (-waveSpeed * Time.deltaTime, 0, 0);
//			} else {
//				navPointsComplete1 = 5;
//			}
//		}



//		if (navPointsComplete2 == 0) {
//			if (enemiesInWave [1].transform.localPosition.x > navigationPoints [0].transform.localPosition.x) {
//				enemiesInWave [1].transform.Translate (-waveSpeed * Time.deltaTime, 0, 0);
//			} else {
//				navPointsComplete2 = 1;
//			}
//		} else if (navPointsComplete2 == 1) {
//			if (enemiesInWave [1].transform.localPosition.y > navigationPoints [1].transform.localPosition.y) {
//				enemiesInWave [1].transform.Translate (0, -waveSpeed * Time.deltaTime, 0);
//			} else {
//				navPointsComplete2 = 2;
//			}
//		} else if (navPointsComplete2 == 2) {
//			if (enemiesInWave [1].transform.localPosition.x < navigationPoints [2].transform.localPosition.x) {
//				enemiesInWave [1].transform.Translate (waveSpeed * Time.deltaTime, 0, 0);
//			} else {
//				navPointsComplete2 = 3;
//			}
//		} else if (navPointsComplete2 == 3) {
//			if (enemiesInWave [1].transform.localPosition.y < navigationPoints [3].transform.localPosition.y) {
//				enemiesInWave [1].transform.Translate (0, waveSpeed * Time.deltaTime, 0);
//			} else {
//				navPointsComplete2 = 4;
//			}
//		} else if (navPointsComplete2 == 4) {
//			if (enemiesInWave [1].transform.localPosition.x > navigationPoints [4].transform.localPosition.x) {
//				enemiesInWave [1].transform.Translate (-waveSpeed * Time.deltaTime, 0, 0);
//			} else {
//				navPointsComplete2 = 5;
//			}
//		}
//
//
//
//		if (navPointsComplete3 == 0) {
//			if (enemiesInWave [2].transform.localPosition.x > navigationPoints [0].transform.localPosition.x) {
//				enemiesInWave [2].transform.Translate (-waveSpeed * Time.deltaTime, 0, 0);
//			} else {
//				navPointsComplete3 = 1;
//			}
//		} else if (navPointsComplete3 == 1) {
//			if (enemiesInWave [2].transform.localPosition.y > navigationPoints [1].transform.localPosition.y) {
//				enemiesInWave [2].transform.Translate (0, -waveSpeed * Time.deltaTime, 0);
//			} else {
//				navPointsComplete3 = 2;
//			}
//		} else if (navPointsComplete3 == 2) {
//			if (enemiesInWave [2].transform.localPosition.x < navigationPoints [2].transform.localPosition.x) {
//				enemiesInWave [2].transform.Translate (waveSpeed * Time.deltaTime, 0, 0);
//			} else {
//				navPointsComplete3 = 3;
//			}
//		} else if (navPointsComplete3 == 3) {
//			if (enemiesInWave [2].transform.localPosition.y < navigationPoints [3].transform.localPosition.y) {
//				enemiesInWave [2].transform.Translate (0, waveSpeed * Time.deltaTime, 0);
//			} else {
//				navPointsComplete3 = 4;
//			}
//		} else if (navPointsComplete3 == 4) {
//			if (enemiesInWave [2].transform.localPosition.x > navigationPoints [4].transform.localPosition.x) {
//				enemiesInWave [2].transform.Translate (-waveSpeed * Time.deltaTime, 0, 0);
//			} else {
//				navPointsComplete3 = 5;
//				Destroy (gameObject);
//			}
//		}
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
