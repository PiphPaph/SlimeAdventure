using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("Config")] public ActConfig actConfig;

    [Header("Start Point (из хаба)")] public Transform startPoint;

    [Header("Prefabs")] public List<GameObject> trapPrefabs;
    public GameObject leverPrefab;

    [Header("Settings")] public int seed = 0;

    private List<GameObject> availableRooms;
    private Dictionary<int, GameObject> fixedLookup;

    void Start()
    {
        Random.InitState(seed);
        Generate();
    }

    void SpawnTraps(RoomData room, Transform parent)
    {
        foreach (Transform point in room.trapPoints)
        {
            if (Random.value < 0.5f)
            {
                GameObject trap = trapPrefabs[Random.Range(0, trapPrefabs.Count)];
                Instantiate(trap, point.position, Quaternion.identity, parent);
            }
        }
    }

    void SpawnLever(RoomData room, GameObject roomObj)
    {
        Door door = roomObj.GetComponentInChildren<Door>();

        if (door == null) return;

        foreach (Transform point in room.interactablePoints)
        {
            if (Random.value < 0.5f)
            {
                GameObject obj = Instantiate(leverPrefab, point.position, Quaternion.identity, roomObj.transform);

                RoomLever lever = obj.GetComponent<RoomLever>();
                lever.SetDoor(door);
            }
        }
    }

    void Generate()
    {
        availableRooms = new List<GameObject>(actConfig.roomPool);

        fixedLookup = new Dictionary<int, GameObject>();
        foreach (var f in actConfig.fixedRooms)
        {
            fixedLookup[f.index] = f.prefab;

            // убираем из пула, чтобы не повторялась
            availableRooms.Remove(f.prefab);
        }

        Transform previousExit = startPoint;

        for (int i = 0; i < actConfig.roomCount; i++)
        {
            GameObject prefab;

            if (fixedLookup.ContainsKey(i))
            {
                prefab = fixedLookup[i];
            }
            else
            {
                prefab = GetUniqueRoom();
            }

            GameObject room = Instantiate(prefab);

            RoomData data = room.GetComponent<RoomData>();

            // 📍 стыковка комнат
            Vector3 offset = previousExit.position - data.entryPoint.position;
            room.transform.position += offset;

            previousExit = data.exitPoint;

            // 🎯 наполнение комнаты
            SpawnTraps(data, room.transform);
            SpawnLever(data, room);
        }
    }

    GameObject GetUniqueRoom()
    {
        int index = Random.Range(0, availableRooms.Count);
        GameObject room = availableRooms[index];

        availableRooms.RemoveAt(index);

        return room;
    }
}