using UnityEngine;
using System.Collections;

public class PlayerData 
{
	private static PlayerData instance = null;
    private int score = 0;
    private int highScore = 0;

    private PlayerData () {
        Debug.Log("PlayerData created!");
        highScore = PlayerPrefs.GetInt("HighScore");
    }

    public static PlayerData Instance
	{
		get {
			if (instance == null) {
				instance = new PlayerData();
			}
			return instance;
		}
	}
       
	public int Score {
		get {
			return score;
		}
		set {
			score = value;
            //PlayerData.Instance.Score = PlayerData.Instance.Score + 1;
			if (score > highScore)
			{
				highScore = score;
				PlayerPrefs.SetInt("HighScore", highScore);
			}
		}
	}

    public int HighScore {
		get {
			return highScore;
		}
	}

}
