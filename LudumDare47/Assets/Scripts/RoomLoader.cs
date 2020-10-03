using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class RoomLoader : MonoBehaviour
{
    [SerializeField] private Room roomPrefab;

    [SerializeField] private PlayerMovement player;

    [SerializeField] private int loadDistance = 2;

    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private RoomFailedManager roomFailed;

    private readonly Queue<Room> loadedRooms = new Queue<Room>();
    private int currentRoom = 0;

    private void Start()
    {
        Vector3 roomSize = roomPrefab.endPoint.position - roomPrefab.startPoint.position;

        Vector3 currentRoomPos = -roomPrefab.startPoint.position - roomSize * (loadDistance + 1);
        for (int i = -loadDistance; i <= loadDistance; i++)
        {
            Room room = SpawnRoom(currentRoomPos, i);

            if (room.roomIndex == 0)
                room.OnRoomLoaded(OnRoomCompleted, OnRoomFailed);

            currentRoomPos = room.endPoint.position;

        }
    }

    private void Update()
    {
        CheckIfNewRoomShouldLoad();

        foreach (Room room in loadedRooms)
        {
            if (room.roomIndex == currentRoom)
                timer.text = Mathf.CeilToInt(room.TimeLeft).ToString();
        }
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

        foreach(Room room in loadedRooms)
        {
            if (room.roomIndex == currentRoom)
                room.OnRoomLoaded(OnRoomCompleted, OnRoomFailed);
        }
    }
    public void OnRoomCompleted()
    {
        Debug.Log("Room Completed!");
    }
    public void OnRoomFailed()
    {
        Debug.Log("Room Failed!");
        roomFailed.Show();
        foreach(Room room in loadedRooms)
        {
            if (room.roomIndex == currentRoom)
                room.OpenExit();
        }
    }
}
