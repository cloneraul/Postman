using UnityEngine;

public class IcePowerUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPowerUp power = other.GetComponent<PlayerPowerUp>();

            if (power != null)
            {
                power.temPoderDeFogo = false;
                power.temPoderDeGelo = true;
            }

            Destroy(gameObject);
        }
    }
}