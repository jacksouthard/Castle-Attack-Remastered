using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {
    public int mapWidth;
    public int mapResolution;

    public MeshFilter terrainMesh;


    float[] heights;

	private void Start() {
        GenerateTerrain();
	}

	public void GenerateTerrain() {
        heights = new float[mapWidth * mapResolution];

        //initialize points
        for (int i = 0; i < heights.Length; i++) {
            heights[i] = Random.Range(5f, 10f);
        }

        UpdateVisual();
    }

    void UpdateVisual() {
        int pointCount = heights.Length + 2;
        Vector2[] points2D = new Vector2[pointCount];
        Vector3[] points3D = new Vector3[pointCount];

        for (int i = 0; i < heights.Length; i++) {
            Vector2 point = new Vector2(i / (float)mapResolution, heights[i]);
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
}
