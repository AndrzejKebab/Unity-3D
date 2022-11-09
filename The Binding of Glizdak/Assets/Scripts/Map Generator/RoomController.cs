using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomInfo
{
	public string name;
	public int X;
	public int Y;
}

public class RoomController : MonoBehaviour
{
	public static RoomController instance;

	string currnetWorldName = "Basement";

	RoomInfo currentLoadRoomData;

	Room currentRoom;

	Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();

	public List<Room> loadedRooms = new List<Room>();

	bool IsLoadingRoom = false;
	bool SpawnedBossRoom = false;
	bool UpdatedRooms = false;

	void Awake()
	{
		instance = this;
	}

	void Update()
	{
		UpdateRoomQueue();
	}

	void UpdateRoomQueue()
	{
		if (IsLoadingRoom)
		{
			return;
		}

		if(loadRoomQueue.Count == 0)
		{
			if (!SpawnedBossRoom)
			{
				StartCoroutine(SpawnBossRoom());
			}
			else if(SpawnedBossRoom && !UpdatedRooms)
			{
				foreach(Room room in loadedRooms)
				{
					room.RemoveUnconnectedDoors();
				}
				UpdateRooms();
				UpdatedRooms = true;
			}
			return;
		}

		currentLoadRoomData = loadRoomQueue.Dequeue();
		IsLoadingRoom = true;

		StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
	}

	IEnumerator SpawnBossRoom()
	{
		SpawnedBossRoom = true;
		yield return new WaitForSeconds(0.5f);
		if(loadRoomQueue.Count == 0)
		{
			Room bossRoom = loadedRooms[loadedRooms.Count - 1];
			Room tempRoom = new Room(bossRoom.X, bossRoom.Y);
			Destroy(bossRoom.gameObject);
			var roomToRemove = loadedRooms.Single(r => r.X == tempRoom.X && r.Y == tempRoom.Y);
			loadedRooms.Remove(roomToRemove);
			LoadRoom("Boss", tempRoom.X, tempRoom.Y);
		}
	}


	public void LoadRoom(string name, int x, int y)
	{
		if(DoesRoomExist(x, y))
		{
			return;
		}

		RoomInfo newRoomData = new RoomInfo();
		newRoomData.name = name;
		newRoomData.X = x;
		newRoomData.Y = y;

		loadRoomQueue.Enqueue(newRoomData);
	}

	IEnumerator LoadRoomRoutine(RoomInfo info)
	{
		string roomName = currnetWorldName + info.name;
		AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);

		while (loadRoom.isDone == false)
		{
			yield return null;
		}
	}

	public void RegisterRoom(Room room)
	{
		if(!DoesRoomExist(currentLoadRoomData.X, currentLoadRoomData.Y))
		{
			room.transform.position = new Vector3(currentLoadRoomData.X * room.Width, currentLoadRoomData.Y * room.Height, 0);
			room.X = currentLoadRoomData.X;
			room.Y = currentLoadRoomData.Y;
			room.name = currnetWorldName + "-" + currentLoadRoomData + " " + room.X + ", " + room.Y;
			room.transform.parent = transform;

			IsLoadingRoom = false;

			if (loadedRooms.Count == 0)
			{
				CameraController.instance.currentRoom = room;
			}

			loadedRooms.Add(room);
		}
		else
		{
			Destroy(room.gameObject);
			IsLoadingRoom = false;
		}
	}

	public bool DoesRoomExist(int x, int y)
	{
		return loadedRooms.Find(item => item.X == x && item.Y == y) != null;
	}

	public Room FindRoom(int x, int y)
	{
		return loadedRooms.Find(item => item.X == x && item.Y == y);
	}

	public string GetRandomRoomName()
	{
		string[] possibleRooms = new string[]
		{
			"Empty",
			"Basic1"
		};

		return possibleRooms[Random.Range(0, possibleRooms.Length)];
	}

	public void OnPlayerEnterRoom(Room room)
	{
		CameraController.instance.currentRoom = room;
		currentRoom = room;

		StartCoroutine(RoomCoroutine());
	}

	public IEnumerator RoomCoroutine()
	{
		yield return new WaitForSeconds(0.2f);
		UpdateRooms();
	}

	public void UpdateRooms()
	{
		foreach(Room room in loadedRooms)
		{
			if(currentRoom != room)
			{
				EnemyController[] enemies = room.GetComponentsInChildren<EnemyController>();
				if(enemies != null)
				{
					foreach(EnemyController enemy in enemies)
					{
						enemy.NotInRoom = true;
						Debug.Log("Not in Room");
					}

					foreach(Door door in room.GetComponentsInChildren<Door>())
					{
						door.doorCollider.SetActive(false);
					}
				}
				else
				{
					foreach (Door door in room.GetComponentsInChildren<Door>())
					{
						door.doorCollider.SetActive(false);
					}
				}
			}
			else
			{
				EnemyController[] enemies = room.GetComponentsInChildren<EnemyController>();
				if (enemies.Length > 0)
				{
					foreach (EnemyController enemy in enemies)
					{
						enemy.NotInRoom = false;
						Debug.Log("In Room");
					}
					foreach (Door door in room.GetComponentsInChildren<Door>())
					{
						door.doorCollider.SetActive(true);
					}
				}
				else
				{
					foreach (Door door in room.GetComponentsInChildren<Door>())
					{
						door.doorCollider.SetActive(false);
					}
				}
			}
		}
	}
}
