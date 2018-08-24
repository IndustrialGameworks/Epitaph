using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Use this script to set locations to move the "center of mass" and control where projectiles are repelled from. Just add nav points in the inspector!
//Must be attached to center of mass!


public class FiringPatternControl : MonoBehaviour 
{

	public Vector2[] navPoints;
	public Vector2 currentLocation;
	public bool movingToNav = true;
	public float speed = 2;
	public int count = 0;

	//calls the boss controller script
	public PrimerBossController controller;

	//nav variables
	float nav1Y;
	float nav2Y;
	float nav3Y;
	float nav3X;
	float nav4X;


	// Use this for initialization
	void Start () 
	{
		nav1Y = navPoints [0].y;
		nav2Y = navPoints [1].y;
		nav3Y = navPoints [2].y;
		nav3X = navPoints [2].x;
		nav4X = navPoints [3].x;
	}
	
	// Update is called once per frame
	void Update () 
	{
		currentLocation = new Vector2 (transform.localPosition.x, transform.localPosition.y);
		if (controller.secondStage == true) 
		{
			movement ();
		}
	}

	void movement ()
	{
		if (count < 6 && movingToNav == true) 
		{
			transform.localPosition = (Vector2.MoveTowards (new Vector2 (transform.localPosition.x, transform.localPosition.y), navPoints [0], speed * Time.deltaTime)); //moves to navPoint
			if (currentLocation.y == nav1Y) 
			{
				count++;
				movingToNav = false;
			}
		} 
		else if (count < 6 && movingToNav == false) 
		{
			transform.localPosition = (Vector2.MoveTowards (new Vector2 (transform.localPosition.x, transform.localPosition.y), navPoints [1], speed * Time.deltaTime)); //moves to navPoint
			if (currentLocation.y == nav2Y) 
			{
				count++;
				movingToNav = true;
			}
		} 
		else if (count == 6) 
		{
			transform.localPosition = (Vector2.MoveTowards (new Vector2 (transform.localPosition.x, transform.localPosition.y), navPoints [2], speed * Time.deltaTime)); //moves to navPoint
			if (currentLocation.y == nav3Y) 
			{
				count = 7;
			}
		} 
		else if (count == 7) 
		{
			transform.localPosition = (Vector2.MoveTowards (new Vector2 (transform.localPosition.x, transform.localPosition.y), navPoints [3], speed * Time.deltaTime)); //moves to navPoint
			if (currentLocation.x == nav4X) 
			{
				count = 8;
			}
		} 
		else if (count == 8) 
		{
			transform.localPosition = (Vector2.MoveTowards (new Vector2 (transform.localPosition.x, transform.localPosition.y), navPoints [2], speed * Time.deltaTime)); //moves to navPoint
			if (currentLocation.x == nav3X)
			{
				count = 0;
			}
		}
	}
}
