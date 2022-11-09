using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	public GameObject DeathScreen;
	public void Play()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void Exit()
	{
		Debug.Log("Exit");
		Application.Quit();
	}
	public void Restart()
	{
		DeathScreen.SetActive(false);
		SceneManager.LoadScene("Menu");
		Time.timeScale = 1f;
	}
}
