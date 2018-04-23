using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoorScript : MonoBehaviour {
	[SerializeField] Animator anim;

	// Use this for initialization
	void Start () {
		anim.SetBool ("Open", true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
