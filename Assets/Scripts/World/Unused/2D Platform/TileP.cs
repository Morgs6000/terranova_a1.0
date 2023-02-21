using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileP : MonoBehaviour {
    private Mesh voxelMesh;

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    private List<Vector3> vertices = new List<Vector3>();
    private enum VoxelSide { BACK }

    private List<int> triangles = new List<int>();
    private int vertexIndex;

    void Start() {
        
    }

    void Update() {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();

        voxelMesh = new Mesh();
        voxelMesh.name = "Tile";

        VoxelGen();

        MeshGen();
    }

    private void MeshGen() {
        voxelMesh.vertices = vertices.ToArray();
        voxelMesh.triangles = triangles.ToArray();

        voxelMesh.RecalculateBounds();
        voxelMesh.RecalculateNormals();
        voxelMesh.Optimize();

        meshFilter.mesh = voxelMesh;
    }

    private void VoxelGen() {
        VerticesAdd(VoxelSide.BACK);
    }

    private void VerticesAdd(VoxelSide side) {
        switch(side) {
            case VoxelSide.BACK: {
                vertices.Add(new Vector3(0, 0, 0));
                vertices.Add(new Vector3(0, 1, 0));
                vertices.Add(new Vector3(1, 1, 0));
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
