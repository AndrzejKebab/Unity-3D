using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	
	public void StartGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //No ogólnie to start gry. Ładuje scene 1.
	}

	public void Exit()
	{
		Debug.Log("Exit");  //Przycisk exit.
		Application.Quit();
	}
}
