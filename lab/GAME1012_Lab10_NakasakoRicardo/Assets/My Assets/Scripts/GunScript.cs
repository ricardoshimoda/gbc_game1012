using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour {
	[SerializeField] GameObject bulletPrefab;
	[SerializeField] LineRenderer laser;
	[SerializeField] Transform bulletSpawn;
	[SerializeField] Transform laserSpawn;
	[SerializeField] float bulletForce;
	[SerializeField] float respawnBullet;
	[SerializeField] float flashAhhhh;
	[SerializeField] GameObject muzzleFlash;

	AudioSource pewPewPew;
	bool canFire = true;

	// Use this for initialization
	void Start () {
		laser.enabled = false;
		pewPewPew = this.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0)) {
			Debug.Log ("pew pew pew");
			if (canFire) {
				GameObject bulletInst = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation) as GameObject;
				Vector3 fwd = bulletSpawn.TransformDirection(Vector3.forward);
				bulletInst.GetComponent<Rigidbody>().AddForce(fwd*bulletForce*Time.deltaTime);
				pewPewPew.Play ();
				canFire = false;
				muzzleFlash.SetActive (true);
				StartCoroutine(ResetGun());
				StartCoroutine(FlashAhhh());
			}
		}
		if (Input.GetMouseButtonDown (1)) {
			laser.enabled = true;
			laser.SetPosition(0, laserSpawn.position);
			laser.SetPosition(1, laserSpawn.position+laserSpawn.forward*50);
			RaycastHit hit;
			Vector3 fwd = laserSpawn.TransformDirection(Vector3.forward);
			Debug.DrawRay(laserSpawn.position, fwd*20, Color.green);
			if (Physics.Raycast(laserSpawn.position, fwd, out hit))
				laser.SetPosition(1, hit.point);
		}
		if (Input.GetMouseButtonUp (1)) {
			laser.enabled = false; 
		}
	}

	IEnumerator ResetGun() {
		yield return new WaitForSeconds(respawnBullet);
		canFire = true;
	} 

	IEnumerator FlashAhhh(){
		yield return new WaitForSeconds(flashAhhhh);
		muzzleFlash.SetActive (false);
	}

}
