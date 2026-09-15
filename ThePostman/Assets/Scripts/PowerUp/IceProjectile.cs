using UnityEngine;

public class IceProjectile : MonoBehaviour
{
    public float velocidade = 8f;
    public int direcao = 1;
    public float tempoCongelado = 3f;

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
        EnemyPatrol enemyTerrestre =
            other.GetComponent<EnemyPatrol>();

        if (enemyTerrestre == null)
        {
            enemyTerrestre =
                other.GetComponentInParent<EnemyPatrol>();
        }

        if (enemyTerrestre != null)
        {
            enemyTerrestre.Freeze(tempoCongelado);
            Destroy(gameObject);
            return;
        }

        FlyingEnemy enemyVoador =
            other.GetComponent<FlyingEnemy>();

        if (enemyVoador == null)
        {
            enemyVoador =
                other.GetComponentInParent<FlyingEnemy>();
        }

        if (enemyVoador != null)
        {
            enemyVoador.Freeze(tempoCongelado);
            Destroy(gameObject);
            return;
        }

        SandwormBoss boss =
            other.GetComponent<SandwormBoss>();

        if (boss == null)
        {
            boss =
                other.GetComponentInParent<SandwormBoss>();
        }

        if (boss != null)
        {
            boss.Freeze(tempoCongelado);
            Destroy(gameObject);
        }
    }
}