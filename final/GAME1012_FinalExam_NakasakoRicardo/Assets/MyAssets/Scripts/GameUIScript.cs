using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUIScript : MonoBehaviour {
	[SerializeField] Text ammo;
	[SerializeField] Image[] lives; 
	[SerializeField] Text gameOver;
	[SerializeField] Text winner;

	public static GameUIScript instance;

	// Use this for initialization
	void Start () {
		if (instance == null) {
			DontDestroyOnLoad(this.gameObject);
			instance = this;
		}
		else 
			GameObject.Destroy(this.gameObject);
		
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerData.Instance.Lives == 0) {
			gameOver.enabled = true;
		}
		ammo.text = "X " + PlayerData.Instance.Ammo;
		for (int i = 0; i < lives.Length; i++) {
			lives [i].enabled = false;
		}
		for (int i = 0; i < PlayerData.Instance.Lives; i++) {
			lives [i].enabled = true;
		}
		if (!PlayerData.Instance.FinalFinalBossStatus ()) {
			Debug.Log ("You are a winner!");
			winner.enabled = true;
		}
	}
}
