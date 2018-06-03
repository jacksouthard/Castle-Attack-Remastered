using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour {
	public static BlockManager instance;
	public float lerpSpeed = 1f;

	private void Awake() {
		instance = this;
	}

	public GameObject woodBlock;

	int widthOfPendingBlock = 1;
	int heightOfPendingBlock = 1;

	public void CreateBlock(float mouseXPosition, float mouseYPosition) {
		float yPosition = TerrainGenerator.instance.GetLowestHeight (mouseXPosition, widthOfPendingBlock +1);
		float xPosition = Mathf.RoundToInt(mouseXPosition);
		Vector3 startPosition = new Vector3 (xPosition + (widthOfPendingBlock * .5f), mouseYPosition, 0);
		Vector3 endPosition = new Vector3 (xPosition + (widthOfPendingBlock * .5f), yPosition + (heightOfPendingBlock * .5f), 0);
		GameObject newBlock = Instantiate (woodBlock, startPosition, Quaternion.identity, this.transform);
		Block block = newBlock.GetComponent<Block> ();
		block.InitializeBlock (widthOfPendingBlock, heightOfPendingBlock);
		StartCoroutine(LerpIt(block.transform, startPosition, endPosition));
	}

	IEnumerator LerpIt(Transform t, Vector3 startPosition, Vector3 endPosition) {
		float startTime = Time.time;
		float journeyLength = Vector3.Distance(startPosition, endPosition);

		while (true) {
			yield return new WaitForEndOfFrame ();
			float distCovered = (Time.time - startTime) * lerpSpeed;
			float fracJourney = distCovered / journeyLength;
			t.position = Vector3.Lerp (startPosition, endPosition, fracJourney);
		}
	}
}
