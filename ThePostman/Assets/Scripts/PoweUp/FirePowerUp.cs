using UnityEngine;

public class FirePowerUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPowerUp powerUp =
                other.GetComponent<PlayerPowerUp>();

            if (powerUp != null)
            {
                powerUp.temPoderDeFogo = true;
            }

            Destroy(gameObject);
        }
    }
}