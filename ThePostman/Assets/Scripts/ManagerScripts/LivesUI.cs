
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LivesUI : MonoBehaviour
{
    public static LivesUI Instance;

    [Header("Texto das Vidas")]
    public TextMeshProUGUI textoVidas;

    [Header("Cenas")]
    public string nomeCenaMenu = "MainMenu";
    public string nomeCenaGameOver = "GameOver";

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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += AoCarregarCena;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= AoCarregarCena;
    }

    private void Start()
    {
        VerificarCena();

        if (PlayerLives.Instance != null)
        {
            AtualizarVidas(PlayerLives.Instance.vidasAtuais);
        }
    }

    private void AoCarregarCena(Scene cena, LoadSceneMode modo)
    {
        VerificarCena();
    }

    private void VerificarCena()
    {
        string cenaAtual = SceneManager.GetActiveScene().name;

        if (cenaAtual == nomeCenaMenu ||
            cenaAtual == nomeCenaGameOver)
        {
            EsconderTexto();
        }
        else
        {
            MostrarTexto();
        }
    }

    public void AtualizarVidas(int quantidade)
    {
        if (textoVidas != null)
        {
            textoVidas.text = "Vidas: " + quantidade;
        }
    }

    public void EsconderTexto()
    {
        if (textoVidas != null)
        {
            textoVidas.enabled = false;
        }
    }

    public void MostrarTexto()
    {
        if (textoVidas != null)
        {
            textoVidas.enabled = true;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
