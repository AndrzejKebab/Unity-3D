using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    Resolution[] resolutions;
    public Dropdown ResolutionDropdown;


    private void Start() //zmienne odpowiedzialne za obliczanie rozdzielczości i odświerzania ekranu na uruchomionym kompie
    {
        
        int CurrentResolutionIndex = 0;
        resolutions = Screen.resolutions;

        ResolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)    
        {
            string Option = resolutions[i].width + " x " + resolutions[i].height + " HZ" + resolutions[i].refreshRate;
            options.Add(Option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                CurrentResolutionIndex = i;
            }
        }
        //Ustawia opcje wyboru w Menu rozdzielczości
        ResolutionDropdown.AddOptions(options);
        ResolutionDropdown.value = CurrentResolutionIndex;
        ResolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int ResolutionIndex) //ustawia wybraną rozdzielczość
    {
        Resolution resolution = resolutions[ResolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float volume) // ustawia wybraną ogólną głośność gry
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetFullscreen(bool isFullscreen) //ustawia fullscreen
    {
        Screen.fullScreen = isFullscreen;
    }
}