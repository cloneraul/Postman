
using System.Collections;
using UnityEngine;

public class MonkeyEnemy : MonoBehaviour
{
    [Header("Perseguição")]
    public float distanciaDeteccao = 8f;
    public float velocidade = 3f;
    public float distanciaParar = 1f;

    [Header("Pulo")]
    public float forcaPulo = 7f;
    public float diferencaAlturaPulo = 1.5f;
    public float distanciaObstaculo = 0.7f;
    public float intervaloPulos = 1.2f;

    [Header("Ataque")]
    public int danoContato = 1;
    public float intervaloDano = 1f;

    [Header("Congelamento")]
    public int maximoCongelamentos = 3;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Transform jogador;

    private bool noChao = false;
    private bool congelado = false;

    private float proximoDano = 0f;
    private float proximoPulo = 0f;

    private int vezesCongelado = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        rb.freezeRotation = true;

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            jogador = player.transform;
        }
    }

    private void Update()
    {
        if (jogador == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                jogador = player.transform;
            }

            return;
        }

        if (congelado)
        {
            return;
        }

        PerseguirJogador();
        VerificarPulo();
    }

    private void PerseguirJogador()
    {
        float distancia = Vector2.Distance(
            transform.position,
            jogador.position
        );

        if (distancia > distanciaDeteccao)
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            return;
        }

        float diferencaX = jogador.position.x - transform.position.x;

        if (Mathf.Abs(diferencaX) <= distanciaParar)
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            return;
        }

        float direcao = Mathf.Sign(diferencaX);

        rb.linearVelocity = new Vector2(
            direcao * velocidade,
            rb.linearVelocity.y
        );

        if (sprite != null)
        {
            sprite.flipX = direcao < 0;
        }
    }

    private void VerificarPulo()
    {
        if (!noChao || Time.time < proximoPulo)
        {
            return;
        }

        float diferencaAltura = jogador.position.y - transform.position.y;

        bool jogadorAcima = diferencaAltura > diferencaAlturaPulo;

        float direcao = Mathf.Sign(jogador.position.x - transform.position.x);

        Vector2 origemRaio = (Vector2)transform.position + Vector2.up * 0.3f;

        RaycastHit2D obstaculo = Physics2D.Raycast(
            origemRaio,
            Vector2.right * direcao,
            distanciaObstaculo
        );

        bool encontrouObstaculo =
            obstaculo.collider != null &&
            !obstaculo.collider.CompareTag("Player");

        if (jogadorAcima || encontrouObstaculo)
        {
            Pular();
        }
    }

    private void Pular()
    {
        rb.linearVelocity = new Vector2(
            rb.linearVelocity.x,
            0f
        );

        rb.AddForce(
            Vector2.up * forcaPulo,
            ForceMode2D.Impulse
        );

        noChao = false;
        proximoPulo = Time.time + intervaloPulos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        VerificarChao(collision);
        CausarDano(collision.gameObject);
        VerificarObstaculo(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        VerificarChao(collision);
        CausarDano(collision.gameObject);
        VerificarObstaculo(collision);
    }

    private void VerificarChao(Collision2D collision)
    {
        bool ehChao = collision.gameObject.CompareTag("chao") ||
                      collision.gameObject.GetComponent<FallingPlatform>() != null;

        if (!ehChao)
        {
            return;
        }

        foreach (ContactPoint2D contato in collision.contacts)
        {
            if (contato.normal.y > 0.5f)
            {
                noChao = true;
                return;
            }
        }
    }

    private void VerificarObstaculo(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        if (!noChao || congelado || Time.time < proximoPulo)
        {
            return;
        }

        foreach (ContactPoint2D contato in collision.contacts)
        {
            if (Mathf.Abs(contato.normal.x) > 0.5f)
            {
                Pular();
                return;
            }
        }
    }

    private void CausarDano(GameObject objeto)
    {
        if (Time.time < proximoDano || congelado)
        {
            return;
        }

        PlayerHealth playerHealth =
            objeto.GetComponentInParent<PlayerHealth>();

        if (playerHealth == null)
        {
            return;
        }

        playerHealth.TakeDamage(
            danoContato,
            transform.position
        );

        proximoDano = Time.time + intervaloDano;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CausarDano(other.gameObject);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        CausarDano(other.gameObject);
    }

    public void Freeze(float tempo)
    {
        vezesCongelado++;

        Debug.Log(
            "Macaco congelado: " +
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

        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);

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
}
