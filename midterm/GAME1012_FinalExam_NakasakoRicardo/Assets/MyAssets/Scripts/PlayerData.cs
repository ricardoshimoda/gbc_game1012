using UnityEngine;
using System.Collections;

public class PlayerData 
{
	public const int MAX_LIVES = 4;
	public const int MAX_AMMO = 25;

	private static PlayerData instance = null;
	private int lives = MAX_LIVES;
	private int ammo = MAX_AMMO;

	public static PlayerData Instance
	{
		get {
			if (instance == null) {
				instance = new PlayerData();
			}
			return instance;
		}
	}

	private PlayerData () {
	}

	public int Lives {
		get {
			return this.lives;
		}
		set {
			lives = value;
		}
	}

	public int Ammo {
		get {
			return this.ammo;
		}
		set {
			ammo = value;
		}
	}

	public void MaxLives(){
		lives = MAX_LIVES;
	}

	public void MaxAmmo(){
		ammo = MAX_AMMO;
	}
}
