using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPlacer : MonoBehaviour {

    //public bool debug = false; //uncomment if you want to debug path placement with visual representation of nav points
    public bool mirror = false;

    public float spacing = .1f;
    public float resolution = 1;
    public int pointDenominator = 0; //for naming nav points

    public List<GameObject> NavPoints;
    public List<GameObject> MirroredNavPoints;

    // Use this for initialization
    void Start()
    {
        Vector2[] points = this.GetComponent<PathCreator>().path.CalculateEvenlySpacedPoints(spacing, resolution); //gets the pathcreator of the object this script is attached to, generates from that
        foreach (Vector2 p in points)
        {
            GameObject g = new GameObject("NavPoint" + pointDenominator); //adds game objects at points
            g.transform.SetParent(this.gameObject.transform, true);

            //if (debug == true)
            //{
            //    g = GameObject.CreatePrimitive(PrimitiveType.Sphere); //for debug
            //}

            g.transform.position = p;
            g.transform.localScale = Vector3.one * spacing * .5f;
            NavPoints.Add(g); //adds navpoints to list
            pointDenominator++; //adds one to point denominator
        }

        pointDenominator = 0; //mirroring script added below

        if (mirror)
        {
            foreach (GameObject g in NavPoints) //cycles through all objects in NavPoints
            {
                GameObject fetchedObject = NavPoints[pointDenominator]; //get the relevant game object from the generated nav points
                float posX = fetchedObject.transform.position.x; //get the X value of the called game object
                float posY = fetchedObject.transform.position.y; //get the Y value of the called game object

                float adjustedY = -posY; //inverts the value of the Y axis value to create symetrical nav point in the opposite range

                GameObject newNav = new GameObject("MirroredNavPoint" + pointDenominator); //adds game objects at points
                newNav.transform.position = new Vector2(posX, adjustedY);
                newNav.transform.SetParent(this.gameObject.transform, true);

               newNav.transform.localScale = Vector3.one * spacing * .5f;
               MirroredNavPoints.Add(newNav); //adds navpoints to list
               pointDenominator++; //adds one to point denominator

                // print("nav point " + pointDenominator + " inverse: " + posX + ", " + adjustedY); //print the mirrored vectors to log

            }
        }
    }
}