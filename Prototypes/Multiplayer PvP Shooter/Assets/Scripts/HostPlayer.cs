using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class HostPlayer : NetworkBehaviour
{
	private float inputX;
	private float inputY;

	//public CapsuleCollider2D characterCollider;
	//public CapsuleCollider2D characterBlockerCollider;

	private Camera cam;
	private Rigidbody2D playerRB;
	public Transform rotationPoint;
	public Transform firePoint;
	public GameObject bulletPrefab;

	public int health = 250;

	private float timeBtwShoots;
	public float startTimeBtwShoots;
	public float bulletForce = 20f;

	Vector2 dir;
	Vector2 cursorPos;
	float fire;

	public float moveSpeed = 5f;

	public override void OnNetworkSpawn()
	{
		if (!IsOwner) Destroy(this);
	}

	// Start is called before the first frame update
	void Start()
	{
		playerRB = GetComponent<Rigidbody2D>();
		cam = Camera.main;
		//Physics2D.IgnoreCollision(characterCollider, characterBlockerCollider, true);
	}

	// Update is called once per frame
	void Update()
	{
		Vector2 objPos = cam.WorldToScreenPoint(transform.position);
		dir = (cursorPos - objPos).normalized;

	}

	private void FixedUpdate()
	{
		playerRB.velocity = new Vector2(inputX * moveSpeed, inputY * moveSpeed);

		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		rotationPoint.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

		if (timeBtwShoots <= 0)
		{
			if (fire == 1)
			{
				Shoot();
				timeBtwShoots = startTimeBtwShoots;
			}
		}
		else
		{
			timeBtwShoots -= Time.deltaTime;
		}
	}

	void Shoot()
	{
		GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
		bulletRB.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
		Destroy(bullet, 5f);
	}

	public void Move(InputAction.CallbackContext context)
	{
		inputX = context.ReadValue<Vector2>().x;
		inputY = context.ReadValue<Vector2>().y;

	}

	public void Look(InputAction.CallbackContext context)
	{
		cursorPos = context.ReadValue<Vector2>();
	}

	public void Fire(InputAction.CallbackContext context)
	{
		fire = context.ReadValue<float>();
	}
}
