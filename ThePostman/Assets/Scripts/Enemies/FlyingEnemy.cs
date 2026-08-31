using System.Collections;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [Header("Detecção")]
    public float distanciaDeteccao = 8f;

    [Header("Movimentação")]
    public float velocidade = 4f;
    public float distanciaMinimaDoPlayer = 0.5f;

    [Header("Dano")]
    public int dano = 1;

    [Header("Gelo")]
    public int maximoCongelamentos = 3;

    private Transform player;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private bool congelado = false;
    private int vezesCongelado = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        GameObject jogador =
            GameObject.FindGameObjectWithTag("Player");

        if (jogador != null)
        {
            player = jogador.transform;
        }
    }

    private void FixedUpdate()
    {
        if (player == null)
        {
            return;
        }

        if (congelado)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        float distancia = Vector2.Distance(
            transform.position,
            player.position
        );

        if (distancia <= distanciaDeteccao &&
            distancia > distanciaMinimaDoPlayer)
        {
            Vector2 direcao =
                (player.position - transform.position).normalized;

            rb.linearVelocity = direcao * velocidade;

            VirarParaPlayer(direcao.x);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void VirarParaPlayer(float direcaoX)
    {
        if (spriteRenderer == null)
        {
            return;
        }

        if (direcaoX < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (direcaoX > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    public void Freeze(float tempo)
    {
        vezesCongelado++;

        Debug.Log(
            "Ataques de gelo no inimigo voador: " +
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

        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.cyan;
        }

        yield return new WaitForSeconds(tempo);

        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;
        }

        congelado = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth =
                collision.gameObject.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(dano);
            }
        }
    }
}