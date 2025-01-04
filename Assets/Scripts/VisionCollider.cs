using System.Collections;
using UnityEngine;

public class VisionCollider : MonoBehaviour
{
    private TurretController turretController; // Reference to the corresponding TurretController

    void Start()
    {
        // Get the TurretController attached to the parent object
        turretController = GetComponentInChildren<TurretController>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            turretController.isPlayerInVisionRange = true;
            turretController.StartShooting(); // Start shooting properly
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            turretController.isPlayerInVisionRange = false;
            turretController.StopShooting(); // Stop shooting properly
        }
    }
}
