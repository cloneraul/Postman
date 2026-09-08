using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public static int vidaAtual = 3;

    private HeartsUI heartsUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);

            heartsUI = GetComponentInChildren<HeartsUI>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        AtualizarVida(vidaAtual);
    }

    public static void AtualizarVida(int vida)
    {
        vidaAtual = vida;

        if (Instance != null && Instance.heartsUI != null)
        {
            Instance.heartsUI.Atualizar(vidaAtual);
        }
    }
}