using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

	[SerializeField] float rotation;
	[SerializeField] Renderer flame;
	[SerializeField] GameObject explosion;

	Rigidbody2D rb;
	bool thrust = false;

	[SerializeField] int speed;

	public static bool exists = false;

	void Start(){
		if (exists == false) {
			PlayerData.Instance.Score = 0;
			PlayerData.Instance.Lives = 11;
			rb = GetComponent<Rigidbody2D> ();
			flame.enabled = false;
			exists = true;
			DontDestroyOnLoad (this.gameObject);
		} else {
			GameObject.Destroy (this.gameObject);
		}
	}

	void FixedUpdate() {
		if (thrust == true)
		{
			Vector2 worldDir = transform.TransformDirection(Vector2.up);
			rb.AddForce(worldDir*speed*Time.fixedDeltaTime);
		}
	}
		
	// Update is called once per frame
	void Update () {
		transform.Rotate(0,0,Input.GetAxis("Horizontal")*rotation*Time.deltaTime);
		if (Input.GetKey(KeyCode.Space)) {
			ToggleFlame(true);
		}
		if (Input.GetKeyUp(KeyCode.Space)) {
			ToggleFlame(false);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Coin") {
			PlayerData.Instance.Score++;
			Destroy (other.gameObject,0);
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Wall") {
			PlayerData.Instance.Lives--;
			if (PlayerData.Instance.Lives == 0) {
				exists = false;
				GameObject.Instantiate(explosion, transform.position, transform.rotation);
				Destroy (gameObject,0);
			}
		}
	}

	void ToggleFlame(bool t)
	{
		thrust = t;
		flame.enabled = t;
	}

}
