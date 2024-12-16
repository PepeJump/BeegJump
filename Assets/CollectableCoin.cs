using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCoin : Enemy
{
    public int isCollected = 0;
    [SerializeField] private string coinID; // Unique identifier for this coin
    [SerializeField] private AudioClip coinCollectedSFX;

    private void Awake()
    {
        if (string.IsNullOrEmpty(coinID))
        {
            // Assign a unique identifier based on the coin's position if not set
            coinID = $"{transform.position.x}_{transform.position.y}_{transform.position.z}";
        }

        LoadCoin();
    }

    private void Start()
    {
        // Set the active state based on whether this specific coin has been collected
        gameObject.SetActive(isCollected == 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CollectCoin();
        }
    }

    private void CollectCoin()
    {
        // Award money to the player with the money sprite animation
        MoneyManager.Instance.AddMoney(moneyReward, transform.position);

        isCollected = 1;

        AudioManager.instance.PlaySoundSFX(coinCollectedSFX);

        SaveCoin();

        gameObject.SetActive(false);
    }

    private void SaveCoin()
    {
        PlayerPrefs.SetInt(coinID, isCollected);
        PlayerPrefs.Save();
    }

    private void LoadCoin()
    {
        isCollected = PlayerPrefs.GetInt(coinID, 0);
    }
}
