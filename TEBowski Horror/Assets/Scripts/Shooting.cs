using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firepoint;
    public GameObject projectilePrefab;

    public float projectileForce = 20f; //siła pocisku nichuja nwm o co chodzi

    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) //Szczylamy spacją pif.. paf.. puf..
        {
            Shoot();
        }
    }
    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firepoint.position, firepoint.rotation); //Skrypt napierdalania z laserka.
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(firepoint.up * projectileForce, ForceMode2D.Impulse);
    }
}
