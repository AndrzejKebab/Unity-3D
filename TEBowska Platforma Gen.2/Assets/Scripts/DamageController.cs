using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    [SerializeField] private float enemyDamage;
    [SerializeField] private PlayerMovement healthController;
    [SerializeField] private Animator animator;
    public bool Dmg = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Damage();
        }
    }

    void Damage()
    {
        healthController.playerHealth -= enemyDamage;
        healthController.UpdateHealth();
        //this.gameObject.SetActive(false); //XDDD na chuj to bruh, raz jest Destroy a raz to XD
        (gameObject.GetComponent(typeof(Collider2D)) as Collider2D).enabled = false;
        animator.SetTrigger("Death");
        //Destroy(this.gameObject);
    }
}
