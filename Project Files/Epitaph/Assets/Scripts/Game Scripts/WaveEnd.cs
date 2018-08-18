using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEnd : MonoBehaviour 
{

	public GameObject[] enemiesAliveInWave;
	public int numberOfEnemiesInArray;
	public int nullElements = 0;
	public float attackspeed;

	// Use this for initialization
	void Start () 
	{
		numberOfEnemiesInArray = enemiesAliveInWave.Length;
	}
	
	// Update is called once per frame
	void Update () 
	{
		checkArray ();
	}

	void checkArray ()
	{
		nullElements = 0;
		for (int i = 0; i < numberOfEnemiesInArray; i++) 
		{
			if (enemiesAliveInWave [i] == null) 
			{
				nullElements++;
				Debug.Log (nullElements.ToString ());
			}
		}
		if (nullElements == numberOfEnemiesInArray) 
		{
			StartCoroutine ("destroyThis");
		}
	}

	IEnumerator destroyThis()
	{
		yield return new WaitForSeconds (0.5f);
		Destroy (gameObject);
	}
}
