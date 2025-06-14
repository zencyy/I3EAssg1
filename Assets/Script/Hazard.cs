using UnityEngine;

public class Hazard : MonoBehaviour
{
    public int damagePerSecond = 10;
    private float damageTimer = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

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
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            damageTimer = 0f;
        }
    }
}
