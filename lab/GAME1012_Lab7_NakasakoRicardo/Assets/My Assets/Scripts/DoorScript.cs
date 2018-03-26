using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DoorScript : MonoBehaviour {
	[SerializeField] Vector2 destination;
	[SerializeField] string levelToLoad;
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player")
		{
			other.transform.position = destination;
			SceneManager.LoadScene(levelToLoad);
		}
	}
} 