using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollow : MonoBehaviour {

    public Vector2[] navLocations;
    public Vector2[] mirroredNavLocations;

    public bool moveToMirrored = false;

    public int speed = 10;
    public int waitTime;

    int currentNavDenominator = 0; 

    bool initiated;
    bool canMove;
	// Use this for initialization
	void Start ()
    {
        StartCoroutine("waitToMove");
	}

	// Update is called once per frame
	void Update ()
    {
        Initiate(); //has to be run here, as the nav points dont exist yet on start!
        if (!moveToMirrored && canMove)
        {
            MoveToNav();
        }
        if (moveToMirrored && canMove)
        {
            MoveToMirroredNav();
        }
    }

    void Initiate()
    {
        int denominator = 0; //local denominator value

        if (initiated == false) //checks to see if has been initiated
        {

            var navList = GetComponentInParent<PathPlacer>().NavPoints; //grab the nav point location list out of path placer
            var mirroredNavList = GetComponentInParent<PathPlacer>().MirroredNavPoints;

            foreach (GameObject g in navList) //for each item in path placer increase the denominator, so you can get length of its generated list
            {
                denominator++;
            }

            navLocations = new Vector2[denominator]; //initiate the array of vectors that we will use to store the nav point locations
            mirroredNavLocations = new Vector2[denominator];

            denominator = 0; //reset denominator to 0 so we can iterate it through the list again

            foreach (GameObject g in navList) //for all the generated nav points in path placer, grab their vector, and assign it to the relevant array index
            {
                Vector2 location = g.transform.position;
                navLocations[denominator] = location;
                denominator++;
            }

            denominator = 0; //resets for the mirrored nav

            foreach (GameObject g in mirroredNavList) 
            {
                Vector2 location = g.transform.position;
                mirroredNavLocations[denominator] = location;
                denominator++;
            }

            initiated = true; //set to true, so cannot be initiated again
        }
    }

    void MoveToNav() //moves the nav points in specific order
    { 
        Vector2 currentLocation = transform.position; //the current vector of the object
        Vector2 targetLocation = navLocations[currentNavDenominator]; //the current target vector

        if (currentLocation != targetLocation) //if the current location is not equal to the target location move towards target location
        {
            transform.position = (Vector2.MoveTowards(new Vector2(currentLocation.x, currentLocation.y), targetLocation, speed * Time.deltaTime));
        }

        else //if (currentLocation == targetLocation) //when reached the target location, increment the denominator, so that the target location calls the next index in the array
        {
            if (currentNavDenominator < navLocations.Length - 1) //make sure you can't go out of array bounds with next increment
            {
                currentNavDenominator++;
            }
        }
    }

    void MoveToMirroredNav() //moves the nav points in specific order using the mirrored locations
    {
        Vector2 currentLocation = transform.position; //the current vector of the object
        Vector2 targetLocation = mirroredNavLocations[currentNavDenominator]; //the current target vector from the mirrored locations array

        if (currentLocation != targetLocation) //if the current location is not equal to the target location move towards target location
        {
            transform.position = (Vector2.MoveTowards(new Vector2(currentLocation.x, currentLocation.y), targetLocation, speed * Time.deltaTime));
        }

        else //if (currentLocation == targetLocation) //when reached the target location, increment the denominator, so that the target location calls the next index in the array
        {
            if (currentNavDenominator < navLocations.Length - 1) //make sure you can't go out of array bounds with next increment
            {
                currentNavDenominator++;
            }
        }
    }

    IEnumerator waitToMove()
    {
        yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(waitTime);
        canMove = true;
    }
}
