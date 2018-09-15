using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coreController : MonoBehaviour {

	public int health = 1000;

	public GameObject pointsText;
	TextMesh theText;
	public bool isDestroyed;
	public GameObject coreParent;

	//color variables
	Color hit = new Color (145f/255f, 50f/255f, 50f/255f, 1);
	Color standard = Color.white;
	SpriteRenderer coreSprite;

	// Use this for initialization
	void Start () {
		coreSprite = GetComponent<SpriteRenderer> ();
		theText = pointsText.GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		Status ();
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "PlayerProjectile") {
			health -= 50;
			Destroy (other.gameObject);
			StartCoroutine ("changeColor");
		}
	}

	IEnumerator changeColor ()
	{
		coreSprite.color = hit;
		yield return new WaitForSeconds (0.125f);
		coreSprite.color = standard;
	}

	void Status () {
		if (health <= 0) {

			GameController.gameScore += (500 * GameController.multiplier);
			theText.text = "+" + (500 * GameController.multiplier);
			isDestroyed = true;
			GameController.multiplier += 1;
			GameController.timer = GameController.resetTimer;
			Destroy (gameObject);
		}
	}
}
