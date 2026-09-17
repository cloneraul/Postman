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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("chao"))
        {
            noChao = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("chao"))
        {
            noChao = false;
        }
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
                0,
                0
            );

            _spriteRenderer.flipX = true;

            if (noChao)
            {
                andando = true;
            }
        }

        if (Keyboard.current.rightArrowKey.isPressed)
        {
            transform.position += new Vector3(
                velocidade * Time.deltaTime,
                0,
                0
            );

            _spriteRenderer.flipX = false;

            if (noChao)
            {
                andando = true;
            }
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame && noChao)
        {
            _rigidbody2D.linearVelocity = new Vector2(
                0,
                _rigidbody2D.linearVelocity.y
            );

            _rigidbody2D.AddForce(
                Vector2.up * forcaPulo,
                ForceMode2D.Impulse
            );
        }

        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            Debug.Log("Power Up");
        }
    }
}