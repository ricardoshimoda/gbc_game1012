using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

	[SerializeField] float tankSpeed; // Tank movement speed - back and forth
	[SerializeField] float tankRotSpeed; // Tank rotation speed - yes it can make curves

	[SerializeField] float turretRotSpeed; // turrent rotation speed - targeting enemies
	[SerializeField] Rotation turretRotation; // script to rotate the turret

	[SerializeField] float barrelRotSpeed; // speed to rotate the barrel up and down
	[SerializeField] Rotation barrelRotation; // script to rotate the barrel

	[SerializeField] GameObject projectilePrefab; // Prefab to create the projectile
	[SerializeField] Transform projectileSpawn; // Where the projectile is instantiated

	// Update is called once per frame
	void Update () {
		MoveTank ();
		RotateTurret ();
		RotateBarrel ();
		FireInput ();
	}
	void RotateTurret(){
		if (Input.GetKey (KeyCode.LeftArrow)) {
			turretRotation.Rotate (-turretRotSpeed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			turretRotation.Rotate (turretRotSpeed * Time.deltaTime);
		}
	}
	void RotateBarrel () {
		if (Input.GetKey(KeyCode.UpArrow))
			barrelRotation.Rotate(-barrelRotSpeed * Time.deltaTime);
		if (Input.GetKey(KeyCode.DownArrow))
			barrelRotation.Rotate(barrelRotSpeed * Time.deltaTime);
	}

	void FireInput ()
	{
		if (Input.GetKeyDown(KeyCode.Space))
			Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation);
	} 
	void MoveTank(){
		// move tank
		if(Input.GetKey(KeyCode.W)){
			// Moves along z at speed per second
			transform.Translate (0, 0, tankSpeed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.S)) {
			transform.Translate (0, 0, -tankSpeed * Time.deltaTime);
		}

		// rotate tank
		if(Input.GetKey(KeyCode.A)){
			// Rotate Left
			transform.Rotate (0, -tankRotSpeed * Time.deltaTime,0);
		}
		if(Input.GetKey(KeyCode.D)){
			// Rotate Right
			transform.Rotate (0, tankRotSpeed * Time.deltaTime,0);
		}

	}
}
