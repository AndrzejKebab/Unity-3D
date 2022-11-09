using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void Menu() //Powrót do menu głównego, i wznowienie czasu
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }

    public void Resume()
	{
        SceneManager.LoadScene("Start");
        Time.timeScale = 1f;
	}
}
