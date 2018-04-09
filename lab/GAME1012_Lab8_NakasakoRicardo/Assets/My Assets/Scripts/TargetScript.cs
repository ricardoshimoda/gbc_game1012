using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour {
	[SerializeField] float respawn;
	[SerializeField] int points;
	bool active = true;

	void OnCollisionEnter(Collision other){
		Debug.Log ("Colided with target: " + other.gameObject.tag);
		if(other.gameObject.tag == "Bullet" && active == true)
		{
			active = false;
			PlayerData.Instance.Score += points;
			transform.localEulerAngles = new Vector3(-90,0,0);
			StartCoroutine(ResetTarget());
		}
	}
	IEnumerator ResetTarget() {
		yield return new WaitForSeconds(respawn);
		active = true;
		transform.localEulerAngles = Vector3.zero;
	} 
}
