using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
	public enum DoorType
	{
		left,
		right,
		top,
		bottom
	}

	public DoorType doorType;

	public GameObject doorCollider;
	private GameObject Player;
	private float widthOffset = 4f;

	private void Start()
	{
		Player = GameObject.FindGameObjectWithTag("Player");
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			switch (doorType)
			{
				case DoorType.bottom:
					Player.transform.position = new Vector2(transform.position.x, transform.position.y - widthOffset);
					break;
				case DoorType.top:
					Player.transform.position = new Vector2(transform.position.x, transform.position.y + widthOffset);
					break;
				case DoorType.left:
					Player.transform.position = new Vector2(transform.position.x - widthOffset, transform.position.y - widthOffset);
					break;
				case DoorType.right:
					Player.transform.position = new Vector2(transform.position.x + widthOffset, transform.position.y - widthOffset);
					break;
			}
		}
	}
}
