using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLives : MonoBehaviour
{
    public static PlayerLives Instance;

    [Header("Configuração das Vidas")]
    public int vidasMaximas = 3;
    public int vidasAtuais = 3;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        AtualizarUI();
    }

    public void PerderVida()
    {
        vidasAtuais--;

        if (vidasAtuais < 0)
        {
            vidasAtuais = 0;
        }

        AtualizarUI();

        if (vidasAtuais <= 0)
        {
            VoltarAoMenu();
        }
        else
        {
            ReiniciarFase();
        }
    }

    private void ReiniciarFase()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void VoltarAoMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void AtualizarUI()
    {
        if (LivesUI.Instance != null)
        {
            LivesUI.Instance.AtualizarVidas(vidasAtuais);
        }
    }
}