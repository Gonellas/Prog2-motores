using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] private CursorLockMode _lockMode;
    [SerializeField] private bool _isCursorVisible;

    private bool isPaused = false;

    private void Start()
    {
        _lockMode = Cursor.lockState;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        isPaused = !isPaused;
        PauseMenu(isPaused);
    }

    public void PauseMenu(bool pause)
    {
        if (pause)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            _lockMode = Cursor.lockState;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = _isCursorVisible;

        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            Cursor.lockState = _lockMode;
            Cursor.visible = false;
        }
    }

    public void LoadMainMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
