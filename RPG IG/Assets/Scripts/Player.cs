using PatataStudio.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	private Camera cam;
	private Rigidbody2D playerRB;
	[SerializeField] private Transform rotationPoint;
	[SerializeField] private Transform firePoint;
	[SerializeField] private GameObject bulletPrefab;
	[SerializeField] private Slider healtBar;

	private Vector2 movement;
	private Vector2 dir;
	private Vector2 cursorPos;

	private int health;
	[SerializeField] private int maxHealth = 250;
	[SerializeField] private float moveSpeed = 10f;
	[SerializeField] private float bulletForce = 20f;
	[SerializeField] private float startTimeBtwShoots;
	private float timeBtwShoots;

	void Start()
	{
		playerRB = GetComponent<Rigidbody2D>();
		cam = Camera.main;

		health = maxHealth;
		healtBar.maxValue = maxHealth;
	}

	void Update()
	{
		cursorPos = InputManager.instance.GetMousePos();
		Vector2 objPos = cam.WorldToScreenPoint(transform.position);
		dir = (cursorPos - objPos).normalized;

		healtBar.value = health;

		if (Input.GetKeyDown(KeyCode.G)) health -= 10;
	}

	void FixedUpdate()
	{
		movement = InputManager.instance.GetMovement();

		playerRB.velocity = new Vector2( movement.x * moveSpeed, movement.y * moveSpeed);

		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		rotationPoint.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

		if (timeBtwShoots <= 0)
		{
			if (InputManager.instance.GetFirePressed() == 1)
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
}
