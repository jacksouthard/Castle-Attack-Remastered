using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

	public int width;
	public int height;
	public int maxHealth;
	public int currentHealth;
	public string buildingMaterialName;

	public void InitializeBlock(float left, int width, int height) {
		float yPosition = TerrainGenerator.instance.GetLowestHeight (left, width);
		float xPosition = Mathf.RoundToInt(left);
		this.transform.position = new Vector3 (xPosition, yPosition, 0);
		currentHealth = maxHealth;
	}
}