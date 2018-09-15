using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour {

    float linearMovementSpeed = 2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Movement();
	}

    void Movement()
    {
        transform.Translate(Vector2.left * linearMovementSpeed * Time.deltaTime);
    }

}
