using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Hunger : MonoBehaviour {
    [SerializeField] public int maxHunger;
    [SerializeField] public float currentHunger;
    
    [SerializeField] private Image barHunger;
    [SerializeField] private TextMeshProUGUI currentHungerText;

    private void Start() {
        maxHunger = 20;
        currentHunger = maxHunger;
    }

    private void Update() {
        HungerUpdateBar();
        HungerUpdateText();
    }

    private void HungerUpdateBar() {
        barHunger.fillAmount = currentHunger / maxHunger;
    }

    private void HungerUpdateText() {
        currentHungerText.text = currentHunger.ToString("F0");

        if(currentHunger <= maxHunger) {
            currentHungerText.color = Color.green;
        }
        if(currentHunger <= (maxHunger * 0.75f)) {
            currentHungerText.color = Color.yellow;
        }
        if(currentHunger <= (maxHunger * 0.50)) {
            currentHungerText.color = new Color(1.0f, 0.5f, 0.0f);
        }
        if(currentHunger <= (maxHunger * 0.25f)) {
            currentHungerText.color = Color.red;
        }
    }
}
