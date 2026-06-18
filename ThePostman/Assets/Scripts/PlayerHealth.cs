using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int vida = 3;

    public void TakeDamage(int dano)
    {
        vida -= dano;

        Debug.Log("Vida: " + vida);

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