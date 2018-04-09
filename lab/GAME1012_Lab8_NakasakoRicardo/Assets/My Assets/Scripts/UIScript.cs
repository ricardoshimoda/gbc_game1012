using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour {
    public Text score;
    public Text highScore;
    public static UIScript instance;

	void Start () {
        if (instance == null) {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else 
            GameObject.Destroy(this.gameObject);
    }

    void Update ()
    {
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
}
