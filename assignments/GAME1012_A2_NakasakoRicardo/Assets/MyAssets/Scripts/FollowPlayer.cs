using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowPlayer : MonoBehaviour {
	[SerializeField] GameObject player;
	[SerializeField] float movementDamping = 1.0f;

	Region[][] cameraFieldsForever;
	int currentScene;

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
		currentScene = int.Parse(SceneManager.GetActiveScene().name.Substring(6,1)) - 1;
		cameraFieldsForever = new Region[][]{
			/* Level 1 - array 0 */
			new Region[] {
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
					xStart = 13f, xEnd = 58.11f,
					yStart = -18f, yEnd = -9f,
					minX = 14.7f, maxX = 49.4f,
					dampX = false,
					dampY = true, yConst = -12.68f,
				},
				new Region(){
					xStart = 46f, xEnd = 58.11f,
					yStart = -10f, yEnd = -3f,
					dampX = true, xConst = 49.4f,
					dampY = true, yConst = -5.28f,
				},
				new Region(){
					xStart = 40f, xEnd = 58.11f,
					yStart = -3f, yEnd = 6f,
					dampX = true, xConst = 49.4f,
					dampY = true, yConst = 2f,
				},
				new Region(){
					xStart = 40f, xEnd = 58.11f,
					yStart = 6f, yEnd = 13f,
					dampX = true, xConst = 49.4f,
					dampY = true, yConst = 11f,
				},
				new Region(){
					xStart = 40f, xEnd = 58.11f,
					yStart = 13f, yEnd = 23f,
					dampX = true, xConst = 49.4f,
					dampY = true, yConst = 18f,
				},
				new Region(){
					xStart = 40f, xEnd = 58.11f,
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
			},
			new Region[] {
				new Region(){
					xStart = 63f, xEnd = 156f,
					yStart = 25f, yEnd = 33f,
					minX = 69.6f, maxX = 151.3f,
					dampX = false, 
					dampY = true, yConst = 30.5f,
				},
				new Region(){
					xStart = 151f, xEnd = 156f,
					yStart = 33f, yEnd = 57f,
					minY = 40f, maxY = 63.4f,
					dampX = true, xConst = 151.3f,
					dampY = false,
				},
				new Region(){
					xStart = 142f, xEnd = 151f,
					yStart = 33f, yEnd = 57f,
					minY = 40f, maxY = 63.4f,
					dampX = true, xConst = 148.3f,
					dampY = false,
				},
				new Region(){
					xStart = 142f, xEnd = 216f,
					yStart = 57f, yEnd = 70f,
					minX = 148.3f, maxX = 209f,
					dampX = false, //xConst = 148.3f,
					dampY = true, yConst=63.2f
				},
			}
		};
	}

	void Update(){
		// Early out if we don't have a target.
		if (!player) { 
			Debug.Log ("CANNOT FIND PLAYER!!!!");
			return;
		}
		Region[] cameraFieldsScene = cameraFieldsForever[currentScene];
		Region currentCameraSetup = null;
		Vector3 playerPosition = player.transform.position;

		for (int cameraSetup = 0; cameraSetup < cameraFieldsScene.Length; cameraSetup++) {
			Region tempCamera = cameraFieldsScene [cameraSetup];
			if (playerPosition.x >= tempCamera.xStart
				&& playerPosition.x < tempCamera.xEnd
				&& playerPosition.y >= tempCamera.yStart
				&& playerPosition.y < tempCamera.yEnd) {
				currentCameraSetup = tempCamera;
				break;
			}
		}
		if (currentCameraSetup == null) {
			Debug.Log ("UNMAPPED POSITON!!!!");
		}
		// Get the current position of the camera.
		Vector3 currentPosition = this.transform.position;
		Vector3 futurePosition = this.transform.position;

		// Store the camera's z value.
		// This value will be modified by the next line.
		float cameraZ = currentPosition.z;

		// If camera setup has been identified then applies the conditions
		if (currentCameraSetup != null) {
			if (currentCameraSetup.dampY) {
				futurePosition.y = currentCameraSetup.yConst;
			} else if (player.transform.position.y < currentCameraSetup.minY) {
				futurePosition.y = currentCameraSetup.minY;
				//skipLerp=true;
			} else if (player.transform.position.y > currentCameraSetup.maxY) {
				//skipLerp=true;
				futurePosition.y = currentCameraSetup.maxY;
			} else {
				futurePosition.y = player.transform.position.y;
			}

			if (currentCameraSetup.dampX) {
				futurePosition.x = currentCameraSetup.xConst;
			} else if (player.transform.position.x < currentCameraSetup.minX) {
				futurePosition.x = currentCameraSetup.minX;
				//skipLerp=true;
			} else if (player.transform.position.x > currentCameraSetup.maxX) {
				futurePosition.x = currentCameraSetup.maxX;
			} else {
				futurePosition.x = player.transform.position.x;
			}
		}
		currentPosition = Vector3.Lerp (currentPosition, futurePosition, this.movementDamping * Time.deltaTime);

		// Only Z gets to be permanent
		currentPosition.z = cameraZ;

		// Update the current position with the smoothed position towards the target object.
		this.transform.position = currentPosition;
	}

}
