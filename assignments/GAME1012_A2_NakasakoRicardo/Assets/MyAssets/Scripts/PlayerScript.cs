using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	public static bool exists = false;

	[SerializeField] Transform groundCheck;
	[SerializeField] Animator anim;
	[SerializeField] float moveForce;
	[SerializeField] float jumpDampening;
	[SerializeField] float jumpForce;
	[SerializeField] float superJumpForce;
	[SerializeField] AudioClip[] clips;
	[SerializeField] float bulletSpeed;
	[SerializeField] GameObject bulletPrefab;
	[SerializeField] float respawnBullet;
	[SerializeField] Transform bulletSpawn;
	[SerializeField] float bulletForce;
	[SerializeField] float invulnerability;

	AudioSource aud;
	Rigidbody2D rb;
	float axisH, axisV;
	float shooting; 
	public bool isGrounded = false;
    bool onLadder = false;
	bool isJumping = false;
	bool isChargingJump = false; 
	bool isSuperJumping = false;
	float jumpTimer = 0;
	const bool doomsday = false;
	bool canFire = true;
	bool isInvulnerable = false;
	bool isAlive = true;

	void Start () {
		/*
		 * The following code deals with level transition between two different scenes 
		 */
		if (!exists) {
			PlayerData.Instance.MaxAmmo ();
			PlayerData.Instance.MaxLives ();
			rb = GetComponent<Rigidbody2D> ();
			aud = GetComponent<AudioSource> ();
			exists = true;
			DontDestroyOnLoad (this.gameObject);
		} else {
			GameObject.Destroy (this.gameObject);
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (!isAlive)
			return;
		CheckGround ();
		DoMoveChecks ();
		PewPewPew ();
		SetAnimSpeed ();
		axisH = Input.GetAxis ("Horizontal");
	}
	void FixedUpdate(){
		if (isJumping) {
			//Debug.Log ("applies force to make player jump");
			// might as well jump!
			aud.clip=clips[0];
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
	void Hurt(){
		PlayerData.Instance.Lives--;
		if (PlayerData.Instance.Lives == 0) {
			isAlive = false;
			anim.SetBool ("Death", true);
			aud.clip = clips [2];
			aud.Play ();
		} else {
			isInvulnerable = true;
			StartCoroutine (ResetImmortality ());
		}
	}
	void Instadeath(){
		PlayerData.Instance.Lives = 0;
		isAlive = false;
		anim.SetBool ("Death", true);
		aud.clip = clips [2];
		aud.Play ();
	}
	void OceanicAirines(){
		PlayerData.Instance.Lives = 0;
		isAlive = false;
		aud.clip = clips [2];
		aud.Play ();
		Destroy (this.gameObject,0.2f);
	}
	void OnCollisionStay2D(Collision2D other){
		if (other.gameObject.tag == "Enemy" && !isInvulnerable) {
			Hurt ();
		}
	}
	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "Spikes" && isAlive) {
			Instadeath();
		}
		if (other.tag == "Ether" && isAlive) {
			OceanicAirines ();
		}
	}
	void OnTriggerEnter2D(Collider2D other){
		if (isJumping) {
			isJumping = false;
			anim.SetBool ("Jumping", false);
		}
		if (other.tag == "Ladder") {
			SetLadder (true);
		}
	}

	IEnumerator ResetImmortality(){
		yield return new WaitForSeconds(invulnerability);
		isInvulnerable = false;
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
			// when on ladder cannot jump or shoot
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
				if (jumpTimer >= 2f) 
					isSuperJumping = true;
				jumpTimer = 0;
			}
			anim.SetFloat ("Speed", Mathf.Abs (axisH));
		}
	}
	void PewPewPew(){
		if (Input.GetAxis ("Fire1") == 1 && canFire && PlayerData.Instance.Ammo > 0) {
			GameObject bulletInst = Instantiate (bulletPrefab, bulletSpawn.position, bulletSpawn.rotation) as GameObject;
			bulletInst.GetComponent<Rigidbody2D> ().AddForce (Vector2.left * transform.localScale.x * bulletForce * Time.deltaTime);
			aud.clip = clips [1];
			aud.Play ();
			canFire = false;
			StartCoroutine ("ResetGun");
			anim.SetBool ("Shooting", true);
			PlayerData.Instance.Ammo--;
		} else {
			anim.SetBool ("Shooting", false);
		}
	}
	IEnumerator ResetGun(){
		yield return new WaitForSeconds(respawnBullet);
		canFire = true;
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
