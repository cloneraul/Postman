using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Cenas")]
    public string primeiraFase = "Fase1";
    public string nomeCenaMenu = "Menu";

    [Header("Painéis")]
    public GameObject panelMenu;
    public GameObject panelCreditos;

    private void Start()
    {
        MostrarMenu();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(primeiraFase);
    }

    public void AbrirCreditos()
    {
        panelMenu.SetActive(false);
        panelCreditos.SetActive(true);
    }

    public void MostrarMenu()
    {
        panelMenu.SetActive(true);
        panelCreditos.SetActive(false);
    }

    public void VoltarAoMenu()
    {
        SceneManager.LoadScene(nomeCenaMenu);
    }

    public void SairDoJogo()
    {
        Debug.Log("Saindo do jogo...");

        Application.Quit();
    }
}