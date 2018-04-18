using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TargetScript : MonoBehaviour {
	[SerializeField] GameObject hitParticle;
	[SerializeField] float respawn;
	[SerializeField] int points;
	Animator targetUpAndDown;
	bool active = true;
	AudioSource ping;
	float targetSpeed = 0.05f;
	Vector3 initialPosition;
	bool direction;

	void Start(){
		targetUpAndDown = GetComponent<Animator>();
		ping = this.GetComponent<AudioSource> ();
		Debug.Log ("Getting the initial position");
		initialPosition = this.transform.position;
	}

	void OnCollisionEnter(Collision other){
		Debug.Log ("Colided with target: " + other.gameObject.tag);
		if(other.gameObject.tag == "Bullet" && active == true)
		{
			active = false;
			PlayerData.Instance.Score += points;
			targetUpAndDown.Play ("TargetDown");
			ping.Play ();
			StartCoroutine(ResetTarget());
			Instantiate (hitParticle, other.contacts[0].point, Quaternion.identity);
		}
	}
	IEnumerator ResetTarget() {
		yield return new WaitForSeconds(respawn);
		active = true;
		targetUpAndDown.Play ("TargetUp", -1);
	}
	void Update(){
		if (active) {
			if (direction) {
				this.transform.Translate (-targetSpeed, 0, 0);
			} else {
				this.transform.Translate (targetSpeed, 0, 0);
			}
			if (Vector3.Distance(this.transform.position, this.initialPosition) > 1.3) {
				direction = !direction;
				/* just to avoid going back and forth without stop */
				if (direction) {
					this.transform.Translate (-targetSpeed, 0, 0);
				} else {
					this.transform.Translate (targetSpeed, 0, 0);
				}
			}
		}
	}
}
