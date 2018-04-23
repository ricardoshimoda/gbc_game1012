using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBulletScript : MonoBehaviour {
	[SerializeField] float 	speed;
	GameObject finalBoss;

	Transform playerPosition;
	FinalFinalBossScript finalBossData;

	// Use this for initialization
	void Start () {
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		playerPosition = player.transform;
		finalBoss = GameObject.FindGameObjectWithTag ("FinalBoss");
		finalBossData = finalBoss.GetComponent<FinalFinalBossScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector2.MoveTowards (transform.position, playerPosition.position, speed);
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		Debug.Log ("I collided");
		if (other.gameObject.tag == "Bullet") {
			// Decreases bullet count for boss invunerability
			finalBossData.FBulletHit();
		}
		Destroy (this.gameObject);
	}
}
