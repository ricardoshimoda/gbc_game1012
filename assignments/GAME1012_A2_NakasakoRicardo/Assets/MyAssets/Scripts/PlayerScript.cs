using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
	[SerializeField] Animator anim;
	Rigidbody2D rb;
	float axisH, axisV;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		axisH = Input.GetAxis ("Horizontal");
		if (axisH < 0)
			transform.localScale = new Vector3 (1, 1, 1);
		else if (axisH > 0)
			transform.localScale = new Vector3 (-1, 1, 1);
		anim.SetFloat ("speed", Mathf.Abs (axisH));
	}
}
