using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance { get; private set; }

    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject crosshair;

    public bool IsPaused { get; private set; } = false;
    private void Start()
    {
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
        CursorManager.Instance.Lock();
        pauseMenu.SetActive(false);
        crosshair.SetActive(true);
        IsPaused = false;
        Time.timeScale = 1f;
    }
    public void Pause()
    {
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
