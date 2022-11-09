using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{

	public int Health = 1;
	public float Speed = 3;
	//public GameObject Player;
	public Animator animator;
	[SerializeField] GameObject Player;

	// Start is called before the first frame update
	void Start()
	{
		Player = GameObject.Find("Player");
	}

	private void FixedUpdate()
	{
		Vector3 direction = Player.transform.position - transform.position;
		direction.Normalize();
		transform.position += direction * Time.fixedDeltaTime * Speed;
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			Health--;
		}
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "Bullet")
		{
			(gameObject.GetComponent(typeof(Collider2D)) as Collider2D).enabled = false;
			animator.SetTrigger("Death");
		}
	}

	private void Death()
	{
		Destroy(this.gameObject);
	}
}
