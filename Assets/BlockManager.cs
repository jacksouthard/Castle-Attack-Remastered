using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour {
	public GameObject blockPrefab;

	int widthOfPendingBlock = 1;
	int heightOfPendingBlock = 2;

	// Use this for initialization
	void Start () {
		CreateBlock (10f);
		CreateBlock (11f);

		widthOfPendingBlock = 3;
		CreateBlock (10f);
	}

	public void CreateBlock(float left) {
		// check for blocks below new block
		float highestY = 0f;
		List<Block> blocksUnderNewBlock = new List<Block> ();
		for (int i = 0; i < widthOfPendingBlock; i++) {
			float xPosToCheck = left + i + 0.5f;
			RaycastHit2D hit = Physics2D.Raycast (new Vector2 (xPosToCheck, 50f), Vector2.down);
			if (hit.transform != null && hit.collider.tag == "Base") {
				// block underneith new block
				if (hit.point.y > highestY) {
					highestY = hit.point.y;

					blocksUnderNewBlock.Clear ();
					blocksUnderNewBlock.Add (hit.collider.GetComponent<Block> ());
				} else if (hit.point.y == highestY) {
					blocksUnderNewBlock.Add (hit.collider.GetComponent<Block> ());
				}
			}
		}

		float yPosition;
		if (highestY != 0) {
			yPosition = highestY;
			// must be block below new block
		} else {
			// no blocks below placed block
			yPosition = TerrainGenerator.instance.GetLowestHeight (left + 0.5f, widthOfPendingBlock + 1);
		}

		float xPosition = Mathf.RoundToInt (left); // left should already be rounded;

		GameObject newBlockGO = Instantiate (blockPrefab, new Vector3 (xPosition + (widthOfPendingBlock / 2f), yPosition + (heightOfPendingBlock / 2f), 0), Quaternion.identity, this.transform);
		Block newBlock = newBlockGO.GetComponent<Block> ();
		newBlock.InitializeBlock (widthOfPendingBlock, heightOfPendingBlock, blocksUnderNewBlock.Count);
		foreach (var block in blocksUnderNewBlock) {
			block.supportingBlocks.Add (newBlock);
		}
	}
}
