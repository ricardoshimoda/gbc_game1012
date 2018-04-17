using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour {
	[SerializeField] GameObject hitParticle;
	[SerializeField] float respawn;
	[SerializeField] int points;
	Animator targetUpAndDown;
	bool active = true;
	AudioSource ping;

	void Start(){
		targetUpAndDown = GetComponent<Animator>();
		ping = this.GetComponent<AudioSource> ();
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
}
