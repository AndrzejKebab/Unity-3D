using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int Health;
    public int fullHealth;
    public float[] position;

    public PlayerData(PlayerHealth player)  //Dane gracza przy zapisywaniu stanu gry.
    {
        fullHealth = player.maxHealth; //HP gracza.
        Health = player.currentHealth;

        position = new float[3]; //Pozycja gracza względem osi x,y i z.
        position[0] = player.transform.position.x; 
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }

}
