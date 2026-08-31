using System.Collections;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Movimentação")]
    public float velocidade = 2f;

    [Header("Limites da Patrulha")]
    public Transform pontoEsquerda;
    public Transform pontoDireita;

    [Header("Gelo")]
    public int maximoCongelamentos = 3;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    private bool indoDireita = true;
    private bool congelado = false;

    private float velocidadeOriginal;
    private int vezesCongelado = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        velocidadeOriginal = velocidade;
    }

    private void FixedUpdate()
    {
        if (congelado)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            return;
        }

        if (indoDireita)
        {
            rb.linearVelocity = new Vector2(
                velocidade,
                rb.linearVelocity.y
            );

            if (transform.position.x >= pontoDireita.position.x)
            {
                indoDireita = false;
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        else
        {
            rb.linearVelocity = new Vector2(
                -velocidade,
                rb.linearVelocity.y
            );

            if (transform.position.x <= pontoEsquerda.position.x)
            {
                indoDireita = true;
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    public void Freeze(float tempo)
    {
        vezesCongelado++;

        Debug.Log(
            "Ataques de gelo: " +
            vezesCongelado +
            "/" +
            maximoCongelamentos
        );

        if (vezesCongelado >= maximoCongelamentos)
        {
            Destroy(gameObject);
            return;
        }

        if (!congelado)
        {
            StartCoroutine(Congelar(tempo));
        }
    }

    private IEnumerator Congelar(float tempo)
    {
        congelado = true;

        velocidade = 0;

        if (sprite != null)
        {
            sprite.color = Color.cyan;
        }

        yield return new WaitForSeconds(tempo);

        velocidade = velocidadeOriginal;

        if (sprite != null)
        {
            sprite.color = Color.white;
        }

        congelado = false;
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

            Gizmos.DrawSphere(
                pontoEsquerda.position,
                0.15f
            );

            Gizmos.DrawSphere(
                pontoDireita.position,
                0.15f
            );
        }
    }
}