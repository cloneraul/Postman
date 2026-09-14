using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Vida do Inimigo")]
    public int vidaMaxima = 3;
    public int vidaAtual = 3;

    private void Start()
    {
        vidaAtual = vidaMaxima;
    }

    public void TakeDamage(int dano)
    {
        vidaAtual -= dano;

        if (vidaAtual < 0)
        {
            vidaAtual = 0;
        }

        Debug.Log("Vida do inimigo: " + vidaAtual);

        if (vidaAtual <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Inimigo derrotado");

        Destroy(gameObject);
    }
}