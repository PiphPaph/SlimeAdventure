using UnityEngine;

public enum ButtonMode { HoldToActivate, Toggle, Timed }

public class PressureButton : MonoBehaviour
{
    public Sprite notPressedSprite;
    public Sprite pressedSprite;

    public ButtonMode mode;
    public float timerDuration = 3f;
    public Door door;

    private SpriteRenderer spriteRenderer;
    private bool isPressed;
    //private bool isPlayerOnButton;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = notPressedSprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        //isPlayerOnButton = true;
        if (mode == ButtonMode.HoldToActivate)
        {
            door.Open();
            spriteRenderer.sprite = pressedSprite;
        }
        else if (mode == ButtonMode.Toggle)
        {
            isPressed = !isPressed;
            spriteRenderer.sprite = isPressed ? pressedSprite : notPressedSprite;
            if (isPressed) door.Open(); else door.Close();
        }
        else if (mode == ButtonMode.Timed)
        {
            spriteRenderer.sprite = pressedSprite;
            door.Open();
            Invoke(nameof(ResetButton), timerDuration);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        //isPlayerOnButton = false;
        if (mode == ButtonMode.HoldToActivate)
        {
            door.Close();
            spriteRenderer.sprite = notPressedSprite;
        }
    }

    private void ResetButton()
    {
        if (mode == ButtonMode.Timed)
        {
            door.Close();
            spriteRenderer.sprite = notPressedSprite;
        }
    }
}