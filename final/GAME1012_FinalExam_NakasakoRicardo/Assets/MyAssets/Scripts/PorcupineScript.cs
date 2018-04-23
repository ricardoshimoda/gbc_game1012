using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorcupineScript : MonoBehaviour {
	[SerializeField] AudioSource aud;
	[SerializeField] float invulnerableTime;
	[SerializeField] float vulnerableTime;
	Animator anim;

	Transform playerLoc = null;
	int lives = 3;

	void Start () {
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		if(player!= null)
			playerLoc =player.transform;
		aud = this.GetComponent<AudioSource> ();
		anim = this.GetComponent<Animator> ();
		StartCoroutine ("TurnInvulnerable");
	}

	// Update is called once per frame
	void Update () {
		if (playerLoc != null) {
			float dist = Vector3.Distance(playerLoc.position, this.transform.position);
			aud.volume = 1 - Mathf.Clamp (dist, 0, 10) / 10;
		}
	}

	void Snikt(){
		aud.Play();
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Bullet" && anim.GetBool("Vulnerable")) {
			lives--;
			if (lives == 0) {
				Destroy (this.gameObject);
			}
		}
	}

	IEnumerator TurnInvulnerable(){
		yield return new WaitForSeconds (vulnerableTime);
		anim.SetBool ("Vulnerable", false);
		StartCoroutine ("TurnVulnerable");
	}

	IEnumerator TurnVulnerable(){
		yield return new WaitForSeconds (invulnerableTime);
		anim.SetBool ("Vulnerable", true);
		StartCoroutine ("TurnInvulnerable");
	}
}
