using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool ativado = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (ativado)
        {
            return;
        }

        if (!other.CompareTag("Player"))
        {
            return;
        }

        if (CheckpointManager.Instance != null)
        {
            CheckpointManager.Instance.AtivarCheckpoint(transform.position);

            ativado = true;

            Debug.Log("Checkpoint ativado pelo Player.");
        }
        else
        {
            Debug.LogWarning("CheckpointManager não foi encontrado.");
        }
    }
}