using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchControlScript : MonoBehaviour 
{
	//bool to check if dragging or not.
	bool dragging = false;

	//floats for the controllers x and y co ordinates and for the screen boundaries.
	public float controllerX;
	public float controllerY;
	private float minX, maxX, minY, maxY;

	//vectors for screen boundaries.
	Vector2 bottomCorner;
	Vector2 topCorner;

    //reference to player.
    public PlayerController Player;

	// Use this for initialization
	void Start () 
	{
		//sets the top and bottom corner vector to equal loaction of top and bottom corners.
		bottomCorner = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
		topCorner = Camera.main.ViewportToWorldPoint(new Vector2(1,1));
        setBounds();
        //sets the default x position of this object to be the minimum x position + 1.
        Vector2 startPosition = new Vector2(minX + 1, transform.position.y);
        transform.position = startPosition;
	}
	
	// Update is called once per frame
	void Update () 
	{
		ControllerPosition (); 

		//check if touchcount > 0 so if its been touched.
		if (Input.touchCount > 0) 
		{
			//gets the input of the touch we want to use, the first touch.
			Touch touch = Input.GetTouch (0);
			//gets the co ordinates of the touch and stores in a vector 2.
			Vector2 touchPos = Camera.main.ScreenToWorldPoint (touch.position);

			switch (touch.phase)
			{
			case TouchPhase.Began://on touch.
				if (GetComponent<Collider2D> () == Physics2D.OverlapPoint (touchPos)) //check if the touch overlaps the collider of the object this script is attached too.
				{
					dragging = true;//set dragging to true so the moved phase can begin.
				}
				break;

			case TouchPhase.Moved://on dragged.
				if (GetComponent<Collider2D> () == Physics2D.OverlapPoint (touchPos) && dragging == true)//check if the touch overlaps the collider of the object this script is attached too and if dragging is set to true.
				{
					Vector2 currentPosition = Camera.main.ScreenToWorldPoint (touch.position);//gets the current position of the finger and store in vector 2.

					transform.position = currentPosition;//moves the position of this gameobject to equal the position of the finger.
				}
				break;	

			case TouchPhase.Ended://on lifted finger.
				dragging = false; //set dragging to false.
                boundaries();
                break;
			}
		}
	}

	void setBounds ()
	{
		//takes the minimum and maximum x and y values from the top and bottom corner vectors.
		minX = bottomCorner.x;
		maxX = topCorner.x;
		minY = bottomCorner.y;
		maxY = topCorner.y;
	}

	void boundaries ()
	{
		//if the player goes outside the minimum or maximum x or y move them back in.
		if (transform.position.x <= minX + 0.5) 
		{
			controllerX = minX + 0.501f;
			transform.position = new Vector2 (controllerX, controllerY);
		} 
		else if (transform.position.x >= maxX - 2) 
		{
			controllerX = maxX - 2.001f;
			transform.position = new Vector2 (controllerX, controllerY);
		} 
		else if (transform.position.y <= minY + 0.5) 
		{
			controllerY = minY + 0.501f;
			transform.position = new Vector2 (controllerX, controllerY);
		} 
		else if (transform.position.y >= maxY - 0.5) 
		{
			controllerY = maxY - 0.501f;
			transform.position = new Vector2 (controllerX, controllerY);
		}
	}

	void ControllerPosition () 
	{ 
		//gets the x and y of the controllers position
		controllerX = transform.position.x;
		controllerY = transform.position.y;
	}

    void OnTriggerEnter2D(Collider2D other)//if a pickup collides with the touch area.
    {
        if (other.tag == "PickupDualBlast")//if the pickup is a dual blast.
        {
            Player.doubleEmitter = true;//enables / disables certain emmiters.
            Player.singleEmitter = false;
            Player.tripleEmitter = false;

            Player.pickupLength = 5;
            Player.StopCoroutine("DualAttack");//stops the dual attack and timer if they pick up two in a row.
            Player.StopCoroutine("PickupTimer");
            Player.StartCoroutine("DualAttack"); //starts a coroutine running for firing projectiles.
            Player.StartCoroutine("PickupTimer");//starts coroutine to time the pickup.
            Player.StopCoroutine("Attack");//stops attack and triple attack coroutines.
            Player.StopCoroutine("TripleAttack");
        }
        if (other.tag == "PickupTripleBlast")//if the pickup is a tri blast.
        {
            Player.tripleEmitter = true;//enables / disables certain emmiters.
            Player.doubleEmitter = false;
            Player.singleEmitter = false;

            Player.pickupLength = 5;
            Player.StopCoroutine("TripleAttack");//stops the tri attack and timer if they pick up two in a row.
            Player.StopCoroutine("PickupTimer");
            Player.StartCoroutine("TripleAttack"); //starts a coroutine running for firing projectiles
            Player.StartCoroutine("PickupTimer");//starts coroutine to time the pickup.
            Player.StopCoroutine("Attack");//stops attack and dual attack coroutines.
            Player.StopCoroutine("DualAttack");
        }
    }
}
