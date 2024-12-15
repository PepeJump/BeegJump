using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    private float levelEndDelay = 0.05f; // Time to wait before showing the level selection

    [SerializeField]private ParticleSystem particleFX;

    private void Awake()
    {
        particleFX = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {           
            if(particleFX != null)
                particleFX.Pause();

            // Trigger the level end process
            StartCoroutine(LevelComplete());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (particleFX != null)
                particleFX.Play();

            if (UIManager.Instance.levelSelectionPanel.activeInHierarchy)
            {
                UIManager.Instance.CloseLevelSelection();
            }
        }
        
    }

    private IEnumerator LevelComplete()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(levelEndDelay);

        // Mark the current level as completed and unlock the next level
        LevelManager.Instance.CompleteLevel();

        // Only show the level selection UI if it's not already open
        if (!UIManager.Instance.levelSelectionPanel.activeInHierarchy)
        {
            UIManager.Instance.ShowLevelSelection();
        }
    }
}
