using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLives : MonoBehaviour
{
    public static PlayerLives Instance;

    [Header("Configuração das Vidas")]
    public int vidasMaximas = 3;
    public int vidasAtuais = 3;

    [Header("Cenas")]
    public string nomeCenaMenu = "MainMenu";
    public string nomeCenaGameOver = "GameOver";

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
        else if (cena.name == nomeCenaGameOver)
        {
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

        Debug.Log("Vidas restantes: " + vidasAtuais);

        AtualizarInterface();

        if (vidasAtuais <= 0)
        {
            SceneManager.LoadScene(
                nomeCenaGameOver,
                LoadSceneMode.Single
            );
        }
        else
        {
            SceneManager.LoadScene(
                SceneManager.GetActiveScene().name,
                LoadSceneMode.Single
            );
        }
    }

    public void GanharVida(int quantidade)
    {
        vidasAtuais += quantidade;

        if (vidasAtuais > vidasMaximas)
        {
            vidasAtuais = vidasMaximas;
        }

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