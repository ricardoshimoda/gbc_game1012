using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalFinalBossScript : MonoBehaviour {
	[SerializeField] Transform startPosition;
	[SerializeField] Transform endPosition;
	[SerializeField] float speed;
	[SerializeField] Transform fireSpawn;
	[SerializeField] GameObject fbullet;
	[SerializeField] AudioSource aud;
	[SerializeField] Animator anim;
	[SerializeField] GameObject fireHead;

	bool showStart = false;
	bool vulnerable = false;

	const int MAX_LIVES = 30;
	int fbulletCounter = 0;
	int lives = MAX_LIVES;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (showStart = true) {
			float weight = Mathf.Cos (Time.time * speed) * 0.5f + 0.5f;
			transform.position = Vector3.Lerp (endPosition.position, startPosition.position, weight);
		}
	}

	void SpitFire(){
		aud.Play ();
		GameObject bulletInst = Instantiate (fbullet, fireSpawn.position, fireSpawn.rotation) as GameObject;
		anim.SetBool ("Shoot", false);
		StartCoroutine (ShootAgain ());
	}

	IEnumerator ShootAgain(){
		yield return new WaitForSeconds(3);
		anim.SetBool ("Shoot", true);
	}

	public void FBulletHit (){
		fbulletCounter++;
		if (fbulletCounter % 3 == 0) {
			vulnerable = true;
			StartCoroutine (InvulnerableAgain ());
		}

	}

	IEnumerator InvulnerableAgain(){
		yield return new WaitForSeconds(3);
		vulnerable = false;
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Bullet" && vulnerable) {
			Debug.Log ("Boss Lives: " + lives.ToString());
			lives--;
			if (lives <= (MAX_LIVES / 2)) {
				Debug.Log ("FireHEAD");
				fireHead.SetActive (true);
			}
			if (lives == 0) {
				Debug.Log ("This should be the end!");
				Destroy (this.gameObject);
				PlayerData.Instance.FinalFinalBossIsDead ();
			}
		}
	}

}
