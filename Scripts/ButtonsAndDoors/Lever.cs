using UnityEngine;

public class Lever : MonoBehaviour
{
    public Door door;
    public Sprite onSprite;
    public Sprite offSprite;

    private SpriteRenderer spriteRenderer;
    private bool isPlayerNearby;
    private bool isOn;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateVisualState();
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            isOn = !isOn;
            UpdateVisualState();

            if (isOn)
                door.Open();
            else
                door.Close();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            isPlayerNearby = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            isPlayerNearby = false;
    }

    private void UpdateVisualState()
    {
        if (spriteRenderer == null) return;
        spriteRenderer.sprite = isOn ? onSprite : offSprite;
    }
}