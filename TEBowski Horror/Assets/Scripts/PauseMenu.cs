using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))   //Wywołanie menu pauzy poprzez naciśnięce ESC

        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()    //Wyłącza menu pauzy i wznawia czas
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()    //Włącza menu pauzy i zatrzymuje czas ( I am the Time Lord)
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Load()
    {
        //Cumming soon
    }

    public void Save()
    {
        //Cumming soon
    }

    public void Menu() //Powrót do menu głównego, i wznowienie czasu
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
