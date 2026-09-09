using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuScript : MonoBehaviour
{
    public GameObject SettingsMenu;
    public Slider VolumeSlider;
    public TextMeshProUGUI VolumeText; // Change this from TextMesh to Text

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenSettings()
    {
        SettingsMenu.SetActive(true);
        VolumeSlider.gameObject.SetActive(true); // Access the GameObject of the slider
        VolumeText.gameObject.SetActive(true);  // Access the GameObject of the text
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void CloseSettings()
    {
        SettingsMenu.SetActive(false);
        VolumeSlider.gameObject.SetActive(false);
        VolumeText.gameObject.SetActive(false);
    }
}

