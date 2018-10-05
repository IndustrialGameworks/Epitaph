using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class linearTankerParent : MonoBehaviour
{
    //Movement Variables
    public float movementSpeed = 3;
    public bool edgeBounce = false;
    bool moveDown; // currently assigns random boolean to whether enemy starts out moving up or down.
    public TankerLinearEnemyController linearController;

    void Start ()
    {
        moveDown = (Random.value > 0.5f);
    }
	
	// Update is called once per frame
	void Update ()
    {
        StartCoroutine("destroyThis");
        Movement();
    }

    void Movement()
    {
        transform.Translate(Vector2.left * movementSpeed * Time.deltaTime);

        if (edgeBounce == true)
        {

            if (transform.position.y <= -4.2f)
            {
                moveDown = false;
            }
            if (transform.position.y >= 4.2f)
            {
                moveDown = true;
            }

            if (moveDown == true)
            {
                transform.Translate(Vector2.down * movementSpeed * Time.deltaTime);
            }
            if (moveDown == false)
            {
                transform.Translate(Vector2.up * movementSpeed * Time.deltaTime);
            }
        }
    }

    IEnumerator destroyThis()
    {
        if (linearController.isDestroyed == true)
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
