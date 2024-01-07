using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuSettings : MonoBehaviour
{
    public Slider soundSlider,
        musicSlider;

    public Toggle soundToggle,
        musicToggle;

    private bool activateSound;
    private bool activateMusic;

    // Start is called before the first frame update
    void Start()
    {
        float soundValue = PlayerPrefs.GetFloat("SoundSlider", 0);
        soundSlider.value = soundValue;

        float musicValue = PlayerPrefs.GetFloat("MusicSlider", 0);
        musicSlider.value = musicValue;

        int soundActive = PlayerPrefs.GetInt("SoundActive", 0);
        int musicActive = PlayerPrefs.GetInt("MusicActive", 0);

        if (soundActive == 1)
        {
            soundToggle.isOn = true;
            ActiveSettings("sound");
        }

        if (musicActive == 1)
        {
            musicToggle.isOn = true;
            ActiveSettings("music");
        }

        soundToggle.onValueChanged.AddListener(
            (v) =>
            {
                if (v == true)
                {
                    ActiveSettings("sound");
                }
                else
                {
                    ActiveSettings("sound", false);
                }
            }
        );

        musicToggle.onValueChanged.AddListener(
            (v) =>
            {
                if (v == true)
                {
                    ActiveSettings("music");
                }
                else
                {
                    ActiveSettings("music", false);
                }
            }
        );
    }

    public void ActiveSettings(string name, bool isActive = true)
    {
        if (name == "sound")
        {
            activateSound = isActive;
            soundSlider.interactable = isActive;
        }
        else if (name == "music")
        {
            activateMusic = isActive;
            musicSlider.interactable = isActive;
        }
    }

    public void CloseMenu()
    {
        PlayerPrefs.SetFloat("SoundSlider", soundSlider.value);
        PlayerPrefs.SetInt("SoundActive", activateSound ? 1 : 0);
        PlayerPrefs.SetFloat("MusicSlider", musicSlider.value);
        PlayerPrefs.SetInt("MusicActive", activateMusic ? 1 : 0);

        SceneManager.LoadScene("MainMenu");
    }
}
