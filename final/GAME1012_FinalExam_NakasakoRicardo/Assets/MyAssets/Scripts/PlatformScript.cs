using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour {
	[SerializeField] Transform startPos;
	[SerializeField] Transform endPos;
	[SerializeField] float speed;

	// Update is called once per frame
	void Update () {
		float weight = Mathf.Cos (Time.time * speed) * 0.5f + 0.5f;
		transform.position = Vector3.Lerp (endPos.position, startPos.position, weight);
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Player") {
			other.transform.parent = this.transform;
		}
	}

	void OnCollisionExit2D(Collision2D other){
		if (other.gameObject.tag == "Player") {
			other.transform.parent = null;
		}
	}

}
