using UnityEngine;

public class DiologTrigger : MonoBehaviour
{
    public DialogLine[] dialogLines;
    public bool autoTrigger = true;

    private bool isPlayerInRange;

    private void Update()
    {
        if (!autoTrigger && isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            DialogManager.Instance.StartDialog(dialogLines);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        isPlayerInRange = true;

        if (autoTrigger)
        {
            DialogManager.Instance.StartDialog(dialogLines);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}