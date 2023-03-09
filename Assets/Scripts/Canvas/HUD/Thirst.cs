using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Thirst : MonoBehaviour {
    [SerializeField] private int maxThirst;
    [SerializeField] public float currentThirst;
    
    [SerializeField] private Image barThirst;
    [SerializeField] private TextMeshProUGUI currentThirstText;

    private void Start() {
        maxThirst = 20;
        currentThirst = maxThirst;
    }

    private void Update() {
        ThirstUpdateBar();
        ThirstUpdateText();
    }

    private void ThirstUpdateBar() {
        barThirst.fillAmount = currentThirst / maxThirst;
    }

    private void ThirstUpdateText() {
        currentThirstText.text = currentThirst.ToString("F0");

        if(currentThirst <= maxThirst) {
            currentThirstText.color = Color.green;
        }
        if(currentThirst <= (maxThirst * 0.75f)) {
            currentThirstText.color = Color.yellow;
        }
        if(currentThirst <= (maxThirst * 0.50)) {
            currentThirstText.color = new Color(1.0f, 0.5f, 0.0f);
        }
        if(currentThirst <= (maxThirst * 0.25f)) {
            currentThirstText.color = Color.red;
        }
    }
}
