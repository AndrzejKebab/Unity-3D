using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_Note : MonoBehaviour
{
	public GameObject DeathScreen;
	public GameObject UI;
	
	private void OnTriggerEnter2D(Collider2D collider)
	{
		DeathScreen.SetActive(true);
		UI.SetActive(false);
		Time.timeScale = 0f;
	}

}

