using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour {
    [Header("Debug Screen")]
    [SerializeField] private GameObject debugScreen;
    [SerializeField] private bool openDebugScreen;

    [Header("Interface")]
    [SerializeField] private GameObject crosshair;
    [SerializeField] private GameObject toolbar;
    [SerializeField] private GameObject HUD;
    [SerializeField] private bool openInterface;
    
    [Header("Inventory")]
    [SerializeField] private GameObject inventory;
    public bool openMenu;
    
    [Header("Game Menu")]
    [SerializeField] private GameObject gameMenu;
    public bool openGameMenu;
    [SerializeField] private Button backToGameButton;
    [SerializeField] private Button saveAndQuitToTitle;
    
    private void Start() {
        debugScreen.SetActive(true);
        openDebugScreen = true;

        crosshair.SetActive(true);
        toolbar.SetActive(true);
        HUD.SetActive(true);
        openInterface = true;

        inventory.SetActive(false);

        gameMenu.SetActive(false);
        backToGameButton.onClick.AddListener(BacktoGame);
        saveAndQuitToTitle.onClick.AddListener(SaveAndQuitToTitle);
    }

    private void Update() {
        DebugScreen();

        if(!openGameMenu && !openMenu) {
            if(Input.GetKeyDown(KeyCode.F1)) {
                openInterface = !openInterface;

                Interface();
            }
        }

        Inventory();
        GameMenu();
    }

    private void DebugScreen() {
        if(Input.GetKeyDown(KeyCode.F3)) {            
            openDebugScreen = !openDebugScreen;

            debugScreen.SetActive(!debugScreen.activeSelf);
        }
    }

    private void Interface() {            
        crosshair.SetActive(!crosshair.activeSelf);
        toolbar.SetActive(!toolbar.activeSelf);
        HUD.SetActive(!HUD.activeSelf);

        if(openDebugScreen) {
            debugScreen.SetActive(!debugScreen.activeSelf);
        }       
    }

    private void Inventory() {
        if(!openGameMenu) {
            if(Input.GetKeyDown(KeyCode.E)) {
                openMenu = !openMenu;

                inventory.SetActive(!inventory.activeSelf);

                if(!openInterface) {
                    Interface();
                }
            }
            /*
            if(Input.GetButtonDown("Escape")) {
                openMenu = false;

                inventory.SetActive(false);
            }
            */
        }
    }

    private void GameMenu() {
        if(!openMenu) {
            if(Input.GetButtonDown("Escape")) {
                openGameMenu = !openGameMenu;

                gameMenu.SetActive(!gameMenu.activeSelf);
            }
        }
    }

    private void BacktoGame() {
        openGameMenu = false;

        gameMenu.SetActive(false);
    }

    private void SaveAndQuitToTitle() {
        Application.Quit();
    }
}
