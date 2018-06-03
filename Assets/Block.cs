using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

	public int width;
	public int height;
	public int maxHealth;
	public int currentHealth;
	public string buildingMaterialName;

	public void InitializeBlock(int width, int height) {
		this.transform.localScale = new Vector3 (width, height, 1);
		currentHealth = maxHealth;
	}

	public void TakeDamage(int damage) {
		currentHealth -= damage;

		if (currentHealth <= 0) {
			Die ();
		}
	}

	void Die() {
		Debug.Log ("ahhhh");
		GameObject.Destroy (this.gameObject);
	}
}