using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkTileP : MonoBehaviour {
    private Mesh voxelMesh;

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private MeshCollider meshCollider;

    private List<Vector3> vertices = new List<Vector3>();
    private enum VoxelSide { RIGHT, LEFT, TOP, BACK }

    private List<int> triangles = new List<int>();
    private int vertexIndex;

    private List<Vector3> verticesCollider = new List<Vector3>();
    private List<int> trianglesCollider = new List<int>();
    private int vertexIndexCollider;

    private List<Vector2> uv = new List<Vector2>();

    public static Vector3 ChunkSizeInVoxels = new Vector3(16, 16, 16);

    private VoxelType[,,] voxelMap = new VoxelType[(int)ChunkSizeInVoxels.x, (int)ChunkSizeInVoxels.y, (int)ChunkSizeInVoxels.z];

    private VoxelType voxelType;

    private static List<ChunkTileP> chunkData = new List<ChunkTileP>();

    void Start() {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();

        chunkData.Add(this);

        VoxelMapGen();
    }

    void Update() {
        
    }

    public void SetBlock(Vector3 worldPos, VoxelType voxelType) {
        Vector3 localPos = worldPos - transform.position;

        int x = Mathf.FloorToInt(localPos.x);
        int y = Mathf.FloorToInt(localPos.y);
        int z = Mathf.FloorToInt(localPos.z);

        voxelMap[x, y, z] = voxelType;

        ChunkGen();
    }

    public VoxelType GetBlock(Vector3 worldPos) {
        Vector3 localPos = worldPos - transform.position;

        int x = Mathf.FloorToInt(localPos.x);
        int y = Mathf.FloorToInt(localPos.y);
        int z = Mathf.FloorToInt(localPos.z);

        if(
            x < 0 || x >= ChunkSizeInVoxels.x ||
            y < 0 || y >= ChunkSizeInVoxels.y ||
            z < 0 || z >= ChunkSizeInVoxels.z
        ) {
            Debug.LogError("Coordinates out of range");

            return default(VoxelType);
        }

        return voxelMap[x, y, z];
    }

    public static ChunkTileP GetChunk(Vector3 pos) {        
        for(int i = 0; i < chunkData.Count; i++) {            
            Vector3 chunkPos = chunkData[i].transform.position;

            if(
                pos.x < chunkPos.x || pos.x >= chunkPos.x + ChunkSizeInVoxels.x || 
                pos.y < chunkPos.y || pos.y >= chunkPos.y + ChunkSizeInVoxels.y || 
                pos.z < chunkPos.z || pos.z >= chunkPos.z + ChunkSizeInVoxels.z
            ) {
                continue;
            }

            return chunkData[i];
        }

        return null;
    }

    private void VoxelLayers(Vector3 offset) {
        int x = (int)offset.x;
        int y = (int)offset.y;
        int z = (int)offset.z;

        float _x = x + transform.position.x;
        float _y = y + transform.position.y;
        float _z = z + transform.position.z;

        _x += (World.WorldSizeInVoxels.x);
        //_y += (World.WorldSizeInVoxels.y);
        _z += (World.WorldSizeInVoxels.z);

        if(
            _y == 0 ||
            _y <= 4 && 
            Random.Range(0, 100) < 50            
        ) {
            voxelMap[x, y, z] = VoxelType.bedrock;
        }
        /*
        else if(
            _y >= 6 &&
            Noise.Perlin3D(_x * 0.05f, _y * 0.05f, _z * 0.05f) >= 0.5f &&
            _y < Noise.Perlin(_x, _z) - 5
        ) {
            voxelMap[x, y, z] = VoxelType.air;
        }
        */
        else if(_y < Noise.Perlin(_x, _z) - 4) {
            voxelMap[x, y, z] = VoxelType.stone;
        }
        else if(_y < Noise.Perlin(_x, _z)) {
            voxelMap[x, y, z] = VoxelType.dirt;
        }
        else if(_y == Noise.Perlin(_x, _z)) {
            voxelMap[x, y, z] = VoxelType.grass_block;
        }
        else {
            voxelMap[x, y, z] = VoxelType.air;
        }
    }

    private void VoxelMapGen() {
        for(int x = 0; x < ChunkSizeInVoxels.x; x++) {
            for(int y = 0; y < ChunkSizeInVoxels.y; y++) {
                //for(int z = 0; z < ChunkSizeInVoxels.z; z++) {
                    VoxelLayers(new Vector3(x, y, 0));
                //}
            }
        }

        ChunkGen();
    }

    private void ChunkGen() {
        voxelMesh = new Mesh();
        voxelMesh.name = "Chunk";

        vertices.Clear();
        triangles.Clear();
        uv.Clear();

        vertexIndex = 0;

        verticesCollider.Clear();
        trianglesCollider.Clear();

        vertexIndexCollider = 0;

        for(int x = 0; x < ChunkSizeInVoxels.x; x++) {
            for(int y = 0; y < ChunkSizeInVoxels.y; y++) {
                //for(int z = 0; z < ChunkSizeInVoxels.z; z++) {
                    if(voxelMap[x, y, 0] != 0) {
                        VoxelGen(new Vector3(x, y, 0));
                        VoxelColliderGen(new Vector3(x, y, 0));
                    }
                //}
            }
        }

        MeshGen();
        MeshColliderGen();
    }

    private void MeshGen() {
        voxelMesh.vertices = vertices.ToArray();
        voxelMesh.triangles = triangles.ToArray();
        voxelMesh.uv = uv.ToArray();

        voxelMesh.RecalculateBounds();
        voxelMesh.RecalculateNormals();
        voxelMesh.Optimize();

        meshFilter.mesh = voxelMesh;
    }

    private void VoxelGen(Vector3 offset) {
        int x = (int)offset.x;
        int y = (int)offset.y;
        int z = (int)offset.z;

        voxelType = voxelMap[x, y, z];

        VerticesAdd(VoxelSide.BACK, offset);
    }

    private void VerticesAdd(VoxelSide side, Vector3 offset) {
        switch(side) {
            case VoxelSide.BACK: {
                vertices.Add(new Vector3(0, 0, 0) + offset);
                vertices.Add(new Vector3(0, 1, 0) + offset);
                vertices.Add(new Vector3(1, 1, 0) + offset);
                vertices.Add(new Vector3(1, 0, 0) + offset);
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
        // STONE
        if(voxelType == VoxelType.stone) {
            UVAdd(new Vector2(1, 0));
        }

        // GRASS BLOCK
        if(voxelType == VoxelType.grass_block) {
            UVAdd(new Vector2(3, 0));
        }

        // DIRT
        if(voxelType == VoxelType.dirt) {
            UVAdd(new Vector2(2, 0));
        }

        // BEDROCK
        if(voxelType == VoxelType.bedrock) {
            UVAdd(new Vector2(1, 1));
        }
    }

    private void MeshColliderGen() {
        Mesh voxelMeshCollider = new Mesh();
        voxelMeshCollider.name = "Chunk Collider";

        voxelMeshCollider.vertices = verticesCollider.ToArray();
        voxelMeshCollider.triangles = trianglesCollider.ToArray();

        meshCollider.sharedMesh = voxelMeshCollider;
    }

    private void VoxelColliderGen(Vector3 offset) {
        VerticesColliderAdd(VoxelSide.RIGHT, offset);
        VerticesColliderAdd(VoxelSide.LEFT, offset);
        VerticesColliderAdd(VoxelSide.TOP, offset);
    }

    private void VerticesColliderAdd(VoxelSide side, Vector3 offset) {
        switch(side) {
            case VoxelSide.RIGHT: {
                verticesCollider.Add(new Vector3(1, 0, 0) + offset);
                verticesCollider.Add(new Vector3(1, 1, 0) + offset);
                verticesCollider.Add(new Vector3(1, 1, 1) + offset);
                verticesCollider.Add(new Vector3(1, 0, 1) + offset);
                break;
            }
            case VoxelSide.LEFT: {
                verticesCollider.Add(new Vector3(0, 0, 1) + offset);
                verticesCollider.Add(new Vector3(0, 1, 1) + offset);
                verticesCollider.Add(new Vector3(0, 1, 0) + offset);
                verticesCollider.Add(new Vector3(0, 0, 0) + offset);
                break;
            }
            case VoxelSide.TOP: {
                verticesCollider.Add(new Vector3(0, 1, 0) + offset);
                verticesCollider.Add(new Vector3(0, 1, 1) + offset);
                verticesCollider.Add(new Vector3(1, 1, 1) + offset);
                verticesCollider.Add(new Vector3(1, 1, 0) + offset);
                break;
            }
        }

        TrianglesColliderAdd();
    }

    private void TrianglesColliderAdd() {
        // Primeiro Tiangulo
        trianglesCollider.Add(0 + vertexIndexCollider);
        trianglesCollider.Add(1 + vertexIndexCollider);
        trianglesCollider.Add(2 + vertexIndexCollider);

        // Segundo Triangulo
        trianglesCollider.Add(0 + vertexIndexCollider);
        trianglesCollider.Add(2 + vertexIndexCollider);
        trianglesCollider.Add(3 + vertexIndexCollider);

        vertexIndexCollider += 4;
    }
}
