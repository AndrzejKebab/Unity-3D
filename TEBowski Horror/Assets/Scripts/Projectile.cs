using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject hitEffect;

    void OnCollisionEnter2D(Collision2D collision)      //Bullet goes brrrrrr
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f); //Niszczy efekt po 5 sekundach. W teori bo w praktyce nie dzia³a XD.
        Destroy(gameObject); 
    }
}