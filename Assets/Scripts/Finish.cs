using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Finish : MonoBehaviour
{
    private AudioSource finishSound;

    [SerializeField] private ItemCollector collector;
    [SerializeField] private int passingItemCount;
    [SerializeField] private int maxItemCount;

    [SerializeField] private GameObject tempText;
    [SerializeField] private TextMeshProUGUI text;

    private bool levelCompleted = false;
    private void Start()
    {
        finishSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.name == "Player" && !levelCompleted)
        {
            if (collector == null)
            {
                Debug.Log("Collector Script is not added");
            }
            else
            {
                if (collector.collectableItems > passingItemCount)
                {
                    finishSound.Play();
                    levelCompleted = true;
                    Invoke("CompleteLevel", 2f);
                }
                else
                {
                    // You needed x amount out of max amount to go to the next level.
                    StartCoroutine(showTempText());
                }
                
            }
            
            
        }
    }

    IEnumerator showTempText()
    {
        tempText.SetActive(true);
        text.text = "You need " + passingItemCount + " collectable items to pass";
        yield return new WaitForSeconds(5);
        tempText.SetActive(false);
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
