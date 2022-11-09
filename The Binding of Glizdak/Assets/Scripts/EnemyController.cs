using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
	Idle,
	Wander,
	Follow,
	Attack,
	Die
}

public enum EnemyType
{
	Melee,
	Ranged
}

public class EnemyController : MonoBehaviour
{
	GameObject Player;
	public GameObject BulletPrefab;
	public EnemyState CurrentState = EnemyState.Idle;
	public EnemyType enemyType;
	public float aggrorange;
	public float speed;
	public float bulletSpeed;
	public float AttackRange;
	public float coolDown;
	private bool chooseDir = false;
	// bool dead = false;
	private bool coolDownAttack = false;
	public bool NotInRoom = false;
	private Vector3 randomDir;

	// Start is called before the first frame update
	void Start()
	{
		Player = GameObject.FindGameObjectWithTag("Player");
		
	}

	// Update is called once per frame
	void Update()
	{
		switch (CurrentState)
		{
			case (EnemyState.Wander):
				Wander();
				break;
			case (EnemyState.Follow):
				Follow();
				break;
			case (EnemyState.Attack):
				Attack();
				break;
			case (EnemyState.Die):

				break;
			case (EnemyState.Idle):
				Idle();
				break;
		}

		if (!NotInRoom)
		{
			if (IsPlayerInRange(aggrorange) && CurrentState != EnemyState.Die)
			{
				CurrentState = EnemyState.Follow;
			}
			else if (!IsPlayerInRange(aggrorange) && CurrentState != EnemyState.Die)
			{
				CurrentState = EnemyState.Wander;
			}
			if (Vector3.Distance(transform.position, Player.transform.position) <= AttackRange)
			{
				CurrentState = EnemyState.Attack;
			}
		}
		else
		{
			CurrentState = EnemyState.Idle;
		}
		
	}

	private bool IsPlayerInRange(float aggrorange)
	{
		return Vector3.Distance(transform.position, Player.transform.position) <= aggrorange;
	}

	private IEnumerator ChooseDirection()
	{
		chooseDir = true;
		yield return new WaitForSeconds(Random.Range(2f, 8f));
		randomDir = new Vector3(0, 0, Random.Range(0, 300));
		Quaternion nextRotation = Quaternion.Euler(randomDir);
		transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2.5f));
		chooseDir = false;
	}

	private void Idle()
	{

	}

	void Wander()
	{
		if (!chooseDir)
		{
			StartCoroutine(ChooseDirection());
		}

		transform.position += -transform.right * speed * Time.deltaTime;
		if (IsPlayerInRange(aggrorange))
		{
			CurrentState = EnemyState.Follow;
		}
	}

	void Follow()
	{
		transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
	}

	private IEnumerator CoolDown()
	{
		coolDownAttack = true;
		yield return new WaitForSeconds(coolDown);
		coolDownAttack = false;
	}

	void Attack()
	{
		if (!coolDownAttack)
		{
			switch (enemyType)
			{
				case (EnemyType.Melee):
					GameManager.DamagePlayer(1);
					StartCoroutine(CoolDown());
					break;
				case (EnemyType.Ranged):
					GameObject bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity) as GameObject;
					bullet.GetComponent<bulletController>().GetPlayer(Player.transform);
					bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
					bullet.GetComponent<bulletController>().IsEnemyBullet = true;
					StartCoroutine(CoolDown());
					break;
			}
		}
	}

	public void Death()
	{
		RoomController.instance.StartCoroutine(RoomController.instance.RoomCoroutine());
		Destroy(this.gameObject);
	}
}
