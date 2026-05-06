using UnityEngine;

public class RoomLever : MonoBehaviour
{
    private Door door;
    private bool activated = false;

    public void SetDoor(Door targetDoor)
    {
        door = targetDoor;
    }

    // вызови это при нажатии (пока можно через Trigger)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (activated) return;

        activated = true;

        if (door != null)
        {
            door.Open();
        }
    }
}