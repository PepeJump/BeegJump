using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Required for EventSystem

public class UIManager : MonoBehaviour
{
    public static UIManager Instance; // Singleton instance
    public GameObject levelSelectionPanel; // Panel to show level selection
    public GameObject upgradePanel; // Panel to show upgrade buttons
    public Button toggleButton; // Button to toggle the level selection panel
    public Button upgradeToggleButton; // Button to toggle the upgrade panel
    public Button[] levelButtons; // Buttons for each level

    private bool isPanelActive = false;
    private bool isUpgradePanelActive = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Ensure the panel is initially inactive
        levelSelectionPanel.SetActive(false);
        upgradePanel.SetActive(true);


        // Add listener to the toggle button to open/close the panel
        toggleButton.onClick.AddListener(ToggleLevelSelection);
        upgradeToggleButton.onClick.AddListener(ToggleUpgradeSelection);

        // Update the level buttons according to unlocked levels
        UpdateLevelButtons();
    }

    private void ToggleLevelSelection()
    {
        isPanelActive = !isPanelActive;
        levelSelectionPanel.SetActive(isPanelActive);

        if (!isPanelActive)
        {
            // Deselect any currently selected UI element
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    private void ToggleUpgradeSelection()
    {
        isUpgradePanelActive = !isUpgradePanelActive;
        upgradePanel.SetActive(isUpgradePanelActive);

        if (!isUpgradePanelActive)
        {
            // Deselect any currently selected UI element
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public void ShowLevelSelection()
    {
        // Ensure the panel is active
        levelSelectionPanel.SetActive(true);
        isPanelActive = true;

        // Update the level buttons to reflect current level unlock status
        UpdateLevelButtons();
    }

    public void ShowUpgradelSelection()
    {
        // Ensure the panel is active
        upgradePanel.SetActive(true);
        isUpgradePanelActive = true;
    }

    public void CloseLevelSelection()
    {
        levelSelectionPanel.SetActive(false);
        isPanelActive = false;
    }

    public void CloseUpgradeSelection()
    {
        upgradePanel.SetActive(false);
        isUpgradePanelActive = false;
    }

    public void UpdateLevelButtons()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            bool isUnlocked = LevelManager.Instance.IsLevelUnlocked(i + 1);
            levelButtons[i].interactable = isUnlocked;

            int levelIndex = i + 1; // Capture the index for the button
            levelButtons[i].onClick.RemoveAllListeners(); // Clear previous listeners

            if (isUnlocked)
            {
                levelButtons[i].onClick.AddListener(() => LevelManager.Instance.LoadLevel(levelIndex));
            }
        }
    }
}

