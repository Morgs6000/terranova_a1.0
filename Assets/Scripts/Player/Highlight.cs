using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour {
    [SerializeField] private GameObject highlight;
        
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    
    private Mesh highlighMesh;

    private List<Vector3> vertices = new List<Vector3>();
    private enum HighlighSide { RIGHT, LEFT, TOP, BOTTOM, FRONT, BACK }

    private List<int> triangles = new List<int>();
    private int vertexIndex;
    
    private List<Vector2> uv = new List<Vector2>();
    [SerializeField] private Material material;

    [SerializeField] private Transform cam;
    private float rangeHit = 5.0f;
    [SerializeField] private LayerMask groundMask;
    
    private void Start() {
        meshFilter = (MeshFilter)highlight.AddComponent(typeof(MeshFilter));
        meshRenderer = (MeshRenderer)highlight.AddComponent(typeof(MeshRenderer));

        highlighMesh = new Mesh();
        highlighMesh.name = "Highlight";

        HighlighGen();
        MeshRenderer();
    }

    private void Update() {
        HighlightUpdates();
    }

    private void HighlightUpdates() {
        RaycastHit hit;

        if(Physics.Raycast(cam.position, cam.forward, out hit, rangeHit, groundMask)) {
            highlight.SetActive(true);

            Vector3 pointPos = hit.point - hit.normal / 2;
            
            highlight.transform.position = new Vector3(
                Mathf.FloorToInt(pointPos.x),
                Mathf.FloorToInt(pointPos.y),
                Mathf.FloorToInt(pointPos.z)
            );
        }
        else {
            highlight.SetActive(false);          
        }
    }

    private void HighlighGen() {
        VerticesAdd(HighlighSide.RIGHT);
        VerticesAdd(HighlighSide.LEFT);
        VerticesAdd(HighlighSide.TOP);
        VerticesAdd(HighlighSide.BOTTOM);
        VerticesAdd(HighlighSide.FRONT);
        VerticesAdd(HighlighSide.BACK);
    }

    private void MeshRenderer() {
        highlighMesh.vertices = vertices.ToArray();
        highlighMesh.triangles = triangles.ToArray();
        highlighMesh.uv = uv.ToArray();

        highlighMesh.RecalculateNormals();
        highlighMesh.Optimize();

        meshFilter.mesh = highlighMesh;
        meshRenderer.material = material;
    }

    private void VerticesAdd(HighlighSide side) {
        switch(side) {
            case HighlighSide.RIGHT: {
                vertices.Add(new Vector3(1, 0, 0));
                vertices.Add(new Vector3(1, 1, 0));
                vertices.Add(new Vector3(1, 1, 1));
                vertices.Add(new Vector3(1, 0, 1));
                break;
            }
            case HighlighSide.LEFT: {
                vertices.Add(new Vector3(0, 0, 1));
                vertices.Add(new Vector3(0, 1, 1));
                vertices.Add(new Vector3(0, 1, 0));
                vertices.Add(new Vector3(0, 0, 0));
                break;
            }
            case HighlighSide.TOP: {
                vertices.Add(new Vector3(0, 1, 0));
                vertices.Add(new Vector3(0, 1, 1));
                vertices.Add(new Vector3(1, 1, 1));
                vertices.Add(new Vector3(1, 1, 0));
                break;
            }
            case HighlighSide.BOTTOM: {
                vertices.Add(new Vector3(1, 0, 0));
                vertices.Add(new Vector3(1, 0, 1));
                vertices.Add(new Vector3(0, 0, 1));
                vertices.Add(new Vector3(0, 0, 0));
                break;
            }
            case HighlighSide.FRONT: {
                vertices.Add(new Vector3(1, 0, 1));
                vertices.Add(new Vector3(1, 1, 1));
                vertices.Add(new Vector3(0, 1, 1));
                vertices.Add(new Vector3(0, 0, 1));
                break;
            }
            case HighlighSide.BACK: {
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
            0, 
            0
        );

        Vector2 textureSizeInTiles = new Vector2(
            1 + offset.x,
            1 + offset.y
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

    private void UVCoordinate(HighlighSide side) {
        UVAdd(new Vector2(0, 0));
    }
}
