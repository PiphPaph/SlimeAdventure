using UnityEngine;

public class RoomData : MonoBehaviour
{
    [Header("Connection Points")]
    public Transform entryPoint; // куда входит предыдущая комната
    public Transform exitPoint;  // куда будет цепляться следующая

    [Header("Spawn Points")]
    public Transform[] trapPoints;
    public Transform[] interactablePoints;

    [Header("Room Info")]
    public string roomID;
}