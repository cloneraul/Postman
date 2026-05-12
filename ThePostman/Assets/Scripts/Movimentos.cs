using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocidade = 10f;
    public float forcaPulo = 10f;

    public bool noChao = false;
    public bool andando = false;

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _animator = gameObject.GetComponent<Animator>();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "chao")
        {
            noChao = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "chao")
        {
            noChao = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        andando = false;

        // ANDAR PARA ESQUERDA
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.position += new Vector3(-velocidade * Time.deltaTime, 0, 0);

            _spriteRenderer.flipX = true;

            if (noChao == true)
            {
                andando = true;
            }

            Debug.Log("Andando para esquerda");
        }

        // ANDAR PARA DIREITA
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.position += new Vector3(velocidade * Time.deltaTime, 0, 0);

            _spriteRenderer.flipX = false;

            if (noChao == true)
            {
                andando = true;
            }

            Debug.Log("Andando para direita");
        }

        // PULO
        if (Input.GetKeyDown(KeyCode.Space) && noChao == true)
        {
            _rigidbody2D.AddForce(new Vector2(0, 1) * forcaPulo, ForceMode2D.Impulse);

            Debug.Log("Pulou");
        }

        // ATAQUE
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Ataque");

            // Ativa animação de ataque
            _animator.SetTrigger("Atacar");
        }

        // POWER UP
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("Power Up");

            // Ativa animação de power up
            _animator.SetTrigger("PowerUp");
        }

        // ANIMAÇÃO DE ANDAR
        _animator.SetBool("Andando", andando);
    }
}