using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Vida")]
    public int vida = 3;
    public int vidaMaxima = 3;

    [Header("Invencibilidade")]
    public float tempoInvencibilidade = 1.5f;
    public float intervaloPiscar = 0.1f;

    [Header("Knockback")]
    public float forcaKnockback = 6f;
    public float forcaKnockbackVertical = 4f;

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;

    private bool morreu = false;
    private bool invencivel = false;
    private bool recebeuKnockback = false;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        vida = vidaMaxima;

        UIManager.AtualizarVida(vida);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("chao"))
        {
            return;
        }

        if (!recebeuKnockback)
        {
            return;
        }

        foreach (ContactPoint2D contato in collision.contacts)
        {
            if (contato.normal.y > 0.5f)
            {
                PararDeslizamento();
                break;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("chao"))
        {
            return;
        }

        if (!recebeuKnockback)
        {
            return;
        }

        foreach (ContactPoint2D contato in collision.contacts)
        {
            if (contato.normal.y > 0.5f)
            {
                PararDeslizamento();
                break;
            }
        }
    }

    public void TakeDamage(int dano)
    {
        TakeDamage(dano, transform.position);
    }

    public void TakeDamage(int dano, Vector2 origemDano)
    {
        if (morreu || invencivel)
        {
            return;
        }

        vida -= dano;

        if (vida < 0)
        {
            vida = 0;
        }

        Debug.Log("Vida do Player: " + vida);

        UIManager.AtualizarVida(vida);

        AplicarKnockback(origemDano);

        if (vida <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(Invencibilidade());
        }
    }

    public void Heal(int quantidade)
    {
        if (morreu)
        {
            return;
        }

        vida += quantidade;

        if (vida > vidaMaxima)
        {
            vida = vidaMaxima;
        }

        Debug.Log("Vida recuperada: " + vida);

        UIManager.AtualizarVida(vida);
    }

    private void AplicarKnockback(Vector2 origemDano)
    {
        if (_rigidbody2D == null)
        {
            return;
        }

        float direcao = transform.position.x - origemDano.x;

        if (direcao >= 0)
        {
            direcao = 1f;
        }
        else
        {
            direcao = -1f;
        }

        recebeuKnockback = true;

        _rigidbody2D.linearVelocity = new Vector2(
            direcao * forcaKnockback,
            forcaKnockbackVertical
        );
    }

    private void PararDeslizamento()
    {
        if (_rigidbody2D == null)
        {
            return;
        }

        _rigidbody2D.linearVelocity = new Vector2(
            0f,
            _rigidbody2D.linearVelocity.y
        );

        recebeuKnockback = false;
    }

    private IEnumerator Invencibilidade()
    {
        invencivel = true;

        float tempo = 0f;

        while (tempo < tempoInvencibilidade)
        {
            if (_spriteRenderer != null)
            {
                _spriteRenderer.enabled = !_spriteRenderer.enabled;
            }

            yield return new WaitForSeconds(intervaloPiscar);

            tempo += intervaloPiscar;
        }

        if (_spriteRenderer != null)
        {
            _spriteRenderer.enabled = true;
        }

        invencivel = false;
    }

    private void Die()
    {
        if (morreu)
        {
            return;
        }

        morreu = true;

        Debug.Log("Player morreu");

        if (PlayerLives.Instance != null)
        {
            PlayerLives.Instance.PerderVida();
        }
        else
        {
            Debug.LogWarning("PlayerLives não foi encontrado.");
        }

        Destroy(gameObject);
    }
}