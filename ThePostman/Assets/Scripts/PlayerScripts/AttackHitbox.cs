using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    public int dano = 1;
    public float tempoParaDesaparecer = 0.15f;

    private bool acertou = false;

    private void Start()
    {
        Destroy(gameObject, tempoParaDesaparecer);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (acertou)
        {
            return;
        }

        EnemyHealth inimigo = other.GetComponentInParent<EnemyHealth>();

        if (inimigo != null)
        {
            acertou = true;
            inimigo.TakeDamage(dano);
            Desaparecer();
            return;
        }

        SandwormBoss boss = other.GetComponentInParent<SandwormBoss>();

        if (boss != null)
        {
            acertou = true;
            boss.TakeDamage(dano);
            Desaparecer();
        }
    }

    private void Desaparecer()
    {
        Destroy(gameObject);
    }
}