using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour //Og�lnie to skrypt do ekranu �adowania ale co� nie dzia�a
{   
    public GameObject loadingScreen;
    public Slider slider;

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex)); // Nimam poj�cia
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true); //Aktywuje ekran �adowania

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f); //Oblicza progress �adowania

            slider.value = progress;

            yield return null; 
        }
    }
}
