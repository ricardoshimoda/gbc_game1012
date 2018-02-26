using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider c){
		if (c.tag == "Player") {
			Debug.Log ("You Win!");
		}
	}
}
