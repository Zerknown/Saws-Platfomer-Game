using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatformSpawnerManager : MonoBehaviour
{

    [SerializeField] private GameObject[] fallingPlatforms;
    [SerializeField] private float respawnTimer;
    private List<float> timers;

    private float currentTime;
     
    // Start is called before the first frame update
    void Start()
    {
        timers = new List<float>(new float[fallingPlatforms.Count()]);
        //for (int i = 0; i < timers.Count; i++)
        //{
          //  timers[i] = respawnTimer;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < fallingPlatforms.Count(); i++)
        {
            if (!fallingPlatforms[i].activeSelf)
            {
                timers[i] += Time.deltaTime;
                if (timers[i] >= respawnTimer)
                {
                    fallingPlatforms[i].SetActive(true);
                    timers[i] = 0;
                }
            }
        }
    }
}
