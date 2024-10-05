using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    
    // Static boolean to keep track of the game's pause state globally
    public static bool GameIsPaused = false;

    private bool isAtOptionsScreen = false;

    [SerializeField] private GameObject optionsPanelGameObject;
    [SerializeField] private GameObject pauseMenuGameObject;
    [SerializeField] private AudioSource buttonSound;
    
    // Update is called once per frame
    void Update()
    {
        if (!isAtOptionsScreen)
        {
            // Check if the Escape key is pressed to toggle pause
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
    }
            

    // Method to resume the game
    public void Resume()
    {
        buttonSound.Play();
        pauseMenuGameObject.SetActive(false); // Hide the pause menu UI
        Time.timeScale = 1; // Resume the game 
        GameIsPaused = false; // Update the static pause state
    }

    // Method to pause the game 
    void Pause()
    {
        buttonSound.Play();
        pauseMenuGameObject.SetActive(true); // Show the pause menu UI
        Time.timeScale = 0; // Pause the game time
        GameIsPaused = true;
    }

    public void ResumeGame()
    {
        Resume();
    }

    // Method to go to Options Panel
    public void GoToOptionsScreen()
    {
        buttonSound.Play();
        isAtOptionsScreen = true;
        optionsPanelGameObject.SetActive(true);
    }

    public void GoBackToPauseMenu()
    {
        buttonSound.Play();
        isAtOptionsScreen = false;
        optionsPanelGameObject.SetActive(false);
    }

    // Method to load the main menu scene
    public void LoadMenu()
    {
        buttonSound.Play();
        Time.timeScale = 1; //Ensure the game time is running
        SceneManager.LoadScene(0);
    }

    // Method to quit the game
    public void QuitGame()
    {
        buttonSound.Play();
        Debug.Log("Quitting game...");
        Application.Quit(); // Quit the application
    }
}
