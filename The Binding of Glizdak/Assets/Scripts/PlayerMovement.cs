using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
	public Rigidbody2D rigidbody;
	public float speed;
	public Text CollectedText;
	public static int CollectedAmount = 0;
	public GameObject bulletPrefab;
	public float bulletSpeed;
	public float firedelay;
	private float lastFire;

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		firedelay = GameManager.FireRate;
		speed = GameManager.MoveSpeed;

		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

		float shoothorizontal = Input.GetAxis("ShootHorizontal");
		float shootvertical = Input.GetAxis("ShootVertical");

		if((shoothorizontal != 0 || shootvertical != 0) && Time.time > lastFire + firedelay)
		{
			Shoot(shoothorizontal, shootvertical);
			lastFire = Time.time;
		}

		rigidbody.velocity = new Vector3(horizontal * speed, vertical * speed, 0);

		CollectedText.text = "Items Collected: " + CollectedAmount;
		
	}
	
	void Shoot(float x, float y) // yyyyyyyyyy nwm cos chujowo dzia³a raz strzela a raz nie xd
	{
		GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
		bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
		bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
			(x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
			(y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed,
			0			
		);
	}
}
