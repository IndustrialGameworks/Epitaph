using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour {

    float linearMovementSpeed = 2;
    public Turret thisTurret;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Movement();
        StartCoroutine("destroyThis");
    }

    void Movement()
    {
        transform.Translate(Vector2.left * linearMovementSpeed * Time.deltaTime);
    }

    IEnumerator destroyThis()
    {
        if (thisTurret.isDestroyed == true)
        {
            yield return new WaitForSeconds(0.5f);
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
