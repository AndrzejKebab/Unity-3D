using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
	private Camera cam;
	private Rigidbody2D playerRB;
	[SerializeField] private Transform rotationPoint;
	[SerializeField] private Transform firePoint;
	[SerializeField] private GameObject bulletPrefab;

	private float inputX;
	private float inputY;
	float fire;
	Vector2 dir;
	Vector2 cursorPos;

	//[SerializeField] private int health = 250;
	[SerializeField] private float moveSpeed = 10f;
	[SerializeField] private float bulletForce = 20f;
	private float timeBtwShoots;
	[SerializeField] private float startTimeBtwShoots;

	void Start()
	{
		playerRB = GetComponent<Rigidbody2D>();
		cam = Camera.main;
	}

	void Update()
	{
		Vector2 objPos = cam.WorldToScreenPoint(transform.position);
		dir = (cursorPos - objPos).normalized;
	}

	void FixedUpdate()
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

	public void Move(InputAction.CallbackContext ctx)
	{
		inputX = ctx.ReadValue<Vector2>().x;
		inputY = ctx.ReadValue<Vector2>().y;
	}

	public void Look(InputAction.CallbackContext ctx)
	{
		cursorPos = ctx.ReadValue<Vector2>();
	}

	public void Fire(InputAction.CallbackContext ctx)
	{
		fire = ctx.ReadValue<float>();
	}
}
