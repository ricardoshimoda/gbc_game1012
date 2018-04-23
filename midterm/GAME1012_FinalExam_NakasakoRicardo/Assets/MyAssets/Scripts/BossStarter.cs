using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStarter : MonoBehaviour {
	[SerializeField] Animator animStartDoor;
	[SerializeField] Animator animFinishDoor;
	[SerializeField] Animator garfield;

	void Update(){
		if (garfield == null) {
			animFinishDoor.SetBool ("Open", true);
		}
	}
	void OnTriggerEnter2D(Collider2D other){
		Debug.Log (other.tag);
		if (other.tag == "Player" && animStartDoor.GetBool("Open")) {
			animStartDoor.SetBool ("Open", false);
			garfield.SetBool ("PlayerArrived", true);
		}
	}

}
