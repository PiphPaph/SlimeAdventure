using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public string formType; // "iron", "fire", "ice" или "default"

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<ChangeSlimeForm>().AddItem(formType);
            Destroy(gameObject);
        }
    }
}