using UnityEngine;
using UnityEngine.SceneManagement;

public class UILoader : MonoBehaviour
{
    private void Start()
    {
        if (GameObject.Find("UIManager") == null)
        {
            SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        }
    }
}