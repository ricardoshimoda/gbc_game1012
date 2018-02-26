using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour {

	[SerializeField] Vector3 rotationAxis;
	[SerializeField] float maxRotationAngle;
	[SerializeField] float minRotationAngle;
	[SerializeField] bool clampRotation;
	float currentAngle = 0;

	// Use this for initialization
	void Start () {
		// this reset the angle to 0 - for the turret
		transform.eulerAngles = rotationAxis * currentAngle; // or Vector3.zero
	}
	
	// Update is called once per frame
	/*
	void Update () {
		
	}*/

	public void Rotate(float rotationAngle){
		float newAngle = rotationAngle + currentAngle;
		if(!clampRotation || (newAngle >= minRotationAngle && newAngle <= maxRotationAngle))
		{
			transform.Rotate (rotationAxis * rotationAngle);
			currentAngle = newAngle;
		}
	}
}
