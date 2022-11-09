using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	//public CapsuleCollider2D characterCollider;
	//public CapsuleCollider2D characterBlockerCollider;

	public Animator animator;
	bool onHit;

	// Start is called before the first frame update
	void Start()
	{
		//Physics2D.IgnoreCollision(characterCollider, characterBlockerCollider, true);
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	//private void OnCollisionEnter2D(Collision2D collision)
	//{
	//	if (!collision.gameObject.CompareTag("Bullet") && !collision.gameObject.CompareTag("Player"))
	//	{
	//		Destroy(this.gameObject);
	//	}
	//}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!collision.gameObject.CompareTag("Bullet") && !collision.gameObject.CompareTag("Player"))
		{
			Destroy(this.gameObject);
		}
	}
}
