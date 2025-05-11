using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public string formType; // "iron", "fire", "ice" или "default"

    /*private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            other.collider.GetComponent<ChangeSlimeForm>().AddItem(formType);
            Destroy(gameObject);
        }
    }*/
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            Debug.Log($"Player подобрал {formType}");
            var slimeForm = other.collider.GetComponent<ChangeSlimeForm>();
            if (slimeForm != null)
            {
                slimeForm.AddItem(formType);
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("На игроке нет компонента ChangeSlimeForm!");
            }
        }
    }
}