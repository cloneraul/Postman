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

    [Header("Dano")]
    public int dano = 1;

    private Vector3 posicaoEscondida;
    private Vector3 posicaoForaDoChao;

    private bool atacando = false;

    private void Start()
    {
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
            transform.position = Vector3.MoveTowards(
                transform.position,
                posicaoAtaque,
                velocidadeMovimento * Time.deltaTime
            );

            yield return null;
        }

        yield return new WaitForSeconds(tempoForaDoChao);

        while (
            Vector3.Distance(
                transform.position,
                posicaoEscondida
            ) > 0.01f
        )
        {
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!atacando)
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