using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SimplePersist : MonoBehaviour {
	public static bool camExists = false;
	void Start () {
		if (camExists == false) {
			DontDestroyOnLoad(this.gameObject);
			camExists = true;
		}
		else {
			Destroy(this.gameObject);
		}
	}
} 