using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	[SerializeField] float projectileSpeed;
	[SerializeField] float disappearTime; 

	// Use this for initialization
	void Start () {
		Destroy (gameObject, disappearTime);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (0, 0, projectileSpeed * Time.deltaTime);
	}
}
