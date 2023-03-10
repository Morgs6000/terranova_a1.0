using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour {
    [SerializeField] private Transform point;
    //[SerializeField] private Transform minimap;
    //[SerializeField] private Transform[] cardinalPoints;
    [SerializeField] private Transform player;
    
    void Start() {
        
    }

    void Update() {
        /*
        float angle = player.rotation.eulerAngles.y;

        foreach(Transform cardinalPoint in cardinalPoints) {
            cardinalPoint.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        }

        minimap.localRotation = Quaternion.Euler(0.0f, 0.0f, angle);
        */

        float angle = player.rotation.eulerAngles.y;
        point.localRotation = Quaternion.Euler(0.0f, 0.0f, -angle);
    }
}
