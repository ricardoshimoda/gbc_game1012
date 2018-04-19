using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour {
	[SerializeField] Animator anim;
	[SerializeField] AudioSource aud;

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Player") {
			DebuggingRays (other);
			Vector2 direction = (other.transform.position - this.transform.position).normalized;
			if (!anim.GetBool ("Open") && Vector2.Angle (direction, Vector2.up) <= 40.0f) {
				anim.SetBool ("Open", true);
				aud.Play ();
			}
		}
	}
	void OnCollisionExit2D(Collision2D other){
		if (other.gameObject.tag == "Player" && anim.GetBool ("Open")) {
			/* closes the door when player no longer in the platform */
			anim.SetBool ("Open", false);
			aud.Play ();
		}
	}
	void OnCollisionStay2D(Collision2D other){
		DebuggingRays (other);
	}
	void Update(){
		Debug.DrawRay (this.transform.position, Vector2.up, Color.green);
	}

	void DebuggingRays(Collision2D other){
		Vector2 direction = (other.transform.position - this.transform.position).normalized;
		Debug.DrawRay (this.transform.position, direction, Color.red);
	}

}
