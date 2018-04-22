using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPowerupScript : MonoBehaviour {

	[SerializeField] AudioSource aud;
	public bool used = false;

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player" && !used) {
			used = true;
			PlayerData.Instance.MaxAmmo();
			aud.Play();
			Destroy (this.gameObject, 0.2f);
		}
	}
}
