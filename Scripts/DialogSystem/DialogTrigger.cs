using UnityEngine;

public class DiologTrigger : MonoBehaviour
{
    public DialogLine[] dialogLines;
    public bool autoTrigger = true;
    public GameObject interactionPromptPrefab;

    private bool isPlayerInRange;
    private GameObject promptInstance;

    private void Update()
    {
        if (!autoTrigger && isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            DialogManager.Instance.StartDialog(dialogLines);
            if (promptInstance)
            {
                promptInstance.SetActive(false);
            }
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
        else if (interactionPromptPrefab != null && promptInstance == null)
        {
            /*promptInstance = Instantiate(interactionPromptPrefab, transform.position + Vector3.up * 1.2f, Quaternion.identity, transform);
            promptInstance.SetActive(true);*/
            
            promptInstance = Instantiate(interactionPromptPrefab, transform.parent); // под Canvas
            promptInstance.SetActive(true);

            Vector3 worldPos = transform.position + Vector3.up * 1.2f;
            Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPos); // позиция в UI
            promptInstance.GetComponent<RectTransform>().position = screenPos;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        isPlayerInRange = false;

        if (promptInstance)
        {
            Destroy(promptInstance);
        }
        /*if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }*/
    }
}