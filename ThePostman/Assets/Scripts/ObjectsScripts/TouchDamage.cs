using UnityEngine;

public class TouchDamage : MonoBehaviour
{
    [Header("Dano")]
    public int dano = 1;

    [Header("Knockback")]
    public float forcaKnockback = 5f;
    public float forcaKnockbackVertical = 3f;

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

        Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();

        if (playerRb != null)
        {
            Vector2 direcao = (other.transform.position - transform.position).normalized;

            if (direcao.x > 0)
            {
                direcao.x = 1f;
            }
            else
            {
                direcao.x = -1f;
            }

            direcao.y = 0;

            playerRb.linearVelocity = new Vector2(
                direcao.x * forcaKnockback,
                forcaKnockbackVertical
            );
        }
    }
}