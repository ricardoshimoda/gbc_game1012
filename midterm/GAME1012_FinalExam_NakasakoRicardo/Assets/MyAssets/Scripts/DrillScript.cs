using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillScript : MonoBehaviour {
	[SerializeField] AudioSource aud;
	[SerializeField] AudioClip[] clips;

	Transform playerLoc=null;

	// Use this for initialization
	void Start () {
		aud = this.GetComponent<AudioSource> ();
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		if(player != null)
			playerLoc = player.transform;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (playerLoc != null) {
			float dist = Vector3.Distance (playerLoc.position, this.transform.position);
			aud.volume = 1 - Mathf.Clamp (dist, 0, 10) / 10;
		}
	}

	void SetDown(int i){
		/*
		aud.clip = clips [i];
		aud.Play();*/
	}
}
