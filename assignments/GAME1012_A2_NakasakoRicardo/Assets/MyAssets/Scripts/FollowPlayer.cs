using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {
	[SerializeField] GameObject player;
	[SerializeField] float movementDamping = 1.0f;

    const float cameraMinX = -15.7f;

    public class Region {
        float xStart;
        float xEnd;
        float yStart;
        float yEnd;
        float xConst;
        float yConst;
        
        bool dampX;
        bool dampY;
    }

	void Start (){

	}

	void Update(){
		// Early out if we don't have a target.
		if (!player) { return; }

		// Get the current position of the camera.
		Vector3 currentPosition = this.transform.position;

		PlayerScript playerData = player.GetComponent<PlayerScript> ();

		// Store the camera's z value.
		// This value will be modified by the next line.
		float cameraZ = currentPosition.z;
		//float cameraY = int.MinValue;

		// store Y value if player is not jumping around
        /*
		if (playerData.isGrounded) {
			cameraY = currentPosition.y;
		}*/
		currentPosition = Vector3.Lerp(currentPosition, player.transform.position, this.movementDamping * Time.deltaTime);

		currentPosition.z = cameraZ;
        //currentPosition = cameraY;

		// Update the current position with the smoothed position towards the target object.
		this.transform.position = currentPosition;
	}

}
