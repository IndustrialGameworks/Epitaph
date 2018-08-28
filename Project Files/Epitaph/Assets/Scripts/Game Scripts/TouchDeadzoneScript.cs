using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDeadzoneScript : MonoBehaviour
{
    //floats.
    private float minX;

    //vectors for screen boundaries.
    Vector2 bottomCorner;
    Vector2 topCorner;

    //gets the sprite renderer.
    SpriteRenderer deadzoneSprite;

    //gets the player touch controller.
    public GameObject playerTouchController;

    // Use this for initialization
    void Start ()
    {
        bottomCorner = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        setBounds();
        Vector2 startPosition = new Vector2(minX + 0.35f, transform.position.y);
        transform.position = startPosition;
        deadzoneSprite = GetComponent<SpriteRenderer>();
        deadzoneSprite.enabled = false;
        playerTouchController = GameObject.FindGameObjectWithTag("PlayerTouchController");//gets the gameobject with tag playertouchcontroller.
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (playerTouchController != null)
        {
            if (playerTouchController.transform.position.x <= minX + 1)//if the players x position is less then the minX + 1 enable this gameobjects sprite renderer.
            {
                deadzoneSprite.enabled = true;
            }
            else//disable this gameobjects sprite renderer.
            {
                deadzoneSprite.enabled = false;
            }
        }
        else
        {
            deadzoneSprite.enabled = false;
        }
    }

    void setBounds()
    {
        //takes the minimum and maximum x and y values from the top and bottom corner vectors.
        minX = bottomCorner.x;
    }
}
