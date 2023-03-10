using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toolbar1 : MonoBehaviour {    
    //[SerializeField] private InterfaceManager interfaceManager;
    private bool openMenu;
    private bool openGameMenu;
    
    [Space(20)]
    [SerializeField] RectTransform highlight;
    //[SerializeField] ItemSlot[] itemSlots;
    [SerializeField] InventorySlot[] inventorySlots;

    int slotIndex = 0;
    
    void Start() {
        
    }

    void Update() {
        //openMenu = interfaceManager.openMenu;
        //openGameMenu = interfaceManager.openGameMenu;

        if(!openGameMenu && !openMenu) {
            KeyInputs();
            ScrollInputs();
        }
        
        highlight.position = inventorySlots[slotIndex].transform.position;
    }

    void KeyInputs() {
        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            slotIndex = 0;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)) {
            slotIndex = 1;
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)) {
            slotIndex = 2;
        }
        if(Input.GetKeyDown(KeyCode.Alpha4)) {
            slotIndex = 3;
        }
        if(Input.GetKeyDown(KeyCode.Alpha5)) {
            slotIndex = 4;
        }
        if(Input.GetKeyDown(KeyCode.Alpha6)) {
            slotIndex = 5;
        }
        if(Input.GetKeyDown(KeyCode.Alpha7)) {
            slotIndex = 6;
        }
        if(Input.GetKeyDown(KeyCode.Alpha8)) {
            slotIndex = 7;
        }
        if(Input.GetKeyDown(KeyCode.Alpha9)) {
            slotIndex = 8;
        }
        if(Input.GetKeyDown(KeyCode.Alpha0)) {
            slotIndex = 9;
        }
    }

    void ScrollInputs() {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        
        if(scroll != 0) {
            if(scroll > 0) {
                slotIndex--;
            }
            else {
                slotIndex++;
            }

            if(slotIndex > inventorySlots.Length - 1) {
                slotIndex = 0;
            }
            if(slotIndex < 0) {
                slotIndex = inventorySlots.Length - 1;
            }
        }
    }

    /*
    public VoxelType GetCurrentItem() {
        return inventorySlots[slotIndex].itemName;
    }
    */
}

/*
[System.Serializable]
public class ItemSlot {
    public VoxelType itemName;
    public Image icon;
}
*/
