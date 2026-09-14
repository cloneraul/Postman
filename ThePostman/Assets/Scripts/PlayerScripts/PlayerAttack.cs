using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Ataque")]
    public GameObject ataquePrefab;
    public float distanciaDoPlayer = 0.9f;
    public float tempoEntreAtaques = 0.35f;

    private SpriteRenderer spriteRenderer;
    private float proximoAtaque;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Keyboard.current == null)
        {
            return;
        }

        if (Keyboard.current.zKey.wasPressedThisFrame)
        {
            Atacar();
        }
    }

    private void Atacar()
    {
        if (Time.time < proximoAtaque)
        {
            return;
        }

        if (ataquePrefab == null)
        {
            Debug.LogWarning("O prefab do ataque não foi configurado no PlayerAttack.");
            return;
        }

        proximoAtaque = Time.time + tempoEntreAtaques;

        float direcao = 1f;

        if (spriteRenderer != null && spriteRenderer.flipX)
        {
            direcao = -1f;
        }

        Vector3 posicaoAtaque = transform.position;

        posicaoAtaque.x += distanciaDoPlayer * direcao;

        GameObject ataque = Instantiate(
            ataquePrefab,
            posicaoAtaque,
            Quaternion.identity
        );

        Vector3 escala = ataque.transform.localScale;
        escala.x = Mathf.Abs(escala.x) * direcao;
        ataque.transform.localScale = escala;
    }
}