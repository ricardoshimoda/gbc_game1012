using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFinalFinalBoss : MonoBehaviour {
	[SerializeField] Animator startDoorAnim;
	[SerializeField] GameObject mists;
	[SerializeField] GameObject boss;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player")
		{
			startDoorAnim.SetBool ("Open", false);
			mists.SetActive (true);
			StartCoroutine (ShowBoss ());
		}
	}

	IEnumerator ShowBoss(){
		yield return new WaitForSeconds(3);
		boss.SetActive (true);
	}
}
