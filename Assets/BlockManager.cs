using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour {
	public GameObject woodBlock;

	int widthOfPendingBlock = 1;
	int heightOfPendingBlock = 1;

	// Use this for initialization
	void Start () {
		CreateBlock (4.3f);
	}

	public void CreateBlock(float left) {
		float yPosition = TerrainGenerator.instance.GetLowestHeight (left, widthOfPendingBlock);
		float xPosition = Mathf.RoundToInt(left);

		GameObject newBlock = Instantiate (woodBlock, new Vector3 (xPosition, yPosition, 0), Quaternion.identity, this.transform);
		Block block = newBlock.GetComponent<Block> ();
		block.InitializeBlock (widthOfPendingBlock, heightOfPendingBlock);
	}
}
