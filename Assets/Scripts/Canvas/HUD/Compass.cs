using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour {
    [SerializeField] private Transform compass;
    [SerializeField] private Transform player;
    
    private void Start() {
        
    }

    private void Update() {
        float angle = player.rotation.eulerAngles.y;
        compass.localRotation = Quaternion.Euler(0.0f, 0.0f, -angle);
    }
}
