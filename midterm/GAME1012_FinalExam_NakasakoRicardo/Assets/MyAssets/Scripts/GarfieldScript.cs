using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarfieldScript : MonoBehaviour {
	[SerializeField] Animator anim;
	[SerializeField] TextMesh healthText;
	[SerializeField] GameObject greatFurball;
	[SerializeField] Transform furballSpit;
	[SerializeField] float furrrce;

	int lives = 18; /* BEAST! */

	void Start(){
	}

	// Update is called once per frame
	void Update () {
		healthText.text = "Health: " + lives.ToString ();
		//anim.SetBool ("PlayerArrived", true);
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Bullet" ) {
			lives--;
			if (lives == 0) {
				Destroy (this.gameObject);
			}
		}
	}

	void SpitFire(){
		GameObject bulletInst = Instantiate (greatFurball, furballSpit.position, furballSpit.rotation) as GameObject;
		bulletInst.GetComponent<Rigidbody2D> ().AddForce (Vector2.left * transform.localScale.x * furrrce * Time.deltaTime);
	}


}
