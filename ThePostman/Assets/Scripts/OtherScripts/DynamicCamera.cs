using UnityEngine;

public class DynamicCamera : MonoBehaviour
{
    [Header("Player")]
    public Transform player;

    [Header("Suavidade")]
    public float smoothSpeed = 5f;

    [Header("Distância da Câmera")]
    public float distanciaHorizontal = 2f;
    public float distanciaVertical = 1f;

    [Header("Limites")]
    public bool seguirVertical = true;

    private SpriteRenderer playerSprite;

    private void Start()
    {
        if (player != null)
        {
            playerSprite = player.GetComponent<SpriteRenderer>();
        }
    }

    private void LateUpdate()
    {
        if (player == null)
        {
            return;
        }

        float direcao = 1f;

        if (playerSprite != null && playerSprite.flipX)
        {
            direcao = -1f;
        }

        float posicaoX = player.position.x +
                          (distanciaHorizontal * direcao);

        float posicaoY;

        if (seguirVertical)
        {
            posicaoY = player.position.y + distanciaVertical;
        }
        else
        {
            posicaoY = transform.position.y;
        }

        Vector3 posicaoDesejada = new Vector3(
            posicaoX,
            posicaoY,
            transform.position.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            posicaoDesejada,
            smoothSpeed * Time.deltaTime
        );
    }
}