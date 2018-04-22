using UnityEngine;
using System.Collections;

public class PlayerData 
{
	private static PlayerData instance = null;
	private int lives = 2;
	private int ammo = 15;

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

}
