using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletKill : MonoBehaviour {
	void OnCollisionEnter(){
		Destroy (this.gameObject);
	}
}
