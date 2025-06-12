using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;  
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player took damage. Current health:" + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
        
    }
    void Die()
    {
        Debug.Log("Player has died.");
        // You can add respawn, reload scene, or disable controls here
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
