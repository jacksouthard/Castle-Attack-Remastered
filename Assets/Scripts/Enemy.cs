using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	public float movementSpeed;

	float slopeModifier = 0.05f; // ratio multiplied to speed based on how close the enemies rotation is to 90
	float xPos;
	float steepness = 0f;

	void Start () {
		xPos = transform.position.x;
	}
	
	void Update () {
		Move ();
	}

	void Move () {
		float speedMultiplier = Mathf.Lerp (1f, slopeModifier, steepness / 90f);
		xPos -= (movementSpeed * speedMultiplier * Time.deltaTime);
		TerrainGenerator.PointData pointData = TerrainGenerator.instance.GetPointDataAtPos (xPos);
		if (pointData.yPos != null) {
			transform.position = new Vector3 (xPos, pointData.yPos, 0);
			transform.rotation = Quaternion.Euler (0f, 0f, pointData.angle);
			steepness = Mathf.Abs (pointData.angle);
		}
	}
}
