using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCollector : MonoBehaviour
{
   
    public int collectableItems { get; private set; }

    
    [SerializeField] private TMP_Text collectableItemsText;
    [SerializeField] private AudioSource collectionSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CollectableItem"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            collectableItems++;
            collectableItemsText.text = "Collectable Items: " + collectableItems;
        }
    }
}
