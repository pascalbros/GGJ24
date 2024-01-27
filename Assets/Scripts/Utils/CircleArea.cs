using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class CircleMeshGenerator: MonoBehaviour {
    public float radius = 1f;
    public int segments = 32;

    void Start() {
        GenerateCircle();
    }

    void GenerateCircle() {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        // Create a new mesh
        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;

        // Vertices and UVs
        Vector3[] vertices = new Vector3[segments + 1];
        Vector2[] uv = new Vector2[segments + 1];

        for (int i = 0; i <= segments; i++) {
            float angle = Mathf.Deg2Rad * (360f * i / segments);
            vertices[i] = new Vector3(Mathf.Cos(angle) * radius, 0f, Mathf.Sin(angle) * radius);
            uv[i] = new Vector2(vertices[i].x / radius / 2f + 0.5f, vertices[i].z / radius / 2f + 0.5f);
        }

        // Triangles
        int[] triangles = new int[segments * 3];

        for (int i = 0, ti = 0; i < segments; i++, ti += 3) {
            triangles[ti] = 0;
            triangles[ti + 1] = i + 1;
            triangles[ti + 2] = (i + 2) % (segments + 1);
        }

        // Assign data to the mesh
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        // Recalculate normals for lighting
        mesh.RecalculateNormals();
    }
}