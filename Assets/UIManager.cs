using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public bool isPaused = false;
    private bool canBePaused = true;
    private bool isFailMessageDisplayed = false;

    public GameObject pauseMenu;
    public GameObject failMessage;
    public GameObject successMessage;

    private GameObject player;

    // MARK:- PUBLIC METHODS
    void Start()
    {
        Time.timeScale = 1;
        player = GameObject.Find("Ninja");
        canBePaused = true;
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex != 0) {
            if (player.GetComponent<PlayerCharacterController>().isDead == true & !isFailMessageDisplayed & !Input.GetKey(KeyCode.R))
            {
                isFailMessageDisplayed = true;
                failMessage.SetActive(true);
                Time.timeScale = 0;
                canBePaused = false;
            }

            if (isFailMessageDisplayed & Input.GetKeyDown(KeyCode.R))
            {
                Time.timeScale = 1;
                failMessage.SetActive(false);
                isFailMessageDisplayed = false;
                canBePaused = true;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {

                if (isPaused)
                {
                    Unpause();
                }
                else if (canBePaused)
                {
                    Pause();
                }
            }
        }

    }

    public void Pause()
    {
        Time.timeScale = 0;
        isPaused = true;
        pauseMenu.SetActive(true);
    }

    public void Unpause()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ShowSuccessMessage()
    {
        successMessage.SetActive(true);
        Time.timeScale = 0;
        canBePaused = false;
    }
    public void loadLevel(int levelNumber)
    {
        SceneManager.LoadScene(levelNumber);
    }

    public void quit()
    {
        Application.Quit();
    }

}
