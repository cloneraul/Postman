using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int vida = 3;

    private void Start()
    {
        UIManager.AtualizarVida(vida);
    }

    public void TakeDamage(int dano)
    {
        vida -= dano;

        if (vida < 0)
        {
            vida = 0;
        }

        Debug.Log("Vida: " + vida);

        UIManager.AtualizarVida(vida);

        if (vida <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player morreu");

        Destroy(gameObject);
    }
}