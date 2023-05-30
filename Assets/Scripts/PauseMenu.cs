using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pauseButtons;
    public GameObject areYouSure;

    public AudioMixer audioMixer;
    public Slider volumeSlider;

    public Slider camSpeedSlider;

    private bool inAreYouSure = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!(PlayerPrefs.GetFloat("musicVol") == 0))
        {
            audioMixer.SetFloat("musicVolume", Mathf.Log10(PlayerPrefs.GetFloat("musicVol")) * 20);
            volumeSlider.value = (PlayerPrefs.GetFloat("musicVol"));
        }

        if (!(PlayerPrefs.GetFloat("effectsVol") == 0))
        {
            audioMixer.SetFloat("effectsVolume", Mathf.Log10(PlayerPrefs.GetFloat("effectsVol")) * 20);
            volumeSlider.value = (PlayerPrefs.GetFloat("effectsVol"));
        }

        if (!(PlayerPrefs.GetFloat("camSpeed") == 0))
        {
            camSpeedSlider.value = PlayerPrefs.GetFloat("camSpeed");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // escape button pauses/resumes game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!PlayerManager.playerInstance.gameIsPaused)
            {
                Pause();
            }

            else
            {
                if (inAreYouSure)
                {
                    Back();
                }

                else
                {
                    Resume();
                }
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        PlayerManager.playerInstance.gameIsPaused = true;
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    public void Resume()
    {
        Time.timeScale = 1f;
        PlayerManager.playerInstance.gameIsPaused = false;
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    public void NewGame()
    {
        Time.timeScale = 1f;
        PlayerManager.playerInstance.gameIsPaused = false;
        pauseMenu.SetActive(false);

        PlayerPrefs.SetInt("cpValue", 0);

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void AreYouSure()
    {
        pauseButtons.SetActive(false);
        areYouSure.SetActive(true);
    }

    public void Back()
    {
        areYouSure.SetActive(false);
        pauseButtons.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // sliders in settings control the volumes in audiomixer

    public void SetMusicVolume(float volume)
    {
        // slider would be logaritmic without the fix
        audioMixer.SetFloat("musicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVol", volume);
    }

    public void SetEffectsVolume(float volume)
    {
        // slider would be logaritmic without the fix
        audioMixer.SetFloat("effectsVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("effectsVol", volume);
    }

    public void SetCamSpeed(float speed)
    {
        PlayerPrefs.SetFloat("camSpeed", speed);
        EventManager.eventInstance.camSpeedX = 75f * speed;
        EventManager.eventInstance.camSpeedY = speed;

        if (EventManager.eventInstance.cineCam.m_XAxis.m_MaxSpeed != 0)
        {
            EventManager.eventInstance.cineCam.m_XAxis.m_MaxSpeed = 75f * speed;
        }

        if (EventManager.eventInstance.cineCam.m_YAxis.m_MaxSpeed != 0)
        {
            EventManager.eventInstance.cineCam.m_YAxis.m_MaxSpeed = speed;
        }
    }
}
