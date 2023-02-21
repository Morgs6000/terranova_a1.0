using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorldTileTD : MonoBehaviour {
    //[SerializeField] private GameObject mainCamera;
    //[SerializeField] private GameObject loadingScreen;

    //[SerializeField] private Image loadBar;
    //[SerializeField] private TextMeshProUGUI loadPercent;
    //private float loadAmount;
    //private float maxLoadAmount;
    
    public static Vector3 WorldSizeInVoxels = new Vector3(256, 256, 256);
    //public static Vector3 WorldSizeInVoxels = new Vector3(32, 256, 32);

    private Vector3 WorldSizeInChunks = new Vector3(
        WorldSizeInVoxels.x / Chunk.ChunkSizeInVoxels.x,
        WorldSizeInVoxels.y / Chunk.ChunkSizeInVoxels.y,
        WorldSizeInVoxels.z / Chunk.ChunkSizeInVoxels.z
    );

    [SerializeField] private GameObject chunkPrefab;
    //[SerializeField] private GameObject player;

    //private int viewDistance = 5;

    void Start() {
        //mainCamera.SetActive(true);
        //loadingScreen.SetActive(true);
        //player.SetActive(false);

        //loadAmount = 0;
        //maxLoadAmount = (WorldSizeInChunks.x * WorldSizeInChunks.y * WorldSizeInChunks.z);

        StartCoroutine(WorldGen());
    }

    void Update() {
        //StartCoroutine(LoadingChunks());
    }

    private IEnumerator WorldGen() {
        Vector3Int WorldSize = new Vector3Int(
            (int)WorldSizeInChunks.x / 2,
            (int)WorldSizeInChunks.y,            
            (int)WorldSizeInChunks.z / 2            
        );
        
        for(int x = -WorldSize.x; x < WorldSize.x; x++) {
            for(int z = -WorldSize.z; z < WorldSize.z; z++) {
                //for(int y = 0; y < WorldSize.y; y++) {
                    InstantiateChunk(new Vector3(x, 0, z));

                    //LoadProgress();
                //}

                yield return null;
            }
        }

        //SetPlayerSpawn();

        //mainCamera.SetActive(false);
        //loadingScreen.SetActive(false);
    }

    private void InstantiateChunk(Vector3 chunkPos) {
        int x = (int)chunkPos.x;
        int y = (int)chunkPos.y;
        int z = (int)chunkPos.z;
        
        Vector3 chunkOffset = new Vector3(
            x * Chunk.ChunkSizeInVoxels.x,
            y * Chunk.ChunkSizeInVoxels.y,
            z * Chunk.ChunkSizeInVoxels.z
        );
        
        Chunk c = Chunk.GetChunk(new Vector3(
            Mathf.FloorToInt(chunkOffset.x),
            Mathf.FloorToInt(chunkOffset.y),
            Mathf.FloorToInt(chunkOffset.z)
        ));

        if(c == null) {
            GameObject chunk = Instantiate(chunkPrefab);
            chunk.transform.position = chunkOffset;
            chunk.transform.SetParent(transform);
            chunk.name = "Chunk: " + x + ", " + z;
        }
    }

    private void LoadProgress() {
        //loadAmount++;
        //loadBar.fillAmount = loadAmount / maxLoadAmount;
        //loadPercent.text = ((loadAmount / maxLoadAmount) * 100).ToString("F0") + "%";
    }

    private void SetPlayerSpawn() {
        Vector3 spawnPosition = new Vector3(
            0,
            WorldSizeInVoxels.y,
            0
        );

        //player.transform.position = spawnPosition;

        //player.SetActive(true);
    }

    /*
    private IEnumerator LoadingChunks() {
        int posX = Mathf.FloorToInt(player.transform.position.x / Chunk.ChunkSizeInVoxels.x);
        int posZ = Mathf.FloorToInt(player.transform.position.z / Chunk.ChunkSizeInVoxels.z);

        for(int x = -viewDistance; x < viewDistance; x++) {
            for(int z = -viewDistance; z < viewDistance; z++) {                    
                for(int y = 0; y < WorldSizeInChunks.y; y++) {
                    InstantiateChunk(new Vector3(x, y, z));                    
                }

                yield return null;
            }
        }
    }
    */
}
