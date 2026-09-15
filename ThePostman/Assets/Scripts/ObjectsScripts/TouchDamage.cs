using UnityEngine;

public class TouchDamage : MonoBehaviour
{
    [Header("Dano")]
    public int dano = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(dano, transform.position);
        }
    }
}