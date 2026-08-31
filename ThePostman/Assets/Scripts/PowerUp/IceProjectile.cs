using UnityEngine;

public class IceProjectile : MonoBehaviour
{
    public float velocidade = 8f;
    public int direcao = 1;

    private void Update()
    {
        transform.Translate(
            Vector2.right *
            direcao *
            velocidade *
            Time.deltaTime
        );
    }

    private void Start()
    {
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyPatrol enemyTerrestre =
                other.GetComponent<EnemyPatrol>();

            if (enemyTerrestre != null)
            {
                enemyTerrestre.Freeze(3f);
            }

            FlyingEnemy enemyVoador =
                other.GetComponent<FlyingEnemy>();

            if (enemyVoador != null)
            {
                enemyVoador.Freeze(3f);
            }

            Destroy(gameObject);
        }
    }
}