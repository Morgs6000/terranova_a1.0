using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using TMPro;

public class Hunger : MonoBehaviour {
    [SerializeField] private int maxHunger;
    [SerializeField] public float currentHunger;
    
    [SerializeField] private Image barHunger;
    //[SerializeField] private TextMeshProUGUI currentHungerText;

    private void Start() {
        maxHunger = 20;
        currentHunger = maxHunger;
    }

    private void Update() {
        HungerUpdate();
    }

    private void HungerUpdate() {
        barHunger.fillAmount = currentHunger / maxHunger;
        //currentHungerText.text = maxHunger + " / " + currentHunger.ToString("F0");
    }
}
