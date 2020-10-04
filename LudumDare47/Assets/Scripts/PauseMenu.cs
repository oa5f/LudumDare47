using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance { get; private set; }
    public bool IsPaused { get; private set; } = false;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject crosshair;

    [SerializeField] private float musicPauseVolume = 0.3f;
    [SerializeField] private AudioSource musicSource;

    private float defaultMusicVolume;

    private void Start()
    {
        defaultMusicVolume = musicSource.volume;
        Instance = this;
        Time.timeScale = 1f;
    }
    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (IsPaused)
                Resume();
            else
                Pause();
        }
    }
    public void Resume()
    {
        musicSource.volume = defaultMusicVolume;
        CursorManager.Instance.Lock();
        pauseMenu.SetActive(false);
        crosshair.SetActive(true);
        IsPaused = false;
        Time.timeScale = 1f;
    }
    public void Pause()
    {
        musicSource.volume = defaultMusicVolume * musicPauseVolume;
        CursorManager.Instance.Unlock();
        crosshair.SetActive(false);
        pauseMenu.SetActive(true);
        IsPaused = true;
        Time.timeScale = 0f;
    }
    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }
}
