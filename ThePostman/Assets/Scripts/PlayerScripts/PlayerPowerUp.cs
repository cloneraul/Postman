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
        GameObject bola =
            Instantiate(
                fireballPrefab,
                pontoDisparo.position,
                Quaternion.identity
            );

        Fireball fireball =
            bola.GetComponent<Fireball>();

        if (spriteRenderer.flipX)
        {
            fireball.direcao = -1;
        }
        else
        {
            fireball.direcao = 1;
        }
    }
}