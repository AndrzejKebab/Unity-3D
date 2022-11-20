using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
	public float lifeTime = 3f;

	private void Start()
	{
		StartCoroutine(DestroyDelay());
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag != "bullet" )
		{
			Destroy(this.gameObject);
		}
	}

	IEnumerator DestroyDelay()
	{
		yield return new WaitForSeconds(lifeTime);
		Destroy(this.gameObject);
	}
}
