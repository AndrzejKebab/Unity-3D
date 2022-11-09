using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour //Ogólnie to skrypt do ekranu ³adowania ale coœ nie dzia³a
{   
    public GameObject loadingScreen;
    public Slider slider;

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex)); // Nimam pojêcia
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true); //Aktywuje ekran ³adowania

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f); //Oblicza progress ³adowania

            slider.value = progress;

            yield return null; 
        }
    }
}
