using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour {
	private void Update() {
        if (Input.GetMouseButtonDown(0) && false) {
            float xPos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;

			if (xPos > TerrainGenerator.instance.mapWidth) {
				return;
			}

			if (xPos < 0) {
				return;
			}

			float yPos = Camera.main.ScreenToWorldPoint (Input.mousePosition).y;

			BlockManager.instance.CreateBlock (xPos);
        }
	}
}
