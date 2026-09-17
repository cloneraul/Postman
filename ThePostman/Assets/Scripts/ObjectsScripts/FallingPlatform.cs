using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [Header("Configurações")]
    public float tempoAntesDeCair = 1.5f;
    public float velocidadeQueda = 5f;
    public float tempoParaDesaparecer = 5f;

    private Rigidbody2D rb;
    private bool ativada = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }

        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ativada)
        {
            return;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            ativada = true;
            StartCoroutine(Cair());
        }
    }

    private IEnumerator Cair()
    {
        yield return new WaitForSeconds(tempoAntesDeCair);

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = velocidadeQueda;

        yield return new WaitForSeconds(tempoParaDesaparecer);

        Destroy(gameObject);
    }
}