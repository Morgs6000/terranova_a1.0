using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour {
    [SerializeField] private int maxHealth;
    [SerializeField] private float currentHealth;
    
    [SerializeField] private Image barHealth;
    [SerializeField] private TextMeshProUGUI currentHealthText;

    [SerializeField] private Hunger hunger;

    [SerializeField] private float tempoDecorrido = 0;
    
    private void Start() {
        maxHealth = 20;
        currentHealth = maxHealth;
    }

    private void Update() {
        HealthUpdate();

        AddHelth();
        RemoveHealth();
    }

    private void HealthUpdate() {
        barHealth.fillAmount = currentHealth / maxHealth;
        currentHealthText.text = maxHealth + " / " + currentHealth.ToString("F0") + " HP";
    }

    private void AddHelth() {
        if(currentHealth < maxHealth) {
            // Se a barra de FOME estiver em 20 e ainda há saturação.
            // +1 HP a cada 0,5 segundo.
            if(hunger.currentHunger == 20) {
                tempoDecorrido += Time.deltaTime;

                if(tempoDecorrido >= 0.5f) {
                    currentHealth++;
                    tempoDecorrido = 0;
                }
            }
            // Se a barra de FOME estiver em 18.
            // +1 HP a cada 4 segundos.
            else if(hunger.currentHunger >= 18) {
                tempoDecorrido += Time.deltaTime;
                
                if(tempoDecorrido >= 4.0f) {
                    currentHealth++;
                    tempoDecorrido = 0;
                }
            }
            /*
            else {
                tempoDecorrido = 0;
            }
            */
        }
    }

    private void RemoveHealth() {
        if(currentHealth > 0) {
            // Se a barra de FOME estiver em 0.
            // -1 HP a cada 4 segundos.            
            if(hunger.currentHunger == 0) {
                tempoDecorrido += Time.deltaTime;

                // Se o tempo decorrido for maior ou igual a 4 segundos, chama o método GanharVida() e reseta o contador
                if(tempoDecorrido >= 4.0f) {
                    currentHealth--;
                    tempoDecorrido = 0;
                }
            }
        }
    }
}
