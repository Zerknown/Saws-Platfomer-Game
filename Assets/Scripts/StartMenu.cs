using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private AudioSource buttonClickSound;
    [SerializeField] private GameObject creditsScreenGameObject;
    [SerializeField] private GameObject OptionsScreenGameObject;

    public void StartGame()
    {
        buttonClickSound.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoToOptionsScreen()
    {
        buttonClickSound.Play();
        OptionsScreenGameObject.SetActive(true);
    }

    public void GoToMainMenu()
    {
        buttonClickSound.Play();
        OptionsScreenGameObject.SetActive(false);
        creditsScreenGameObject.SetActive(false);
    }

    public void GoToCreditsScreen()
    {
        buttonClickSound.Play();
        creditsScreenGameObject.SetActive(true);
    }

    public void Quit()
    {
        buttonClickSound.Play();
        Application.Quit();
    }

}
