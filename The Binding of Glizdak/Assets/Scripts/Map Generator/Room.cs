using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
	public int Width;
	public int Height;
	public int X;
	public int Y;
	private bool UpdatedDoors = false;
	public Room(int x, int y)
	{
		X = x;
		Y = y;
	}

	public Door doorLeft;
	public Door doorRight;
	public Door doorTop;
	public Door doorBottom;

	public List<Door> doors = new List<Door>();

	// Start is called before the first frame update
	void Start()
	{
		if(RoomController.instance == null)
		{
			Debug.Log("ding dong Something is wrong");
			return;
		}

		Door[] ds = GetComponentsInChildren<Door>();
		foreach(Door d in ds)
		{
			doors.Add(d);
			switch (d.doorType)
			{
				case Door.DoorType.left:
					doorLeft = d;
					break;
				case Door.DoorType.right:
					doorRight = d;
					break;
				case Door.DoorType.top:
					doorTop = d;
					break;
				case Door.DoorType.bottom:
					doorBottom = d;
					break;
			}
		}

		RoomController.instance.RegisterRoom(this);
	}

	void Update()
	{
		if(name.Contains("Boss") && !UpdatedDoors)
		{
			RemoveUnconnectedDoors();
			UpdatedDoors = true;
		}
	}

	public void RemoveUnconnectedDoors() //yyyy no tak œrednio dzia³a xd
	{
		foreach(Door door in doors)
		{
			switch (door.doorType)
			{
				case Door.DoorType.left:
					if(GetLeft() == null)
						door.gameObject.SetActive(false);
					
					break;
				case Door.DoorType.right:
					if (GetRight() == null)
						door.gameObject.SetActive(false);
					break;
				case Door.DoorType.top:
					if (GetTop() == null)
						door.gameObject.SetActive(false);
					break;
				case Door.DoorType.bottom:
					if (GetBottom() == null)
						door.gameObject.SetActive(false);
					break;
			}
		}
	}

	public Room GetLeft()
	{
		if(RoomController.instance.DoesRoomExist(X - 1, Y))
		{
			return RoomController.instance.FindRoom(X - 1, Y);
		}
		return null;
	}

	public Room GetRight()
	{
		if (RoomController.instance.DoesRoomExist(X + 1, Y))
		{
			return RoomController.instance.FindRoom(X + 1, Y);
		}
		return null;
	}

	public Room GetTop()
	{
		if (RoomController.instance.DoesRoomExist(X, Y + 1))
		{
			return RoomController.instance.FindRoom(X, Y + 1);
		}
		return null;
	}

	public Room GetBottom()
	{
		if (RoomController.instance.DoesRoomExist(X, Y - 1))
		{
			return RoomController.instance.FindRoom(X, Y - 1);
		}
		return null;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(transform.position, new Vector3(Width, Height, 0));
	}

	public Vector3 GetRoomCentre()
	{
		return new Vector3(X * Width, Y * Height);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			RoomController.instance.OnPlayerEnterRoom(this);
		}
	}
}
