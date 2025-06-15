/// PlayerHealth.cs
/// Manages the player's health system, including damage, death, respawn,
/// UI updates, and temporary invincibility.
/// 
using UnityEngine;
using TMPro;
using System.Collections;


public class PlayerHealth : MonoBehaviour
{
    /// Maximum health value the player can have.
    public int maxHealth = 100;

    /// Current health value of the player.
    public int currentHealth;

    /// UI text that displays current health.
    public TextMeshProUGUI healthText;

    /// UI text that shows a death message when the player dies.
    public TextMeshProUGUI deathMessageText;

    /// The transform where the player should respawn after death.
    public Transform respawnPoint;

    /// Delay before the player respawns after dying.
    public float respawnDelay = 2f;

    /// Whether the player is currently dead.
    private bool isDead = false;

    /// Whether the player is temporarily invincible.
    public bool isInvincible = false;

    /// Duration of invincibility after respawn.
    public float invincibilityDuration = 2f;

    /// Audio clip played when the player takes damage.
    public AudioClip damageSFX;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    /// Initializes health and UI at the start of the game.
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthText();
        if (deathMessageText != null)
            deathMessageText.enabled = false;
    }

    /// Reduces player health and checks for death.
    public void TakeDamage(int damage)
    {
        if (isDead || isInvincible) return;

        currentHealth -= damage;
        Debug.Log("Player took damage. Current health:" + currentHealth);
        if (damageSFX != null)
            AudioSource.PlayClipAtPoint(damageSFX, transform.position);
        UpdateHealthText();

        if (currentHealth <= 0)
        {
            Die();
        }

    }

    /// Triggers the player death sequence and starts the respawn process.
    void Die()
    {
        Debug.Log("Player has died.");
        isDead = true;

        if (deathMessageText != null)
            deathMessageText.enabled = true;

        Invoke(nameof(Respawn), respawnDelay);
    }

    // Update is called once per frame
    /// Respawns the player at a predefined point, resets health, and re-enables control.
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
    UpdateHealthText();

    if (deathMessageText != null)
            deathMessageText.enabled = false;

    Invoke(nameof(DisableInvincibility), invincibilityDuration);
}
    /// Disables temporary invincibility after a delay.
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

    /// Updates the on-screen health UI text.
    void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "Health:" + currentHealth + "/" + maxHealth;
        }
    }


}
