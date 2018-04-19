using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
	[SerializeField] Transform groundCheck;
	[SerializeField] Animator anim;
	[SerializeField] float moveForce;
	[SerializeField] float jumpDampening;
	[SerializeField] float jumpForce;
	[SerializeField] float superJumpForce;
	[SerializeField] AudioClip[] clips;

	AudioSource aud;
	Rigidbody2D rb;
	float axisH, axisV;
	public bool isGrounded = false;
    bool onLadder = false;
	bool isJumping = false;
	bool isChargingJump = false; 
	bool isSuperJumping = false;
	float jumpTimer = 0;
	const bool doomsday = false;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		aud = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		CheckGround ();
		DoMoveChecks ();
		SetAnimSpeed ();
		axisH = Input.GetAxis ("Horizontal");
	}
	void FixedUpdate(){
		if (isJumping) {
			//Debug.Log ("applies force to make player jump");
			// might as well jump!
			aud.Play ();
			if (!isSuperJumping) {
				rb.AddForce (new Vector2 (0, jumpForce * Time.fixedDeltaTime));
			} else {
				rb.AddForce (new Vector2 (0, superJumpForce * Time.fixedDeltaTime));
				isSuperJumping = false;
			}
			isJumping = false;
		} else if (onLadder) {
			// snakes and ladders
			rb.AddForce (new Vector2 (axisH * moveForce * Time.fixedDeltaTime, axisV * moveForce * Time.fixedDeltaTime));
		} else {
			// makes it more difficult to change horoizontal movement is the player is mid-jump
			// (realistically if the player is mid-jump shouldn't change horizontal movement)
			rb.AddForce (new Vector2 (axisH * (isGrounded?moveForce:moveForce/jumpDampening) * Time.fixedDeltaTime, 0));
		}
	}
	void OnTriggerEnter2D(Collider2D other){
		if (isJumping) {
			isJumping = false;
			anim.SetBool ("Jumping", false);
		}
		if (other.tag == "Ladder") {
            Debug.Log("Ladder activated!");
			SetLadder (true);
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Ladder") {
			SetLadder (false);
		}
	}

	void SetLadder(bool situation){
		onLadder = situation;
		anim.SetBool ("Ladder", onLadder);
	}
	void CheckGround (){
		Collider2D collider = Physics2D.OverlapCircle (groundCheck.position, 0.075f);
		isGrounded = (collider != null);
		if (onLadder)
			isGrounded = true;
		anim.SetBool ("Jumping", !isGrounded);
	}
	void DoMoveChecks (){
		axisH = Input.GetAxis ("Horizontal");
		if (onLadder) {
			rb.gravityScale = 0;
			rb.drag = 15;
			axisV = Input.GetAxis ("Vertical");
            anim.SetFloat ("Speed", Mathf.Abs (axisH + axisV));
		} else {
			rb.gravityScale = 3;
			rb.drag = 5;
			/* changes the player orientation - where he is looking at */
			if (axisH < 0) transform.localScale = new Vector3 (1, 1, 1);
			else if (axisH > 0) transform.localScale = new Vector3 (-1, 1, 1);

			/* makes the player charge jump */
			if (Input.GetKeyDown (KeyCode.Space) && isGrounded && !isChargingJump) {
				//Debug.Log ("Charging Jumping");
				isChargingJump = true;
				StartCoroutine ("SuperJumpTimer");
			}
			/* makes the player really jump */
			if (Input.GetKeyUp (KeyCode.Space) && isGrounded) {
				//Debug.Log ("Probably really jumping");
				isJumping = true;
				isChargingJump = false;
				StopCoroutine ("SuperJumpTimer");
				if (jumpTimer > 1.5f) 
					isSuperJumping = true;
				jumpTimer = 0;
			}
			anim.SetFloat ("Speed", Mathf.Abs (axisH));
		}
	}
	void SetAnimSpeed (){
		  if (anim.GetCurrentAnimatorStateInfo (0).IsName ("mm_climb")) {
			anim.speed = Mathf.Abs (axisH )+ Mathf.Abs(axisV);
		} else if (anim.GetCurrentAnimatorStateInfo (0).IsName ("mm_run")) {
			anim.speed = Mathf.Abs (axisH);
		} else {
			anim.speed = 1;
		}
	}
	IEnumerator SuperJumpTimer(){
		while (!doomsday) {
			jumpTimer++;
			yield return new WaitForSeconds (1.0f);
		}
	}
}
