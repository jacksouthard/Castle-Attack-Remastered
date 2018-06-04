using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

	public int width;
	public int height;
	public int maxHealth;
	public int currentHealth;
	public string buildingMaterialName;

	public List<Block> supportingBlocks; // the blocks that would fall if this block is destroyed
	public int numberOfBlocksSupportingThisBlock;

	public void InitializeBlock(int width, int height, int _numberOfBlocksSupportingThisBlock) {
		this.transform.localScale = new Vector3 (width, height, 1);
		currentHealth = maxHealth;
		numberOfBlocksSupportingThisBlock = _numberOfBlocksSupportingThisBlock;
	}

	public void TakeDamage(int damage) {
		currentHealth -= damage;

		if (currentHealth <= 0) {
			Die ();
		}
	}

	public void SupportingBlockRemoved () {
		print ("Supporting block removed");
		numberOfBlocksSupportingThisBlock--;
		if (numberOfBlocksSupportingThisBlock <= 0) {
			Fall ();
		}
	}

	void Fall () {
		print ("Fall");
		Die ();
	}

	void Die() {
		foreach (var supportingBlock in supportingBlocks) {
			supportingBlock.SupportingBlockRemoved ();
		}
		GameObject.Destroy (this.gameObject);
	}
}