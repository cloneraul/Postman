using UnityEngine;

public class ExtraLife : MonoBehaviour
{
    public int quantidadeVidas = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        if (PlayerLives.Instance == null)
        {
            Debug.LogWarning("PlayerLives não foi encontrado.");
            return;
        }

        PlayerLives.Instance.GanharVida(quantidadeVidas);

        Destroy(gameObject);
    }
}