using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTD : MonoBehaviour {
    private Mesh voxelMesh;

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private MeshCollider meshCollider;

    private List<Vector3> vertices = new List<Vector3>();
    private enum VoxelSide { TOP }

    private List<int> triangles = new List<int>();
    private int vertexIndex;
    
    void Start() {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();

        voxelMesh = new Mesh();
        voxelMesh.name = "Voxel";

        VoxelGen();

        MeshGen();
    }

    void Update() {
        
    }

    private void MeshGen() {
        voxelMesh.vertices = vertices.ToArray();
        voxelMesh.triangles = triangles.ToArray();

        voxelMesh.RecalculateBounds();
        voxelMesh.RecalculateNormals();
        voxelMesh.Optimize();

        meshFilter.mesh = voxelMesh;
        meshCollider.sharedMesh = voxelMesh;
    }

    private void VoxelGen() {
        VerticesAdd(VoxelSide.TOP);
    }

    private void VerticesAdd(VoxelSide side) {
        switch(side) {
            case VoxelSide.TOP: {
                vertices.Add(new Vector3(0, 0, 0));
                vertices.Add(new Vector3(0, 0, 1));
                vertices.Add(new Vector3(1, 0, 1));
                vertices.Add(new Vector3(1, 0, 0));
                break;
            }
        }

        TrianglesAdd();
    }

    private void TrianglesAdd() {
        // Primeiro Tiangulo
        triangles.Add(0 + vertexIndex);
        triangles.Add(1 + vertexIndex);
        triangles.Add(2 + vertexIndex);

        // Segundo Triangulo
        triangles.Add(0 + vertexIndex);
        triangles.Add(2 + vertexIndex);
        triangles.Add(3 + vertexIndex);

        vertexIndex += 4;
    }
}
