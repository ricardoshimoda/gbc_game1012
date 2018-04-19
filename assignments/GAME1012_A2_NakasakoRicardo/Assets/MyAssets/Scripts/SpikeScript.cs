using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour {
	[SerializeField] AudioSource aud;
	[SerializeField] AudioClip[] clips;

	Transform playerLoc;

	// Use this for initialization
	void Start () {
		aud = this.GetComponent<AudioSource> ();
		playerLoc = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		float dist = Vector3.Distance(playerLoc.position, this.transform.position);
		aud.volume = 1 - Mathf.Clamp(dist, 0, 10)/10;
	}

	void SetDown(int i){
		aud.clip = clips [i];
		aud.Play();
	}
}
