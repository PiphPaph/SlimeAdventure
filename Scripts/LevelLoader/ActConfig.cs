using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FixedRoom
{
    public int index;
    public GameObject prefab;
}

[CreateAssetMenu(menuName = "Level/Act Config")]
public class ActConfig : ScriptableObject
{
    public List<GameObject> roomPool;
    public List<FixedRoom> fixedRooms;

    public int roomCount = 5;
}