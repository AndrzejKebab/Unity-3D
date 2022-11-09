using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public new Rigidbody2D rigidbody;
    public float speed;
    public float level = 1;
    public readonly float baseHealth = 150;
    public float bonusHealth = 0;
    public float maxHealth;
    public float currentHealth;
    public float currentExp = 0;
    public float reqExp = 100;
    public GameObject player;
	public GameObject bullet;
    public Transform firePoint;
	public Transform firePointparent;
    public float bulletspeed = 50;
    public Text health;
    public Text exp;
    private static float multiplier = 2.25f;

    Vector2 lookDir;
    float lookAngle;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = baseHealth + bonusHealth + (baseHealth / 4 * level);
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        rigidbody.velocity = new Vector3(horizontal * speed, vertical * speed, 0);

        lookDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(player.transform.position.x, player.transform.position.y);
        lookAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        firePointparent.rotation = Quaternion.Euler(0, 0, lookAngle);

		if (Input.GetMouseButtonDown(0))
		{
            GameObject bulletClone = Instantiate(bullet);
            bulletClone.transform.position = firePoint.position;
            bulletClone.transform.rotation = Quaternion.Euler(0, 0, lookAngle);
            bulletClone.GetComponent<Rigidbody2D>().velocity = firePoint.right * bulletspeed;
		}
        maxHealth = baseHealth + bonusHealth + (baseHealth / 4 * level);
        maxHealth = Mathf.RoundToInt(maxHealth);
        currentHealth = Mathf.RoundToInt(currentHealth);
        health.text = currentHealth + " / " + maxHealth;

        exp.text = currentExp + " / " + reqExp + " Level: " + level;

        if (currentExp >= reqExp)
		{
            reqExp = Mathf.Pow(reqExp,multiplier/2);
            reqExp = Mathf.RoundToInt(reqExp);
            level++;
		}
    }
}
