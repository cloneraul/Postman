using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float velocidade = 10f;
    public float forcaPulo = 10f;

    public bool noChao = false;
    public bool andando = false;

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        VerificarChao(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        VerificarChao(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (EhChaoOuPlataforma(collision.gameObject))
        {
            noChao = false;
        }
    }

    private void VerificarChao(Collision2D collision)
    {
        if (!EhChaoOuPlataforma(collision.gameObject))
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

    private bool EhChaoOuPlataforma(GameObject objeto)
    {
        if (objeto.CompareTag("chao"))
        {
            return true;
        }

        if (objeto.GetComponent<FallingPlatform>() != null)
        {
            return true;
        }

        return false;
    }

    private void Update()
    {
        if (Keyboard.current == null)
        {
            return;
        }

        andando = false;

        if (Keyboard.current.leftArrowKey.isPressed)
        {
            transform.position += new Vector3(
                -velocidade * Time.deltaTime,
                0f,
                0f
            );

            if (_spriteRenderer != null)
            {
                _spriteRenderer.flipX = true;
            }

            if (noChao)
            {
                andando = true;
            }
        }

        if (Keyboard.current.rightArrowKey.isPressed)
        {
            transform.position += new Vector3(
                velocidade * Time.deltaTime,
                0f,
                0f
            );

            if (_spriteRenderer != null)
            {
                _spriteRenderer.flipX = false;
            }

            if (noChao)
            {
                andando = true;
            }
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame && noChao)
        {
            _rigidbody2D.linearVelocity = new Vector2(
                _rigidbody2D.linearVelocity.x,
                0f
            );

            _rigidbody2D.AddForce(
                Vector2.up * forcaPulo,
                ForceMode2D.Impulse
            );

            noChao = false;
        }

        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            Debug.Log("Power Up");
        }
    }
}