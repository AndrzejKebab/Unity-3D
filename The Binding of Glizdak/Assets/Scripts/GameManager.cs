using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

	public static int healAmount = 1;

	public static GameManager instance;
	public GameObject DeathScreen;

	private static float health = 8;
	private static int maxHealth = 8;
	private static float moveSpeed = 5;
	private static float fireRate = 0.5f;
	private static float bulletSize = 0.2f;

	public static float Health { get => health; set => health = value; }
	public static int MaxHealth { get => maxHealth; set => maxHealth = value; }
	public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
	public static float FireRate { get => fireRate; set => fireRate = value; }
	public static float BulletSize { get => bulletSize; set => bulletSize = value; }

	public Text HealthText;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}

	}

	// Update is called once per frame
	void Update()
	{
		HealthText.text = "Health: " + health; //nie ustawia hp, hp ca³y czas wynosi 0 zamiast 10

		if(health == 0)
		{
			Time.timeScale = 0f;
			DeathScreen.SetActive(true);
		}
	}

	public static void DamagePlayer(int damage)
	{
		health -= damage;

		if (Health <= 0)
		{
			KillPlayer();
		}
		
	}

	public static void HealPlayer(float healAmount)
	{
		Health = Mathf.Min(maxHealth, health + healAmount);
	}

	public static void MoveSpeedChange(float speed)
	{
		moveSpeed += speed;
	}

	public static void FireRateChange(float firerate)
	{
		fireRate -= firerate;
	}

	public static void BulletSizeChange(float bulletsize)
	{
		bulletSize += bulletsize;
	}

	private static void KillPlayer()
	{

	}
}
