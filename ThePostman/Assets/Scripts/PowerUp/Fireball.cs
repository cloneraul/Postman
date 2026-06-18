using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float velocidade = 10f;
    public int direcao = 1;

    private void Update()
    {
        transform.Translate(
            Vector2.right *
            direcao *
            velocidade *
            Time.deltaTime
        );
    }

    private void Start()
    {
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}