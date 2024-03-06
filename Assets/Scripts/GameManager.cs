using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isCoopMode = false;
    [SerializeField]
    private bool _isGameOver = false;

    [SerializeField]
    private GameObject _pauseMenuPanel;

    private void Update()
    {
        if (_isGameOver)
        {
            if (Input.GetKeyUp(KeyCode.R) && !isCoopMode)
            {
                SceneManager.LoadScene(1); // Single Player
            }
            if (Input.GetKeyUp(KeyCode.R) && isCoopMode)
            {
                SceneManager.LoadScene(2); // Co-Op
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(0); // Main Menu
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            _pauseMenuPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }

    public void ResumeGame()
    {
        _pauseMenuPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
