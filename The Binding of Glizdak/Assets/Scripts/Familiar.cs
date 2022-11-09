using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Familiar : MonoBehaviour
{
	private float lastFire;
	private GameObject Player;
	public FamiliarData familiar;
	private float lastOffsetX;
	private float lastOffsetY;

	private void Start()
	{
		Player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update()
	{
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

		float shoothorizontal = Input.GetAxis("ShootHorizontal");
		float shootvertical = Input.GetAxis("ShootVertical");

		if ((shoothorizontal != 0 || shootvertical != 0) && Time.time > lastFire + familiar.fireDelay)
		{
			Shoot(shoothorizontal, shootvertical);
			lastFire = Time.time;
		}

		if(horizontal != 0 || vertical != 0)
		{
			float offsetX = (horizontal < 0) ? Mathf.Floor(horizontal) : Mathf.Ceil(horizontal);
			float offsetY = (vertical < 0) ? Mathf.Floor(vertical) : Mathf.Ceil(vertical);
			transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, familiar.speed * Time.deltaTime);
			lastOffsetX = offsetX;
			lastOffsetY = offsetY;
		}
		else
		{
			if (!(transform.position.x < lastOffsetX + 0.5f) || !(transform.position.y < lastOffsetY + 0.5f))
			{
				transform.position = Vector2.MoveTowards(transform.position, new Vector2(Player.transform.position.x - lastOffsetX, Player.transform.position.y - lastOffsetY), familiar.speed * Time.deltaTime);
			}
		}
	}

	void Shoot(float x, float y)
	{
		GameObject bullet = Instantiate(familiar.bulletPrefab, transform.position, Quaternion.identity) as GameObject;
		float posX = (x < 0) ? Mathf.Floor(x) * familiar.speed : Mathf.Ceil(x) * familiar.speed;
		float posY = (y < 0) ? Mathf.Floor(y) * familiar.speed : Mathf.Ceil(y) * familiar.speed;
		bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
		bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(posX, posY);
	}
}
