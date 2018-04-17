using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour {
	public int gameTime = 10;
	public GameObject gun;
	public Text timer;
	public Text score;
	public Text highScore;
    public static UIScript instance;

	int internalTimer;

	void Start () {
        if (instance == null) {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else 
            GameObject.Destroy(this.gameObject);
		internalTimer = gameTime;
		InvokeRepeating("TimeGoesBySoSlowly", 0f, 1f);
    }

    void Update ()
    {
		if (internalTimer > 0) {
			this.timer.text = "Timer: " + internalTimer.ToString ();	
		} else {
			this.timer.text = "Time's up!";	
		}
        this.score.text = "Score: " + PlayerData.Instance.Score.ToString();
        this.highScore.text = "Highscore: " + PlayerData.Instance.HighScore.ToString();
    }

    void OnSceneLoaded (Scene scene)
    {
        if (scene.name == "Menu")
        {
            PlayerData.Instance.Score = 0;
            Destroy (this.gameObject);
        }
    }

	void TimeGoesBySoSlowly(){
		internalTimer--;
		if (internalTimer <= 0) {
			CancelInvoke ("TimeGoesBySoSlowly");
			var theScript = gun.GetComponent<GunScript> ();
			theScript.enabled = false;
		}

	}
}
