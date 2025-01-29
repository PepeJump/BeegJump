using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefsHandler : MonoBehaviour
{
    //public Button clearAllButton; // Button to clear all PlayerPrefs
    //public Button clearLevelButton; // Button to clear a specific level progress (e.g., Level 1)
    public bool clearLevels = false;

    void Start()
    {
        //// Assign methods to button clicks
        //clearAllButton.onClick.AddListener(ClearAllProgress);
        //clearLevelButton.onClick.AddListener(() => ClearLevelProgress(1)); // Example for Level 1
    }
    private void Update()
    {
        ClearAllProgress();
    }

    // Method to clear all PlayerPrefs
    void ClearAllProgress()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("All player progress has been cleared.");
        }
    }

    // Method to clear specific level progress
    void ClearLevelProgress(int levelIndex)
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            PlayerPrefs.DeleteKey("Level" + levelIndex + "Unlocked");
            Debug.Log("Level " + levelIndex + " progress has been cleared.");
        }
    }
}
