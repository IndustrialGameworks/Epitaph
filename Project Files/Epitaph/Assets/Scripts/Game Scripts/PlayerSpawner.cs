using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script controls the player spawner. on start this script will spawn a specific type of player controller depending on which player you selected in the options menu.

public class PlayerSpawner : MonoBehaviour 
{

	public GameObject [] playerControllers;//array of the player controllers.

	// Use this for initialization
	void Start () 
	{
		//Debug.Log ((PlayerPrefs.GetInt ("playerType").ToString ()));
		if (PlayerPrefs.GetInt ("playerType") == 1)//checks the playerType player pref and spawns a specific player controller depending on the int.
        {
			var player = Instantiate (playerControllers [0], gameObject.transform.position, Quaternion.identity);
			player.transform.SetParent (gameObject.transform);
		}
        else if (PlayerPrefs.GetInt ("playerType") == 2)
        {
			var player = Instantiate (playerControllers [1], gameObject.transform.position, Quaternion.identity);
			player.transform.SetParent (gameObject.transform);
		}
        else if (PlayerPrefs.GetInt ("playerType") == 3)
        {
			var player = Instantiate (playerControllers [2], gameObject.transform.position, Quaternion.identity);
			player.transform.SetParent (gameObject.transform);
		}
        else if (PlayerPrefs.GetInt ("playerType") == 4)
        {
			var player = Instantiate (playerControllers [3], gameObject.transform.position, Quaternion.identity);
			player.transform.SetParent (gameObject.transform);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
