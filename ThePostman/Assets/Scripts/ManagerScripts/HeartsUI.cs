using UnityEngine;

public class HeartsUI : MonoBehaviour
{
    public GameObject[] blocosVida;

    public void Atualizar(int vida)
    {
        for (int i = 0; i < blocosVida.Length; i++)
        {
            if (i < vida)
            {
                blocosVida[i].SetActive(true);
            }
            else
            {
                blocosVida[i].SetActive(false);
            }
        }
    }
}