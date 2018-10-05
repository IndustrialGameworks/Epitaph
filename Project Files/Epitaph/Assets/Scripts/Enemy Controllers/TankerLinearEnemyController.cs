using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankerLinearEnemyController : MonoBehaviour {

	public int health = 2000;

    //text variables
    public GameObject pointsText;
    TextMesh theText;
    public bool isDestroyed = false;
    public GameObject linearParent;

    //color variables
    Color hit = new Color(145f / 255f, 50f / 255f, 50f / 255f, 1);
    Color standard = Color.white;
    SpriteRenderer linearSprite;
    public GameObject deathParticle;

    // Use this for initialization
    void Start ()
    {
        linearSprite = GetComponent<SpriteRenderer>();//gets the sprite renderer for this gameobject.
        theText = pointsText.GetComponent<TextMesh>();//gets the text mesh of this text object
	}
	
	// Update is called once per frame
	void Update ()
    {
		Status ();
	}

	void Status () {
		if (health <= 0)
        {

            GameController.gameScore += (50 * GameController.multiplier);//returns score to the game controller.
            theText.text = "+" + (50 * GameController.multiplier);//edits the text of this textmesh.
            pointsText.transform.SetParent(linearParent.transform);
            isDestroyed = true;
            GameController.multiplier += 1;//adds 1 to the games multiplier and also resets the timer before the multiplier degrades.
            GameController.timer = GameController.resetTimer;
            if (deathParticle != null)
            {
                Instantiate(deathParticle, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);//destroy this gameobject.
        }
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "PlayerProjectile") {
			health -= 50;
			Destroy (other.gameObject);
            StartCoroutine("changeColor");//run this co routine.
        }
	}

    IEnumerator changeColor()//changes the color of the sprite renderer on hit.
    {
        linearSprite.color = hit;//change to red.
        yield return new WaitForSeconds(0.125f);//wait this long.
        linearSprite.color = standard;//return to normal.
    }

    void OnBecameInvisible ()
	{
		Destroy (gameObject);
	}
}
