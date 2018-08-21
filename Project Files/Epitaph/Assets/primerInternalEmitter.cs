using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class primerInternalEmitter : MonoBehaviour {

	public Vector2 centerOfMass;
	public GameObject origin;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		centerOfMass = origin.transform.position; //updates the center of mass
	}
}
