using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    [Header("Dano")]
    public int dano = 1;

    [Header("Duração do Ataque")]
    public float tempoDeAtaque = 0.15f;

    private void Start()
    {
        Destroy(gameObject, tempoDeAtaque);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(dano);
        }
    }
}