using System.Collections;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Movimentação")]
    public float velocidade = 2f;
    public float distanciaPatrulha = 5f;

    [Header("Gelo")]
    public int maximoCongelamentos = 3;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    private bool indoDireita = true;
    private bool congelado = false;

    private float velocidadeOriginal;
    private int vezesCongelado = 0;

    private float posicaoInicialX;
    private float limiteEsquerdo;
    private float limiteDireito;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        velocidadeOriginal = velocidade;

        posicaoInicialX = transform.position.x;

        limiteEsquerdo = posicaoInicialX - distanciaPatrulha;
        limiteDireito = posicaoInicialX + distanciaPatrulha;
    }

    private void FixedUpdate()
    {
        if (rb == null)
        {
            return;
        }

        if (congelado)
        {
            rb.linearVelocity = new Vector2(
                0,
                rb.linearVelocity.y
            );

            return;
        }

        if (indoDireita)
        {
            rb.linearVelocity = new Vector2(
                velocidade,
                rb.linearVelocity.y
            );

            if (transform.position.x >= limiteDireito)
            {
                indoDireita = false;

                if (sprite != null)
                {
                    sprite.flipX = true;
                }
            }
        }
        else
        {
            rb.linearVelocity = new Vector2(
                -velocidade,
                rb.linearVelocity.y
            );

            if (transform.position.x <= limiteEsquerdo)
            {
                indoDireita = true;

                if (sprite != null)
                {
                    sprite.flipX = false;
                }
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

        rb.linearVelocity = new Vector2(
            0,
            rb.linearVelocity.y
        );

        if (sprite != null)
        {
            sprite.color = Color.cyan;
        }

        yield return new WaitForSeconds(tempo);

        if (sprite != null)
        {
            sprite.color = Color.white;
        }

        congelado = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying)
        {
            float esquerda =
                transform.position.x - distanciaPatrulha;

            float direita =
                transform.position.x + distanciaPatrulha;

            Vector3 inicio = new Vector3(
                esquerda,
                transform.position.y,
                transform.position.z
            );

            Vector3 fim = new Vector3(
                direita,
                transform.position.y,
                transform.position.z
            );

            Gizmos.color = Color.red;

            Gizmos.DrawLine(inicio, fim);

            Gizmos.DrawSphere(inicio, 0.15f);
            Gizmos.DrawSphere(fim, 0.15f);
        }
    }
}