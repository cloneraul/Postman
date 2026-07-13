using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPowerUp : MonoBehaviour
{
    [Header("Poderes")]
    public bool temPoderDeFogo = false;
    public bool temPoderDeGelo = false;

    [Header("Projéteis")]
    public GameObject fireballPrefab;
    public GameObject iceProjectilePrefab;

    public Transform pontoDisparo;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            if (temPoderDeFogo)
            {
                AtirarFogo();
            }

            if (temPoderDeGelo)
            {
                AtirarGelo();
            }
        }
    }

    private void AtirarFogo()
    {
        Vector3 posicaoTiro = transform.position;

        if (spriteRenderer.flipX)
            posicaoTiro.x -= 0.7f;
        else
            posicaoTiro.x += 0.7f;

        GameObject bola = Instantiate(
            fireballPrefab,
            posicaoTiro,
            Quaternion.identity);

        Fireball fireball = bola.GetComponent<Fireball>();

        fireball.direcao = spriteRenderer.flipX ? -1 : 1;
    }

    private void AtirarGelo()
    {
        Vector3 posicaoTiro = transform.position;

        if (spriteRenderer.flipX)
            posicaoTiro.x -= 0.7f;
        else
            posicaoTiro.x += 0.7f;

        GameObject gelo = Instantiate(
            iceProjectilePrefab,
            posicaoTiro,
            Quaternion.identity);

        IceProjectile projectile = gelo.GetComponent<IceProjectile>();

        projectile.direcao = spriteRenderer.flipX ? -1 : 1;
    }
}