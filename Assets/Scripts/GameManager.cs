using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isCoopMode = false;
    [SerializeField]
    private bool _isGameOver = false;

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
    }

    public void GameOver()
    {
        _isGameOver = true;
    }
}
