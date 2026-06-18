using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPowerUp : MonoBehaviour
{
    public bool temPoderDeFogo = false;

    public Transform pontoDisparo;
    public GameObject fireballPrefab;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (temPoderDeFogo &&
            Keyboard.current.xKey.wasPressedThisFrame)
        {
            Atirar();
        }
    }

    private void Atirar()
    {
        Vector3 posicaoTiro = transform.position;

        if (spriteRenderer.flipX)
        {
            posicaoTiro.x -= 0.7f;
        }
        else
        {
            posicaoTiro.x += 0.7f;
        }

        GameObject bola = Instantiate(
            fireballPrefab,
            posicaoTiro,
            Quaternion.identity
        );

        Fireball fireball = bola.GetComponent<Fireball>();

        fireball.direcao = spriteRenderer.flipX ? -1 : 1;
    }
}