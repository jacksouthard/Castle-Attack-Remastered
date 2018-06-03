using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour {
	public GameObject woodBlock;

	int widthOfPendingBlock = 5;
	int heightOfPendingBlock = 5;

	// Use this for initialization
	void Start () {
		CreateBlock (3.5f);
	}

	public void CreateBlock(float left) {
		float yPosition = TerrainGenerator.instance.GetLowestHeight (left, widthOfPendingBlock +1);
		float xPosition = Mathf.RoundToInt(left);

		GameObject newBlock = Instantiate (woodBlock, new Vector3 (xPosition + (widthOfPendingBlock * .5f), yPosition + (heightOfPendingBlock * .5f), 0), Quaternion.identity, this.transform);
		Block block = newBlock.GetComponent<Block> ();
		block.InitializeBlock (widthOfPendingBlock, heightOfPendingBlock);
	}
}
