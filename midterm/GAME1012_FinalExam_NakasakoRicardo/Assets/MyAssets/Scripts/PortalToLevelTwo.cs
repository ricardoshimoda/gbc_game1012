using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalToLevelTwo : MonoBehaviour {
	[SerializeField] Vector2 destination = new Vector2(63.74f, 29.52f);
	[SerializeField] string levelToLoad;
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player")
		{
			other.transform.position = destination;
			SceneManager.LoadScene(levelToLoad);
		}
	}
}
