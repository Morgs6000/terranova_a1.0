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
        HealthUpdateBar();
        HealthUpdateText();

        AddHelth();
        RemoveHealth();
    }

    private void HealthUpdateBar() {
        barHealth.fillAmount = currentHealth / maxHealth;        
    }

    private void HealthUpdateText() {
        currentHealthText.text = currentHealth.ToString("F0");

        if(currentHealth <= maxHealth) {
            currentHealthText.color = Color.green;
        }
        if(currentHealth <= (maxHealth * 0.75f)) {
            currentHealthText.color = Color.yellow;
        }
        if(currentHealth <= (maxHealth * 0.50)) {
            currentHealthText.color = new Color(1.0f, 0.5f, 0.0f);
        }
        if(currentHealth <= (maxHealth * 0.25f)) {
            currentHealthText.color = Color.red;
        }
    }

    private void AddHelth() {
        if(currentHealth < maxHealth) {
            // Se a barra de FOME estiver pelo menos 75% cheia.
            // +1 HP a cada 0,5 segundo.
            if(hunger.currentHunger >= (hunger.maxHunger * 0.75f)) {
                tempoDecorrido += Time.deltaTime;

                if(tempoDecorrido >= 0.5f) {
                    currentHealth++;
                    tempoDecorrido = 0;
                }
                if(currentHealth == maxHealth) {
                    tempoDecorrido = 0;
                }
            }
            // Se a barra de FOME estiver estiver pelo menos 25% cheia.
            // +1 HP a cada 4 segundos.
            if(hunger.currentHunger >= (hunger.maxHunger * 0.25f)) {
                tempoDecorrido += Time.deltaTime;
                
                if(tempoDecorrido >= 4.0f) {
                    currentHealth++;
                    tempoDecorrido = 0;
                }
                if(currentHealth == maxHealth) {
                    tempoDecorrido = 0;
                }
            }
            
            // Na dificuldade Facil, a saúde do jogador para de cair em 50%,
            // no Normal, para em 5%,
            // e no Dificil, continua drenando até que o jogador coma alguma coisa ou morra de fome.
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
                if(currentHealth == 0) {
                    tempoDecorrido = 0;
                }
            }            
        }        
    }
}
