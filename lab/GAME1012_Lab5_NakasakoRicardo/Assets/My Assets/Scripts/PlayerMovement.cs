using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

	[SerializeField] int playerSpeed;
	[SerializeField] Text score;
	int playerScore = 0;

	void Start(){
		playerScore = 0;
	}
		
	// Update is called once per frame
	void Update () {
		/*
		 * Left and right
		 */
		if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
			transform.Translate (-playerSpeed * Time.deltaTime,0,0);
		}
		if(Input.GetKey(KeyCode.D)|| Input.GetKey(KeyCode.RightArrow)){
			transform.Translate (playerSpeed * Time.deltaTime,0,0);
		}

		transform.Translate (0, 0, playerSpeed * Time.deltaTime);

		/*
		 * Up and Down
		 */
		if(Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.UpArrow)){
			transform.Translate (0, playerSpeed * Time.deltaTime, 0);
		}
		if (Input.GetKey (KeyCode.S)|| Input.GetKey(KeyCode.DownArrow)) {
			transform.Translate (0, -playerSpeed * Time.deltaTime, 0);
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		
		if (other.gameObject.tag == "Coin") {
			playerScore++;
			Destroy (other.gameObject,0);
			score.text = "Score: " + playerScore.ToString ();
		}
	}
}
