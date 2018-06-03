using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {
    public static TerrainGenerator instance;

    public int mapWidth;
    public float maxHeightDiff;

    public MeshFilter terrainMesh;

    float[] heights;

    private void Awake() {
        instance = this;

        GenerateTerrain();
	}

	public void GenerateTerrain() {
        heights = new float[mapWidth];

        //initialize points
        for (int i = 0; i < heights.Length; i++) {
            //heights[i] = Random.Range(5f, 10f);
            heights[i] = 5f;
        }

        UpdateVisual();
    }

	/*private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            float xPos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            AddMassAtPos(xPos, 1f);
        }
	}*/

	void UpdateVisual() {
        int pointCount = heights.Length + 2;
        Vector2[] points2D = new Vector2[pointCount];
        Vector3[] points3D = new Vector3[pointCount];

        for (int i = 0; i < heights.Length; i++) {
            Vector2 point = new Vector2(i, heights[i]);
            points2D[i] = point;
            points3D[i] = new Vector3(point.x, point.y, 0f);
        }

        Vector2 rightCorner = new Vector2(mapWidth, 0f);
        Vector2 leftCorner = Vector2.zero;

        points2D[pointCount - 2] = rightCorner;
        points3D[pointCount - 2] = new Vector3(rightCorner.x, rightCorner.y, 0f);
        points2D[pointCount - 1] = leftCorner;
        points3D[pointCount - 1] = new Vector3(leftCorner.x, leftCorner.y, 0f);

        Mesh mesh = new Mesh();
        Triangulator tr = new Triangulator(points2D);
        int[] triangles = tr.Triangulate();
        mesh.vertices = points3D;
        mesh.triangles = triangles;
        mesh.uv = points2D;

        terrainMesh.mesh = mesh;
    }

    public int GetLowestHeight(float left, int width) {
        int index = Mathf.RoundToInt(left);
        float curMinHeight = Mathf.Infinity;

        for (int i = 0; i < width; i++) {
            curMinHeight = Mathf.Min(curMinHeight, heights[index + i]);
        }

        return Mathf.FloorToInt(curMinHeight);
    }

    public PointData GetPointDataAtPos(float xPos) {
        int leftIndex = Mathf.FloorToInt(xPos);
        int rightIndex = Mathf.CeilToInt(xPos);
        float p = (xPos - leftIndex);

        float yPos = Mathf.Lerp(heights[leftIndex], heights[rightIndex], p);
        Vector2 diff = new Vector2(1f, heights[rightIndex] - heights[leftIndex]);
        float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        return new PointData(yPos, angle);
    }

    /*public void AddMassAtPos(float xPos, float mass) {
        int index = Mathf.RoundToInt(xPos * mapResolution);
        AddMassAtIndex(index, mass);
        
        UpdateVisual();
    }

    bool AddMassAtIndex(int index, float mass) {
        if(index 

        float curHeight = heights[index];
        float remaininingMass = mass;

        float maxDiff = Mathf.Max(Mathf.Abs(curHeight - heights[index - 1]), Mathf.Abs(curHeight - heights[index + 1]));
        if (maxDiff < maxHeightDiff) {
            heights[index] += mass;
            return 0f;
        } else {
            remaininingMass -= maxHeightDiff;
            AddMassAtIndex(index-1,remaininingMass/2);
            AddMassAtIndex(index+1,remaininingMass/2);
            return 0f;
        }
    }*/

    public struct PointData {
        public float yPos;
        public float angle;

        public PointData(float _yPos, float _angle) {
            yPos = _yPos;
            angle = _angle;
        }
    }
}
