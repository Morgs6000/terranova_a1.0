using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using TMPro;

public class Thirst : MonoBehaviour {
    [SerializeField] private int maxThirst;
    [SerializeField] public float currentThirst;
    
    [SerializeField] private Image barThirst;
    //[SerializeField] private TextMeshProUGUI currentThirstText;

    private void Start() {
        maxThirst = 20;
        currentThirst = maxThirst;
    }

    private void Update() {
        ThirstUpdate();
    }

    private void ThirstUpdate() {
        barThirst.fillAmount = currentThirst / maxThirst;
        //currentThirstText.text = maxThirst + " / " + currentThirst.ToString("F0");
    }
}
