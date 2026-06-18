using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float velocidade = 2f;

    [Header("Limites da Patrulha")]
    public Transform pontoEsquerda;
    public Transform pontoDireita;

    private Rigidbody2D rb;
    private bool indoDireita = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (indoDireita)
        {
            rb.linearVelocity = new Vector2(velocidade, rb.linearVelocity.y);

            if (transform.position.x >= pontoDireita.position.x)
            {
                indoDireita = false;
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        else
        {
            rb.linearVelocity = new Vector2(-velocidade, rb.linearVelocity.y);

            if (transform.position.x <= pontoEsquerda.position.x)
            {
                indoDireita = true;
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (pontoEsquerda != null && pontoDireita != null)
        {
            Gizmos.color = Color.red;

            Gizmos.DrawLine(
                pontoEsquerda.position,
                pontoDireita.position
            );

            Gizmos.DrawSphere(pontoEsquerda.position, 0.15f);
            Gizmos.DrawSphere(pontoDireita.position, 0.15f);
        }
    }
}