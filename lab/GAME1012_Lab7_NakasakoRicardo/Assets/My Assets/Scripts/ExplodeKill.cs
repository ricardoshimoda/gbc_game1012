using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ExplodeKill : MonoBehaviour {
	void Kill() {
		Destroy(this.gameObject);
		SceneManager.LoadScene("Menu");
	}
} 