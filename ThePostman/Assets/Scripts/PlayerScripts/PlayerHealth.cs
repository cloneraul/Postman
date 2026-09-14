using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int vida = 3;
    public int vidaMaxima = 3;

    private bool morreu = false;

    private void Start()
    {
        vida = vidaMaxima;
        UIManager.AtualizarVida(vida);
    }

    public void TakeDamage(int dano)
    {
        if (morreu)
        {
            return;
        }

        vida -= dano;

        if (vida < 0)
        {
            vida = 0;
        }

        Debug.Log("Vida do Player: " + vida);

        UIManager.AtualizarVida(vida);

        if (vida <= 0)
        {
            Die();
        }
    }

    public void Heal(int quantidade)
    {
        if (morreu)
        {
            return;
        }

        vida += quantidade;

        if (vida > vidaMaxima)
        {
            vida = vidaMaxima;
        }

        Debug.Log("Vida recuperada: " + vida);

        UIManager.AtualizarVida(vida);
    }

    private void Die()
    {
        if (morreu)
        {
            return;
        }

        morreu = true;

        Debug.Log("Player morreu");

        if (PlayerLives.Instance != null)
        {
            PlayerLives.Instance.PerderVida();
        }
        else
        {
            Debug.LogWarning("PlayerLives não foi encontrado na cena.");
        }

        Destroy(gameObject);
    }
}