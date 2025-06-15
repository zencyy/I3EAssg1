/// Hazard.cs
/// Handles damage logic when the player is inside a hazard zone.
/// Applies damage immediately upon entry and every second while inside.
/// Handles damage logic when the player is inside a hazard zone.
/// Applies damage immediately upon entry and every second while inside.

using UnityEngine;

public class Hazard : MonoBehaviour
{
    /// Amount of damage applied per second while the player is in the hazard.
    public int damagePerSecond = 10;

    /// Internal timer used to track time between damage applications.
    private float damageTimer = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    /// Called when the player first enters the hazard area.
    /// Immediately applies one instance of damage.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player ENTERED hazard");
            PlayerHealth health = other.GetComponentInParent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(damagePerSecond);
            }
        }
    }
    /// Called once per frame while the player remains in the hazard area.
    /// Applies damage once every full second.
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player is in hazard zone");
            damageTimer += Time.deltaTime;

            if (damageTimer >= 1f)
            {
                PlayerHealth health = other.GetComponentInParent<PlayerHealth>();
                if (health != null)
                {
                    health.TakeDamage(damagePerSecond);
                }
                damageTimer = 0f;
            }
        }
    }
    /// Called when the player exits the hazard zone.
    /// Resets the damage timer to prevent carryover.
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            damageTimer = 0f;
        }
    }
}
