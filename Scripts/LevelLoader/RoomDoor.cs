using UnityEngine;

public class RoomDoor : MonoBehaviour
{
    public bool isOpen = false;

    public void Open()
    {
        if (isOpen) return;

        isOpen = true;

        // самый простой вариант
        gameObject.SetActive(false);

        // позже можно заменить на анимацию
    }
}
