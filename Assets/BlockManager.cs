using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour {
	public static BlockManager instance;

	public GameObject blockPrefab;
	public float lerpSpeed;

	int widthOfPendingBlock = 1;
	int heightOfPendingBlock = 2;

	void Awake() {
		instance = this;
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

		float xPosition = Mathf.RoundToInt(left) + (widthOfPendingBlock / 2f);
		float yPosition;
		if (highestY != 0) {
			yPosition = highestY;
			// must be block below new block
		} else {
			// no blocks below placed block
			yPosition = TerrainGenerator.instance.GetLowestHeight (left + 0.5f, widthOfPendingBlock + 1);
		}

		Vector3 startPosition = new Vector3 (xPosition + (widthOfPendingBlock * .5f), 50f, 0);
		Vector3 endPosition = new Vector3 (xPosition + (widthOfPendingBlock * .5f), yPosition + (heightOfPendingBlock * .5f), 0);

		GameObject newBlockGO = Instantiate (blockPrefab, startPosition, Quaternion.identity, this.transform);
		Block newBlock = newBlockGO.GetComponent<Block> ();
		newBlock.InitializeBlock (widthOfPendingBlock, heightOfPendingBlock, blocksUnderNewBlock.Count);
		foreach (var block in blocksUnderNewBlock) {
			block.supportingBlocks.Add (newBlock);
		}

		StartCoroutine(LerpIt(newBlockGO.transform, startPosition, endPosition));
	}

	IEnumerator LerpIt(Transform t, Vector3 startPosition, Vector3 endPosition) {
		float startTime = Time.time;
		float journeyLength = Vector3.Distance(startPosition, endPosition);

        while (true) {
			yield return new WaitForEndOfFrame ();
			float distCovered = (Time.time - startTime) * lerpSpeed;
			float fracJourney = distCovered / journeyLength;
			t.position = Vector3.Lerp (startPosition, endPosition, fracJourney);

            if (fracJourney >= 0.99f) {
                break;
            }
		}

        t.position = endPosition;
	}
}
