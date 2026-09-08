using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movimentação")]
    public float velocidade = 2f;
    public float distanciaMovimento = 3f;

    [Header("Direção")]
    public bool moverHorizontalmente = true;
    public bool moverVerticalmente = false;

    private Vector3 posicaoInicial;
    private Vector3 destino;
    private bool indoParaFrente = true;

    private void Start()
    {
        posicaoInicial = transform.position;

        DefinirDestino();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            destino,
            velocidade * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, destino) < 0.01f)
        {
            indoParaFrente = !indoParaFrente;
            DefinirDestino();
        }
    }

    private void DefinirDestino()
    {
        Vector3 direcao = Vector3.zero;

        if (moverHorizontalmente)
        {
            direcao = Vector3.right;
        }

        if (moverVerticalmente)
        {
            direcao = Vector3.up;
        }

        if (indoParaFrente)
        {
            destino = posicaoInicial + direcao * distanciaMovimento;
        }
        else
        {
            destino = posicaoInicial - direcao * distanciaMovimento;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 direcao = Vector3.zero;

        if (moverHorizontalmente)
        {
            direcao = Vector3.right;
        }

        if (moverVerticalmente)
        {
            direcao = Vector3.up;
        }

        Vector3 pontoA =
            transform.position - direcao * distanciaMovimento;

        Vector3 pontoB =
            transform.position + direcao * distanciaMovimento;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(pontoA, pontoB);

        Gizmos.DrawSphere(pontoA, 0.15f);
        Gizmos.DrawSphere(pontoB, 0.15f);
    }
}