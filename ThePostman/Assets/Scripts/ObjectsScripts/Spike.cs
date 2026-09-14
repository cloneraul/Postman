using UnityEngine;

public class Spike : MonoBehaviour
{
    public int dano = 999;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(dano);
        }
        else
        {
            Debug.LogWarning("O Player não possui o componente PlayerHealth.");
        }
    }
}