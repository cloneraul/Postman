using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void TentarNovamente()
    {
        SceneManager.LoadScene("Fase1", LoadSceneMode.Single);
    }

    public void VoltarAoMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}