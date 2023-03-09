using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Coin : MonoBehaviour {    
    [SerializeField] private int currentCoin;

    [SerializeField] private TextMeshProUGUI currentCoinText;
    [SerializeField] private Image imageCoins;
    [SerializeField] private Sprite[] spriteCoins;
    
    private void Start() {
        
    }

    private void Update() {
        CoinUpdateText();
        CoinUpdateSprite();
    }

    private void CoinUpdateText() {
        if(currentCoin == 0) {
            currentCoinText.text = null;
        }
        if(currentCoin >= 1) {
            currentCoinText.text = currentCoin.ToString();
            currentCoinText.color = Color.yellow;
        }
        if(currentCoin >= 1000) {
            currentCoinText.text = (currentCoin / 1000).ToString() + " K";
            currentCoinText.color = Color.white;
        }
        if(currentCoin >= 1000000) {
            currentCoinText.text = (currentCoin / 1000000).ToString() + " M";
            currentCoinText.color = Color.green;
        }
        if(currentCoin >= 1000000000) {
            currentCoinText.text = (currentCoin / 1000000000).ToString() + " B";
            currentCoinText.color = Color.cyan;
        }
    }

    private void CoinUpdateSprite() {
        if(currentCoin == 0) {
            imageCoins.sprite = spriteCoins[0];
        }
        if(currentCoin == 1) {
            imageCoins.sprite = spriteCoins[1];
        }
        if(currentCoin == 2) {
            imageCoins.sprite = spriteCoins[2];
        }
        if(currentCoin == 3) {
            imageCoins.sprite = spriteCoins[3];
        }
        if(currentCoin == 4) {
            imageCoins.sprite = spriteCoins[4];
        }
        if(currentCoin >= 5) {
            imageCoins.sprite = spriteCoins[5];
        }
        if(currentCoin >= 25) {
            imageCoins.sprite = spriteCoins[6];
        }
        if(currentCoin >= 100) {
            imageCoins.sprite = spriteCoins[7];
        }
        if(currentCoin >= 250) {
            imageCoins.sprite = spriteCoins[8];
        }
        if(currentCoin >= 1000) {
            imageCoins.sprite = spriteCoins[9];
        }
        if(currentCoin >= 10000) {
            imageCoins.sprite = spriteCoins[10];
        }
    }
}
