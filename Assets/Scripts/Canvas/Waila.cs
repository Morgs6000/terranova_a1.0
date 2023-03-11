using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Waila : MonoBehaviour {
    [SerializeField] private GameObject waila;
    [SerializeField] private TextMeshProUGUI wailaText;

    private string text;

    [SerializeField] private Transform cam;
    private float rangeHit = 5.0f;
    [SerializeField] private LayerMask groundMask;
    
    private void Start() {
        
    }

    private void Update() {
        RaycastHit hit;

        if(Physics.Raycast(cam.position, cam.forward, out hit, rangeHit, groundMask)) {
            Vector3 pointPos = hit.point - hit.normal / 2;

            Chunk c = Chunk.GetChunk(new Vector3(
                Mathf.FloorToInt(pointPos.x),
                Mathf.FloorToInt(pointPos.y),
                Mathf.FloorToInt(pointPos.z)
            ));

            text = c.GetBlock(pointPos).ToString();
            wailaText.text = text;

            waila.SetActive(true);
        }
        else {
            waila.SetActive(false);          
        }
    }
}
