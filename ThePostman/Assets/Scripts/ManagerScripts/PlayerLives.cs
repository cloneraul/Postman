using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLives : MonoBehaviour
{
    public static PlayerLives Instance;

    [Header("Configuração das Vidas")]
    public int vidasMaximas = 3;
    public int vidasAtuais = 3;

    [Header("Cena do Menu")]
    public string nomeCenaMenu = "Menu";

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
        AtualizarInterface();
    }

    private void AoCarregarCena(Scene cena, LoadSceneMode modo)
    {
        if (cena.name == nomeCenaMenu)
        {
            vidasAtuais = vidasMaximas;

            if (CheckpointManager.Instance != null)
            {
                CheckpointManager.Instance.LimparCheckpoint();
            }

            AtualizarInterface();

            if (LivesUI.Instance != null)
            {
                LivesUI.Instance.EsconderTexto();
            }
        }
        else
        {
            AtualizarInterface();

            if (LivesUI.Instance != null)
            {
                LivesUI.Instance.MostrarTexto();
            }
        }
    }

    public void PerderVida()
    {
        vidasAtuais--;

        if (vidasAtuais < 0)
        {
            vidasAtuais = 0;
        }

        AtualizarInterface();

        if (vidasAtuais <= 0)
        {
            SceneManager.LoadScene(nomeCenaMenu);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void GanharVida(int quantidade)
    {
        vidasAtuais += quantidade;

        Debug.Log("Vidas atuais: " + vidasAtuais);

        AtualizarInterface();
    }

    private void AtualizarInterface()
    {
        if (LivesUI.Instance != null)
        {
            LivesUI.Instance.AtualizarVidas(vidasAtuais);
        }
    }
}