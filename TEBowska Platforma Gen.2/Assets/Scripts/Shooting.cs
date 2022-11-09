using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject Player;
    public GameObject Bullet;
    public Transform FirePoint;
    public float bulletSpeed = 50;

    Vector2 lookDirection;
    float lookAngle;

    void Update()
    {
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(Player.transform.position.x, Player.transform.position.y);
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        FirePoint.rotation = Quaternion.Euler(0, 0, lookAngle);

        if (Input.GetMouseButtonDown(0))
        {

            GameObject bulletClone = Instantiate(Bullet);
            bulletClone.transform.position = FirePoint.position;
            bulletClone.transform.rotation = Quaternion.Euler(0, 0, lookAngle);

            bulletClone.GetComponent<Rigidbody2D>().velocity = FirePoint.right * bulletSpeed;

        }
    }
}
