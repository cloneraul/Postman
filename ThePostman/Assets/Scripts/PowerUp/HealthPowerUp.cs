using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{
    public int quantidadeCura = 2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.Heal(quantidadeCura);
            Destroy(gameObject);
        }
    }
}