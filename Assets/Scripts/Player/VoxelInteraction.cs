using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelInteraction : MonoBehaviour {
    [SerializeField] private Transform player;

    [SerializeField] private Transform cam;
    private float rangeHit = 5.0f;
    [SerializeField] private LayerMask groundMask;

    [SerializeField] private Toolbar toolbar;

    [SerializeField] private float currentTime;

    [SerializeField] private InterfaceManager interfaceManager;
    private bool openMenu;
    private bool openGameMenu;
    
    void Start() {
        
    }

    void Update() {
        openMenu = interfaceManager.openMenu;
        openGameMenu = interfaceManager.openGameMenu;
             
        if(!openGameMenu && !openMenu) {
            RaycastUpdate();
        }
    }

    private void RaycastUpdate() {
        RaycastHit hit;

        if(Physics.Raycast(cam.position, cam.forward, out hit, rangeHit, groundMask)) {
            Destroy(hit);
            Add(hit);
        }
    }

    private void Destroy(RaycastHit hit) {
        if(Input.GetMouseButtonDown(0)) {
            Vector3 pointPos = hit.point - hit.normal / 2;

            Chunk c = Chunk.GetChunk(new Vector3(
                Mathf.FloorToInt(pointPos.x),
                Mathf.FloorToInt(pointPos.y),
                Mathf.FloorToInt(pointPos.z)
            ));

            Debug.Log("Voxel type destroyed: " + c.GetBlock(pointPos).ToString());

            c.SetBlock(pointPos, VoxelType.air);
        }
    }

    private void Add(RaycastHit hit) {
        if(Input.GetMouseButton(1)) {
            currentTime += Time.deltaTime;

            if(currentTime >= 0.25f) {
                Vector3 pointPos = hit.point + hit.normal / 2;

                //*
                float distance = 0.81f;
                float playerDistance = Vector3.Distance(player.position, pointPos);
                float camDistance = Vector3.Distance(cam.position, pointPos);

                if(playerDistance < distance || camDistance < distance) {
                    return;
                }
                if(player.position.y - 1 > World.WorldSizeInVoxels.y) {
                    return;
                }
                //*/
                
                Chunk c = Chunk.GetChunk(new Vector3(
                    Mathf.FloorToInt(pointPos.x),
                    Mathf.FloorToInt(pointPos.y),
                    Mathf.FloorToInt(pointPos.z)
                ));

                c.SetBlock(pointPos, toolbar.GetCurrentItem()); 

                currentTime = 0.0f;
            }
        }
        else {
            currentTime = 0.25f;
        }
    }
}
