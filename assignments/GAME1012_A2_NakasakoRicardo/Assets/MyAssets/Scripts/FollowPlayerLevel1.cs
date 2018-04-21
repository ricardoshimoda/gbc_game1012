using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerLevel1 : MonoBehaviour {
	[SerializeField] GameObject player;
	[SerializeField] float movementDamping = 1.0f;

	Region[] cameraFieldsForever;

    public class Region {
		/* Limits the region */
        public float xStart;
		public float xEnd;
		public float yStart;
		public float yEnd;

		/* Limits camera movement */
		public float minX;
		public float maxX;
		public float minY;
		public float maxY;
		public float xConst;
		public float yConst;
		public bool dampX;
		public bool dampY;
    }

	void Start (){
		cameraFieldsForever = new Region[]{
			new Region(){
				xStart = -24.5f, xEnd = -15.9f,
				yStart = -3.3f, yEnd = 10.3f,
				dampX = true, xConst = -15.9f,
				dampY = true, yConst = 2.6f,
			},
			new Region(){
				xStart = -15.9f, xEnd = 15.0f,
				yStart = -3.3f, yEnd = 10.3f,
				minX = -15.9f, maxX = 14.5f, 
				dampX = false,
				dampY = true, yConst = 2.6f,
			},
			new Region(){
				xStart = 15.0f, xEnd = 21.0f,
				yStart = -3.3f, yEnd = 10.3f,
				dampX = true, xConst = 14.5f,
				dampY = true, yConst = 2.6f,
			},
			new Region(){
				xStart = 21.0f, xEnd = 23.3f,
				yStart = -3.3f, yEnd = 6f,
				minY=-10, maxY = -3.3f,
				dampX = true, xConst = 14.8f,
				dampY = false,
			},
			new Region(){
				xStart = 13f, xEnd = 23.3f,
				yStart = -10f, yEnd = -3.3f,
				dampX = true, xConst = 14.77f,
				dampY = true, yConst = -5.7f,
			},
			new Region(){
				xStart = 10f, xEnd = 13f,
				yStart = -16f, yEnd = -3.3f,
				minY = -12.68f, maxY = -3.3f,
				dampX = true, xConst = 14.77f,
				dampY = false,								
			},
			new Region(){
				xStart = 13f, xEnd = 56.11f,
				yStart = -18f, yEnd = -10f,
				minX = 14.7f, maxX = 49.4f,
				dampX = false,
				dampY = true, yConst = -12.68f,
			},
			new Region(){
				xStart = 46f, xEnd = 56.11f,
				yStart = -10f, yEnd = -3f,
				dampX = true, xConst = 49.4f,
				dampY = true, yConst = -5.28f,
			},
			new Region(){
				xStart = 40f, xEnd = 56.11f,
				yStart = -3f, yEnd = 6f,
				dampX = true, xConst = 49.4f,
				dampY = true, yConst = 2f,
			},
			new Region(){
				xStart = 40f, xEnd = 56.11f,
				yStart = 6f, yEnd = 13f,
				dampX = true, xConst = 49.4f,
				dampY = true, yConst = 11f,
			},
			new Region(){
				xStart = 40f, xEnd = 56.11f,
				yStart = 13f, yEnd = 23f,
				dampX = true, xConst = 49.4f,
				dampY = true, yConst = 18f,
			},
			new Region(){
				xStart = 40f, xEnd = 56.11f,
				yStart = 23f, yEnd = 29f,
				dampX = true, xConst = 49.4f,
				dampY = true, yConst = 27f,
			},
			new Region(){
				xStart = 40f, xEnd = 53f,
				yStart = 29f, yEnd = 41f,
				dampX = true, xConst = 49.4f,
				dampY = true, yConst = 33f,
			},
			new Region(){
				xStart = 53f, xEnd = 90f,
				yStart = 29f, yEnd = 41f,
				minX = 55f, maxX = 82.1f,
				dampX = false, 
				dampY = true, yConst = 33f,
			}

		};
	}

	void Update(){
		// Early out if we don't have a target.
		if (!player) { return; }
		Region currentCameraSetup = null;
		Vector3 playerPosition = player.transform.position;

		for (int cameraSetup = 0; cameraSetup < cameraFieldsForever.Length; cameraSetup++) {
			Region tempCamera = cameraFieldsForever [cameraSetup];
			if (playerPosition.x >= tempCamera.xStart
				&& playerPosition.x < tempCamera.xEnd
				&& playerPosition.y >= tempCamera.yStart
				&& playerPosition.y < tempCamera.yEnd) {
				currentCameraSetup = tempCamera; 
				Debug.Log (cameraSetup);
				break;
			}
		}
		if (currentCameraSetup == null)
			Debug.Log ("Encontrei bosta nenhuma");
		// Get the current position of the camera.
		Vector3 currentPosition = this.transform.position;
		Vector3 futurePosition = this.transform.position;

		// Store the camera's z value.
		// This value will be modified by the next line.
		float cameraZ = currentPosition.z;
		float? cameraY=null;
		float? cameraX=null;

		bool skipLerp = false;

		// If camera setup has been identified then applies the conditions
		if (currentCameraSetup != null) {
			skipLerp = currentCameraSetup.dampY && currentCameraSetup.dampX;
			if (currentCameraSetup.dampY) {
				futurePosition.y = currentCameraSetup.yConst;
			} else if (currentPosition.y < currentCameraSetup.minY) {
				futurePosition.y = currentCameraSetup.minY;
				//skipLerp=true;
			} else if (currentPosition.y > currentCameraSetup.maxY) {
				//skipLerp=true;
				futurePosition.y = currentCameraSetup.maxY;
			} else {
				futurePosition.y = player.transform.position.y;
			}

			if (currentCameraSetup.dampX) {
				futurePosition.x = currentCameraSetup.xConst;
			} else if (player.transform.position.x < currentCameraSetup.minX) {
				Debug.Log("go to the min");
				futurePosition.x = currentCameraSetup.minX;
				//skipLerp=true;
			} else if (player.transform.position.x > currentCameraSetup.maxX) {
				Debug.Log("go to the max");
				futurePosition.x = currentCameraSetup.maxX;
			} else {
				Debug.Log("folllow the player");
				futurePosition.x = player.transform.position.x;
			}
		}
		currentPosition = Vector3.Lerp (currentPosition, futurePosition, this.movementDamping * Time.deltaTime);
			
		currentPosition.z = cameraZ;
		/*
		if (cameraY.HasValue) {
			currentPosition.y = cameraY.Value;
		}

		if (cameraX.HasValue) {
			currentPosition.x = cameraX.Value;
		}*/

		// Update the current position with the smoothed position towards the target object.
		this.transform.position = currentPosition;
	}

}
