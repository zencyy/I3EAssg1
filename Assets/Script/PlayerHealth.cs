using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;


public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Slider healthBar;
    public TextMeshProUGUI deathMessageText;

    public Transform respawnPoint;
    public float respawnDelay = 2f;

    private bool isDead = false;
    public bool isInvincible = false;
    public float invincibilityDuration = 2f;


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
        if (isDead || isInvincible) return;

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

    Debug.Log("RESPAWNING...");

    Transform capsule = null;
    foreach (Transform t in GetComponentsInChildren<Transform>(true))
    {
        if (t.name == "PlayerCapsule")
        {
            capsule = t;
            break;
        }
    }
    if (capsule == null)
    {
        Debug.LogError("PlayerCapsule NOT FOUND — check name in hierarchy!");
        return;
    }

    if (capsule != null)
        {
            var cc = capsule.GetComponent<CharacterController>();
            var fpc = capsule.GetComponent<StarterAssets.FirstPersonController>();
            var input = capsule.GetComponent<StarterAssets.StarterAssetsInputs>();

            if (input != null) input.enabled = false;
            if (fpc != null) fpc.enabled = false;
            if (cc != null) cc.enabled = false;

            capsule.position = respawnPoint.position;
            Debug.Log("Teleported to: " + respawnPoint.position);

            StartCoroutine(ReEnableControllerAfterDelay(cc, fpc, input, 0.2f));
        }
        else
        {
            Debug.LogError("PlayerCapsule not found.");
        }

    currentHealth = maxHealth;
    isDead = false;
    isInvincible = true;

    if (healthBar != null)
        healthBar.value = currentHealth;

    if (deathMessageText != null)
        deathMessageText.enabled = false;

    Invoke(nameof(DisableInvincibility), invincibilityDuration);
}
    void DisableInvincibility()
    {
    isInvincible = false;
    Debug.Log("Invincibility ended.");
    }
    private IEnumerator ReEnableControllerAfterDelay(CharacterController cc, StarterAssets.FirstPersonController fpc, StarterAssets.StarterAssetsInputs input, float delay)
{
    yield return new WaitForSeconds(delay);

    if (cc != null) cc.enabled = true;
    if (fpc != null) fpc.enabled = true;
    if (input != null) input.enabled = true;

    Debug.Log("Controller and input re-enabled after teleport.");
}
}
