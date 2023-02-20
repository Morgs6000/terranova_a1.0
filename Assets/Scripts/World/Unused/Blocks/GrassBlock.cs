using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassBlock : MonoBehaviour {
    private Mesh voxelMesh;

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private MeshCollider meshCollider;

    private List<Vector3> vertices = new List<Vector3>();
    private enum VoxelSide { 
        RIGHT, LEFT, TOP, BOTTOM, FRONT, BACK, 
        RIGHT1, LEFT1, FRONT1, BACK1 
    }

    private List<int> triangles = new List<int>();
    private int vertexIndex;

    private List<Vector2> uv = new List<Vector2>();
    
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
        voxelMesh.uv = uv.ToArray();

        voxelMesh.RecalculateBounds();
        voxelMesh.RecalculateNormals();
        voxelMesh.Optimize();

        meshFilter.mesh = voxelMesh;
        meshCollider.sharedMesh = voxelMesh;
    }

    private void VoxelGen() {
        VerticesAdd(VoxelSide.RIGHT);
        VerticesAdd(VoxelSide.LEFT);
        VerticesAdd(VoxelSide.TOP);
        VerticesAdd(VoxelSide.BOTTOM);
        VerticesAdd(VoxelSide.FRONT);
        VerticesAdd(VoxelSide.BACK);

        VerticesAdd(VoxelSide.RIGHT1);
        VerticesAdd(VoxelSide.LEFT1);
        VerticesAdd(VoxelSide.FRONT1);
        VerticesAdd(VoxelSide.BACK1);
    }

    private void VerticesAdd(VoxelSide side) {
        switch(side) {
            case VoxelSide.RIGHT: {
                vertices.Add(new Vector3(1, 0, 0));
                vertices.Add(new Vector3(1, 1, 0));
                vertices.Add(new Vector3(1, 1, 1));
                vertices.Add(new Vector3(1, 0, 1));
                break;
            }
            case VoxelSide.LEFT: {
                vertices.Add(new Vector3(0, 0, 1));
                vertices.Add(new Vector3(0, 1, 1));
                vertices.Add(new Vector3(0, 1, 0));
                vertices.Add(new Vector3(0, 0, 0));
                break;
            }
            case VoxelSide.TOP: {
                vertices.Add(new Vector3(0, 1, 0));
                vertices.Add(new Vector3(0, 1, 1));
                vertices.Add(new Vector3(1, 1, 1));
                vertices.Add(new Vector3(1, 1, 0));
                break;
            }
            case VoxelSide.BOTTOM: {
                vertices.Add(new Vector3(0, 0, 1));
                vertices.Add(new Vector3(0, 0, 0));
                vertices.Add(new Vector3(1, 0, 0));
                vertices.Add(new Vector3(1, 0, 1));
                break;
            }
            case VoxelSide.FRONT: {
                vertices.Add(new Vector3(1, 0, 1));
                vertices.Add(new Vector3(1, 1, 1));
                vertices.Add(new Vector3(0, 1, 1));
                vertices.Add(new Vector3(0, 0, 1));
                break;
            }
            case VoxelSide.BACK: {
                vertices.Add(new Vector3(0, 0, 0));
                vertices.Add(new Vector3(0, 1, 0));
                vertices.Add(new Vector3(1, 1, 0));
                vertices.Add(new Vector3(1, 0, 0));
                break;
            }

            /* OVERLAY */

            case VoxelSide.RIGHT1: {
                vertices.Add(new Vector3(1, 0, 0));
                vertices.Add(new Vector3(1, 1, 0));
                vertices.Add(new Vector3(1, 1, 1));
                vertices.Add(new Vector3(1, 0, 1));
                break;
            }
            case VoxelSide.LEFT1: {
                vertices.Add(new Vector3(0, 0, 1));
                vertices.Add(new Vector3(0, 1, 1));
                vertices.Add(new Vector3(0, 1, 0));
                vertices.Add(new Vector3(0, 0, 0));
                break;
            }
            case VoxelSide.FRONT1: {
                vertices.Add(new Vector3(1, 0, 1));
                vertices.Add(new Vector3(1, 1, 1));
                vertices.Add(new Vector3(0, 1, 1));
                vertices.Add(new Vector3(0, 0, 1));
                break;
            }
            case VoxelSide.BACK1: {
                vertices.Add(new Vector3(0, 0, 0));
                vertices.Add(new Vector3(0, 1, 0));
                vertices.Add(new Vector3(1, 1, 0));
                vertices.Add(new Vector3(1, 0, 0));
                break;
            }
        }

        TrianglesAdd();

        UVCoordinate(side);
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

    private void UVAdd(Vector2 textureCoordinate) {
        Vector2 offset = new Vector2(
            1,
            1
        );

        Vector2 textureSizeInTiles = new Vector2(
            16 + offset.x,
            16 + offset.y
        );
        
        float x = textureCoordinate.x + offset.x;
        float y = textureCoordinate.y + offset.y;

        float _x = 1.0f / textureSizeInTiles.x;
        float _y = 1.0f / textureSizeInTiles.y;

        y = (textureSizeInTiles.y - 1) - y;

        x *= _x;
        y *= _y;

        uv.Add(new Vector2(x, y));
        uv.Add(new Vector2(x, y + _y));
        uv.Add(new Vector2(x + _x, y + _y));
        uv.Add(new Vector2(x + _x, y));
    }

    private void UVCoordinate(VoxelSide side) {
        if(side == VoxelSide.TOP) {
            UVAdd(new Vector2(0, 0));
            return;
        }
        if(side == VoxelSide.BOTTOM) {
            UVAdd(new Vector2(2, 0));
            return;
        }
        if(
            side == VoxelSide.RIGHT1 ||
            side == VoxelSide.LEFT1 ||
            side == VoxelSide.FRONT1 ||
            side == VoxelSide.BACK1
        ) {
            UVAdd(new Vector2(6, 2));
            return;
        }

        UVAdd(new Vector2(3, 0));
    }
}
