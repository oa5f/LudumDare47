using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomLoader : MonoBehaviour
{
    [SerializeField] private Room roomPrefab;

    [SerializeField] private PlayerMovement player;

    [SerializeField] private int loadDistance = 2;

    private readonly Queue<Room> loadedRooms = new Queue<Room>();
    private int currentRoom = 0;

    private void Start()
    {
        Vector3 roomSize = roomPrefab.endPoint.position - roomPrefab.startPoint.position;

        Vector3 currentRoomPos = -roomPrefab.startPoint.position - roomSize * (loadDistance + 1);
        for (int i = -loadDistance; i <= loadDistance; i++)
        {
            Room room = SpawnRoom(currentRoomPos, i);

            currentRoomPos = room.endPoint.position;

        }
    }

    private void Update()
    {
        CheckIfNewRoomShouldLoad();
    }
    private void CheckIfNewRoomShouldLoad()
    {
        foreach (Room room in loadedRooms)
        {
            if (room.roomIndex == currentRoom)
            {
                if (room.endPoint.position.z < player.transform.position.z)
                    LoadNextRoom();
                break;
            }
        }
    }
    private Room SpawnRoom(Vector3 pos, int index)
    {
        Room newRoom = Instantiate(roomPrefab);
        newRoom.transform.position = pos - newRoom.startPoint.position;
        newRoom.roomIndex = index;
        newRoom.name = $"Room {index}";

        loadedRooms.Enqueue(newRoom);
        return newRoom;
    }
    private void LoadNextRoom()
    {
        Destroy(loadedRooms.Dequeue().gameObject);

        currentRoom++;
        SpawnRoom(loadedRooms.Last().endPoint.position, currentRoom + loadDistance);
    }
}
