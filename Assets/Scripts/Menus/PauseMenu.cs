using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        // Locks cursor again
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }

    public void Pause() 
    {
        pauseMenuUI.SetActive(true);
        // Freezes time
        Time.timeScale = 0f;
        GameIsPaused = true;

        // Allows cursor movement
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}
