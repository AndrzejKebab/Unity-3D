using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Item
{
	public string Name;
	public string description;
	public Sprite itemImage;
}

public class ItemController : MonoBehaviour
{
	public Item item;
	public float healthChange;
	public float moveSpeedChange;
	public float attackSpeed;
	public float bulletSizeChange;

	private void Start()
	{
		GetComponent<SpriteRenderer>().sprite = item.itemImage;
		Destroy(GetComponent<PolygonCollider2D>());
		gameObject.AddComponent < PolygonCollider2D>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Player")
		{
			PlayerMovement.CollectedAmount++;
			GameManager.HealPlayer(healthChange);
			GameManager.MoveSpeedChange(moveSpeedChange);
			GameManager.FireRateChange(attackSpeed);
			GameManager.BulletSizeChange(bulletSizeChange);
			Destroy(this.gameObject);
		}

	}
}
