using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{
	public float lifeTime;
	public bool IsEnemyBullet = false;

	private Vector2 lastPos;
	private Vector2 currentPos;
	private Vector2 playerPos;
	// Start is called before the first frame update
	void Start()
	{
		StartCoroutine(DeathDelay());
		transform.localScale = new Vector2(GameManager.BulletSize, GameManager.BulletSize);
	}

	// Update is called once per frame
	void Update()
	{
		if (IsEnemyBullet)
		{
			currentPos = transform.position;
			transform.position = Vector2.MoveTowards(transform.position, playerPos, 5f * Time.deltaTime);
			if (currentPos == lastPos)
			{
				Destroy(this.gameObject);
			}
			lastPos = currentPos;
		}
	}

	public void GetPlayer(Transform Player)
	{
		playerPos = Player.position;
	}

	IEnumerator DeathDelay()
	{
		yield return new WaitForSeconds(lifeTime);
		Destroy(this.gameObject);
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Enemy" && !IsEnemyBullet)
		{
			col.gameObject.GetComponent<EnemyController>().Death();
			Destroy(this.gameObject);
		}

		if(col.tag == "Player" && IsEnemyBullet)
		{
			GameManager.DamagePlayer(1);
			Destroy(this.gameObject);
		}
	}
}
