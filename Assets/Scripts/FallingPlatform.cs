using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{

    [SerializeField] private float respawnTimer;
    [SerializeField] private int delayToFallSeconds;
    [SerializeField] private float timeForFallSeconds;
    [SerializeField] private int fallSpeed;
    
    private Animator anim;
    private bool fallDownState = false, despawnedState;
    private Vector3 currentSpawnPosition;
    private float currentTime;

    private void Awake()
    {
        anim = GetComponent<Animator>();        
        despawnedState = false;
        anim.Play("FallingPlatform");
        currentSpawnPosition = transform.position;
    }

    private void OnEnable()
    {
        transform.position = currentSpawnPosition;
        anim.Play("FallingPlatform");
    }

    private void Update()
    {
        if (fallDownState)
        {
            transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            StartCoroutine(startFalling());
        }
    }

    IEnumerator startFalling()
    {
        yield return new WaitForSeconds(delayToFallSeconds);

        anim.Play("PlatformFalling");
        fallDownState = true;
        yield return new WaitForSeconds(timeForFallSeconds);
        //Destroy(gameObject);
        fallDownState = false;
        gameObject.SetActive(false);
        //despawnedState = true;
    }
}
