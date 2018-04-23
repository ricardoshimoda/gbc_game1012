using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurballScript : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D coll) {
		Destroy (this.gameObject);
	}
}
