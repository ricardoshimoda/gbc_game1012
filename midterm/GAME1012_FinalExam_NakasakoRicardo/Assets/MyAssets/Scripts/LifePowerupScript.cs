using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePowerupScript : MonoBehaviour {
	[SerializeField] AudioSource aud;
	public bool used = false;

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player" && !used) {
			used = true;
			PlayerData.Instance.MaxLives ();
			aud.Play();
			Destroy (this.gameObject, 0.2f);
		}
	}
}
