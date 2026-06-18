using System.Collections;
using System.Collections.Generic;
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
  //  private Animator _animator;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
  //      _animator = GetComponent<Animator>();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("chao"))
        {
            noChao = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("chao"))
        {
            noChao = false;
        }
    }

    void Update()
    {
        andando = false;

        
        if (Keyboard.current.aKey.isPressed)
        {
            transform.position += new Vector3(-velocidade * Time.deltaTime, 0, 0);

            _spriteRenderer.flipX = true;

            if (noChao)
            {
                andando = true;
            }

            Debug.Log("Andando para esquerda");
        }

        
        if (Keyboard.current.dKey.isPressed)
        {
            transform.position += new Vector3(velocidade * Time.deltaTime, 0, 0);

            _spriteRenderer.flipX = false;

            if (noChao)
            {
                andando = true;
            }

            Debug.Log("Andando para direita");
        }

        
        if (Keyboard.current.spaceKey.wasPressedThisFrame && noChao)
        {
            _rigidbody2D.AddForce(Vector2.up * forcaPulo, ForceMode2D.Impulse);

            Debug.Log("Pulou");
        }

        
        if (Keyboard.current.zKey.wasPressedThisFrame)
        {
            Debug.Log("Ataque");
       //     _animator.SetTrigger("Atacar");
        }

        
        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            Debug.Log("Power Up");
      //      _animator.SetTrigger("PowerUp");
        }

        
     //   _animator.SetBool("Andando", andando);
    }
}