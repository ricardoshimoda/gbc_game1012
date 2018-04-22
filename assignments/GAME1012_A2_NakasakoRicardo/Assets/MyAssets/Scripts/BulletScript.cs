using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
	[SerializeField] Animator anim;
	[SerializeField] AudioSource aud;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D coll) {
		// triggers explosion
		anim.SetBool("Hit", true);
		aud.Play ();
	}

	void AfterExplosion(){
		Destroy (this.gameObject);
	}
}
