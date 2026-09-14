using UnityEngine;
using TMPro;

public class LivesUI : MonoBehaviour
{
    public static LivesUI Instance;

    [Header("Texto das Vidas")]
    public TextMeshProUGUI textoVidas;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (PlayerLives.Instance != null)
        {
            AtualizarVidas(PlayerLives.Instance.vidasAtuais);
        }
    }

    public void AtualizarVidas(int quantidade)
    {
        if (textoVidas != null)
        {
            textoVidas.text = "Vidas: " + quantidade;
        }
    }
}