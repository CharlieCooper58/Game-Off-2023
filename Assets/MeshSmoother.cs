using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshSmoother : MonoBehaviour
{
    public MeshFilter meshFilter;
    public int iterations = 1; // Number of iterations for smoothing

    void Start()
    {
        if (meshFilter == null || meshFilter.sharedMesh == null)
        {
            Debug.LogError("Mesh filter or mesh not assigned!");
            return;
        }

        SmoothMesh(iterations);
    }

    void SmoothMesh(int iterations)
    {
        Mesh mesh = meshFilter.sharedMesh;

        for (int i = 0; i < iterations; i++)
        {
            Vector3[] originalVertices = mesh.vertices;
            Vector3[] smoothedVertices = new Vector3[originalVertices.Length];

            for (int j = 0; j < mesh.vertices.Length; j++)
            {
                Vector3 vertex = originalVertices[j];
                Vector3[] connectedVertices = GetConnectedVertices(mesh, j);

                Vector3 smoothedVertex = vertex;

                foreach (Vector3 connectedVertex in connectedVertices)
                {
                    smoothedVertex += connectedVertex;
                }

                smoothedVertex /= connectedVertices.Length + 1; // +1 to include the original vertex
                smoothedVertices[j] = smoothedVertex;
            }

            mesh.vertices = smoothedVertices;
            mesh.RecalculateNormals();
        }

        meshFilter.mesh = mesh;
    }

    Vector3[] GetConnectedVertices(Mesh mesh, int vertexIndex)
    {
        int[] triangles = mesh.triangles;
        int triangleCount = triangles.Length / 3;
        List<Vector3> connectedVertices = new List<Vector3>();

        for (int i = 0; i < triangleCount; i++)
        {
            int triIndex = i * 3;

            if (triangles[triIndex] == vertexIndex ||
                triangles[triIndex + 1] == vertexIndex ||
                triangles[triIndex + 2] == vertexIndex)
            {
                int connectedIndexA = triangles[triIndex];
                int connectedIndexB = triangles[triIndex + 1];
                int connectedIndexC = triangles[triIndex + 2];

                if (connectedIndexA != vertexIndex)
                    connectedVertices.Add(mesh.vertices[connectedIndexA]);

                if (connectedIndexB != vertexIndex)
                    connectedVertices.Add(mesh.vertices[connectedIndexB]);

                if (connectedIndexC != vertexIndex)
                    connectedVertices.Add(mesh.vertices[connectedIndexC]);
            }
        }

        return connectedVertices.ToArray();
    }
}