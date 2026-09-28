using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void TentarNovamente()
    {
        if (PlayerLives.Instance != null)
        {
            PlayerLives.Instance.ReiniciarVidas();
        }

        SceneManager.LoadScene("Fase1", LoadSceneMode.Single);
    }

    public void VoltarAoMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}