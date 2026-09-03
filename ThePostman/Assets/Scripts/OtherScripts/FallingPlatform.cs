using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [Header("Configurações")]
    public float tempoAntesDeCair = 1.5f;
    public float intensidadeTremor = 0.05f;

    private Rigidbody2D rb;
    private bool ativada = false;
    private Vector3 posicaoInicial;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.bodyType = RigidbodyType2D.Static;

        posicaoInicial = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!ativada && collision.gameObject.CompareTag("Player"))
        {
            ativada = true;
            StartCoroutine(TremerECair());
        }
    }

    private IEnumerator TremerECair()
    {
        float tempo = 0f;

        while (tempo < tempoAntesDeCair)
        {
            transform.position = posicaoInicial +
                                 (Vector3)Random.insideUnitCircle * intensidadeTremor;

            tempo += Time.deltaTime;

            yield return null;
        }

        transform.position = posicaoInicial;

        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void OnBecameInvisible()
    {
        if (ativada)
        {
            Destroy(gameObject);
        }
    }
}