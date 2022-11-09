using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;


    void Start()
    {
        currentHealth = maxHealth;  //HP gracza
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))    //Zbindowany damage żeby sprawdzać czy healthbar działa.
        {
            TakeDamage(20);
        }
        healthBar.GetComponent<HealthBar>().SetHealth(currentHealth);
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;  //Current health - dmg chyba kurwa każdy rozumie.
        healthBar.SetHealth(currentHealth);
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);    //Zapisuje stan gracza.
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();  //Wczytuje stan danych gracza.

        maxHealth = data.fullHealth;
        currentHealth = data.Health;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        transform.position = position;
    }
}
