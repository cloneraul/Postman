using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;

    private Vector3 posicaoCheckpoint;
    private string cenaCheckpoint;
    private bool checkpointAtivado = false;

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

    public void AtivarCheckpoint(Vector3 novaPosicao)
    {
        posicaoCheckpoint = novaPosicao;
        cenaCheckpoint = SceneManager.GetActiveScene().name;
        checkpointAtivado = true;

        Debug.Log("Checkpoint ativado!");
    }

    private void AoCarregarCena(Scene cena, LoadSceneMode modo)
    {
        if (!checkpointAtivado)
        {
            return;
        }

        if (cena.name != cenaCheckpoint)
        {
            return;
        }

        StartCoroutine(ReposicionarPlayer());
    }

    private IEnumerator ReposicionarPlayer()
    {
        yield return null;

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            player.transform.position = posicaoCheckpoint;

            Debug.Log("Player voltou para o checkpoint.");
        }
        else
        {
            Debug.LogWarning("Player não foi encontrado após carregar a cena.");
        }
    }

    public void LimparCheckpoint()
    {
        checkpointAtivado = false;
    }
}