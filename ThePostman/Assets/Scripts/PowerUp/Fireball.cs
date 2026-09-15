using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float velocidade = 10f;
    public int direcao = 1;
    public int dano = 2;

    private void Start()
    {
        Destroy(gameObject, 3f);
    }

    private void Update()
    {
        transform.Translate(
            Vector2.right *
            direcao *
            velocidade *
            Time.deltaTime
        );
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth inimigo =
            other.GetComponentInParent<EnemyHealth>();

        if (inimigo != null)
        {
            inimigo.TakeDamage(dano);
            Destroy(gameObject);
            return;
        }

        SandwormBoss boss =
            other.GetComponentInParent<SandwormBoss>();

        if (boss != null)
        {
            boss.TakeDamage(dano);
            Destroy(gameObject);
        }
    }
}