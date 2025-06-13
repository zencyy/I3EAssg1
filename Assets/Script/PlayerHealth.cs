using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Slider healthBar;
    public TextMeshProUGUI deathMessageText;

    public Transform respawnPoint;
    public float respawnDelay = 2f;

    private bool isDead = false;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
        if (deathMessageText != null)
            deathMessageText.enabled = false;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log("Player took damage. Current health:" + currentHealth);
        if (healthBar != null)
            healthBar.value = currentHealth;
    
        if (currentHealth <= 0)
        {
            Die();
        }
        
    }
    void Die()
    {
        Debug.Log("Player has died.");
        isDead = true;

        if (deathMessageText != null)
            deathMessageText.enabled = true;

        Invoke(nameof(Respawn), respawnDelay);  
    }

    // Update is called once per frame
    void Respawn()
    {
        if (respawnPoint != null)
        {
            transform.position = respawnPoint.position;
            Debug.Log("Player respawned.");
        }

        // Reset health
        currentHealth = maxHealth;
        isDead = false;

        if (healthBar != null)
            healthBar.value = currentHealth;

        if (deathMessageText != null)
            deathMessageText.enabled = false;
    }
}
