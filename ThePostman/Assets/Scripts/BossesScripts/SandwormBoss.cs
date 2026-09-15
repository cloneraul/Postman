using System.Collections;
using UnityEngine;

public class SandwormBoss : MonoBehaviour
{
    [Header("Player")]
    public Transform player;

    [Header("Ataque")]
    public float distanciaDeteccao = 8f;
    public float tempoEscondido = 2f;
    public float tempoForaDoChao = 2f;

    [Header("Movimentação")]
    public float velocidadeMovimento = 8f;
    public float alturaAtaque = 3f;

    [Header("Dano ao Player")]
    public int dano = 1;

    [Header("Vida do Boss")]
    public int vidaMaxima = 10;
    public int vidaAtual;

    [Header("Congelamento")]
    public bool congelado = false;
    public float tempoCongeladoAtual = 0f;
    public Color corCongelado = Color.cyan;

    private Vector3 posicaoEscondida;
    private Vector3 posicaoForaDoChao;

    private bool atacando = false;
    private Coroutine rotinaCongelamento;

    private SpriteRenderer[] sprites;
    private Color[] coresOriginais;

    private void Start()
    {
        vidaAtual = vidaMaxima;

        sprites = GetComponentsInChildren<SpriteRenderer>();

        coresOriginais = new Color[sprites.Length];

        for (int i = 0; i < sprites.Length; i++)
        {
            coresOriginais[i] = sprites[i].color;
        }

        if (player == null)
        {
            GameObject objetoPlayer =
                GameObject.FindGameObjectWithTag("Player");

            if (objetoPlayer != null)
            {
                player = objetoPlayer.transform;
            }
        }

        posicaoEscondida = transform.position;

        posicaoForaDoChao =
            posicaoEscondida + Vector3.up * alturaAtaque;

        StartCoroutine(ComportamentoBoss());
    }

    private IEnumerator ComportamentoBoss()
    {
        while (true)
        {
            if (congelado)
            {
                yield return null;
                continue;
            }

            if (player != null)
            {
                float distancia = Vector2.Distance(
                    transform.position,
                    player.position
                );

                if (distancia <= distanciaDeteccao)
                {
                    yield return StartCoroutine(AparecerAtacar());
                }
                else
                {
                    yield return new WaitForSeconds(0.5f);
                }
            }
            else
            {
                yield return null;
            }
        }
    }

    private IEnumerator AparecerAtacar()
    {
        atacando = true;

        Vector3 posicaoAtaque = new Vector3(
            player.position.x,
            posicaoForaDoChao.y,
            posicaoEscondida.z
        );

        transform.position = new Vector3(
            player.position.x,
            posicaoEscondida.y,
            posicaoEscondida.z
        );

        while (
            Vector3.Distance(
                transform.position,
                posicaoAtaque
            ) > 0.01f
        )
        {
            if (congelado)
            {
                atacando = false;
                yield break;
            }

            transform.position = Vector3.MoveTowards(
                transform.position,
                posicaoAtaque,
                velocidadeMovimento * Time.deltaTime
            );

            yield return null;
        }

        yield return new WaitForSeconds(tempoForaDoChao);

        if (congelado)
        {
            atacando = false;
            yield break;
        }

        while (
            Vector3.Distance(
                transform.position,
                posicaoEscondida
            ) > 0.01f
        )
        {
            if (congelado)
            {
                atacando = false;
                yield break;
            }

            transform.position = Vector3.MoveTowards(
                transform.position,
                posicaoEscondida,
                velocidadeMovimento * Time.deltaTime
            );

            yield return null;
        }

        atacando = false;

        yield return new WaitForSeconds(tempoEscondido);
    }

    public void Freeze(float tempo)
    {
        if (rotinaCongelamento != null)
        {
            StopCoroutine(rotinaCongelamento);
        }

        rotinaCongelamento =
            StartCoroutine(CongelarBoss(tempo));
    }

    private IEnumerator CongelarBoss(float tempo)
    {
        congelado = true;
        atacando = false;
        tempoCongeladoAtual = tempo;

        MudarCor(corCongelado);

        Debug.Log(
            "Sandworm congelado por " +
            tempo +
            " segundos."
        );

        yield return new WaitForSeconds(tempo);

        congelado = false;
        tempoCongeladoAtual = 0f;

        RestaurarCor();

        Debug.Log("Sandworm descongelado.");
    }

    private void MudarCor(Color novaCor)
    {
        if (sprites == null)
        {
            return;
        }

        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].color = novaCor;
        }
    }

    private void RestaurarCor()
    {
        if (sprites == null || coresOriginais == null)
        {
            return;
        }

        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].color = coresOriginais[i];
        }
    }

    public void TakeDamage(int quantidade)
    {
        vidaAtual -= quantidade;

        Debug.Log(
            "Sandworm recebeu dano. Vida atual: " +
            vidaAtual
        );

        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    private void Morrer()
    {
        Debug.Log("Sandworm derrotado!");

        StopAllCoroutines();
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!atacando || congelado)
        {
            return;
        }

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