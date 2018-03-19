using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

	[SerializeField] float rotation;
	[SerializeField] Renderer flame;
	Rigidbody2D rb;
	bool thrust = false;

	[SerializeField] int speed;

	[SerializeField] Text lives;
	[SerializeField] int playerLives;

	[SerializeField] Text score;
	int playerScore = 0;

	void Start(){
		playerScore = 0;
		score.text = "Score: " + playerScore.ToString ();
		lives.text = "Lives: " + playerLives.ToString ();
		rb = GetComponent<Rigidbody2D>();
		flame.enabled = false;
	}

	void FixedUpdate() {
		if (thrust == true)
		{
			Vector2 worldDir = transform.TransformDirection(Vector2.up);
			rb.AddForce(worldDir*speed*Time.fixedDeltaTime);
		}
	}
		
	// Update is called once per frame
	void Update () {
		transform.Rotate(0,0,Input.GetAxis("Horizontal")*rotation*Time.deltaTime);
		if (Input.GetKey(KeyCode.Space)) {
			ToggleFlame(true);
		}
		if (Input.GetKeyUp(KeyCode.Space)) {
			ToggleFlame(false);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Coin") {
			playerScore++;
			Destroy (other.gameObject,0);
			score.text = "Score: " + playerScore.ToString ();
		}
		if (other.gameObject.tag == "Portal") {
			score.text = "Next Level";
		}

	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Wall") {
			playerLives--;
			lives.text = "Lives: " + playerLives.ToString ();
			if (playerLives == 0) {
				score.text = "You are dead";
				Destroy (gameObject,0);
			}
		}
	}

	void ToggleFlame(bool t)
	{
		thrust = t;
		flame.enabled = t;
	}

}
