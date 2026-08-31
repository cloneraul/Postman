using System.Collections;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [Header("Detecção")]
    public float distanciaDeteccao = 8f;

    [Header("Movimentação")]
    public float velocidade = 4f;
    public float distanciaMinimaDoPlayer = 0.5f;

    [Header("Movimento Aleatório")]
    public float distanciaMovimentoAleatorio = 3f;
    public float tempoParaNovoDestino = 2f;

    [Header("Dano")]
    public int dano = 1;

    [Header("Gelo")]
    public int maximoCongelamentos = 3;

    private Transform player;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private bool congelado = false;
    private int vezesCongelado = 0;

    private Vector2 destinoAleatorio;
    private float tempoDestino;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        GameObject jogador = GameObject.FindGameObjectWithTag("Player");

        if (jogador != null)
        {
            player = jogador.transform;
        }

        EscolherNovoDestino();
    }

    private void FixedUpdate()
    {
        if (player == null)
        {
            rb.linearVelocity = Vector2.zero;
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

        if (distancia <= distanciaDeteccao)
        {
            PerseguirPlayer();
        }
        else
        {
            MovimentoAleatorio();
        }
    }

    private void PerseguirPlayer()
    {
        float distancia = Vector2.Distance(
            transform.position,
            player.position
        );

        if (distancia > distanciaMinimaDoPlayer)
        {
            Vector2 direcao = (
                player.position - transform.position
            ).normalized;

            rb.linearVelocity = direcao * velocidade;

            VirarParaDirecao(direcao.x);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void MovimentoAleatorio()
    {
        tempoDestino += Time.fixedDeltaTime;

        if (
            Vector2.Distance(transform.position, destinoAleatorio) < 0.2f ||
            tempoDestino >= tempoParaNovoDestino
        )
        {
            EscolherNovoDestino();
        }

        Vector2 direcao = (
            destinoAleatorio - (Vector2)transform.position
        ).normalized;

        rb.linearVelocity = direcao * velocidade;

        VirarParaDirecao(direcao.x);
    }

    private void EscolherNovoDestino()
    {
        Vector2 posicaoAleatoria =
            Random.insideUnitCircle * distanciaMovimentoAleatorio;

        destinoAleatorio =
            (Vector2)transform.position + posicaoAleatoria;

        tempoDestino = 0f;
    }

    private void VirarParaDirecao(float direcaoX)
    {
        if (spriteRenderer == null)
        {
            return;
        }

        if (direcaoX < -0.01f)
        {
            spriteRenderer.flipX = true;
        }
        else if (direcaoX > 0.01f)
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

        rb.linearVelocity = Vector2.zero;

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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(
            transform.position,
            distanciaDeteccao
        );

        Gizmos.color = Color.cyan;

        Gizmos.DrawWireSphere(
            transform.position,
            distanciaMovimentoAleatorio
        );
    }
}