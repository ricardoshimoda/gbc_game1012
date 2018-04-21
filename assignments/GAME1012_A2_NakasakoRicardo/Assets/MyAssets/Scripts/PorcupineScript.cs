using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorcupineScript : MonoBehaviour {
	[SerializeField] AudioSource aud;
	Animator anim;

	Transform playerLoc;

	void Start () {
		aud = this.GetComponent<AudioSource> ();
		anim = this.GetComponent<Animator> ();
		playerLoc = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		float dist = Vector3.Distance(playerLoc.position, this.transform.position);
		aud.volume = 1 - Mathf.Clamp (dist, 0, 10) / 10;
		Debug.Log (dist);
		anim.SetFloat ("PlayerDistance",dist);
	}

	void Snikt(){
		aud.Play();
	}
}
