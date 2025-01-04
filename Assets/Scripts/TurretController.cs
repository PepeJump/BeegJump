using System.Collections;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public GameObject projectilePrefab; // Projectile prefab
    public Transform shootPoint; // Point where the projectile is spawned
    public float bulletSpeed = 5f;
    public float shootCooldown = 1f; // Fire rate cooldown

    public AudioClip turretAttackSound;

    private Transform player; // The player's transform
    public bool isPlayerInVisionRange = false; // Flag to check if player is in range
    private bool isCooldownActive = false; // Cooldown flag
    private Vector3 direction;

    public Coroutine shootingCoroutine; // Store coroutine reference

    void Start()
    {
        // Find the player's transform
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void StartShooting()
    {
        if (shootingCoroutine == null)
        {
            shootingCoroutine = StartCoroutine(ShootContinuously());
        }
    }

    public void StopShooting()
    {
        if (shootingCoroutine != null)
        {
            StopCoroutine(shootingCoroutine);
            shootingCoroutine = null;
        }
    }

    void RotateTurret()
    {
        if (player != null)
        {
            direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }

    void Shoot()
    {
        AudioManager.instance.PlaySoundSFX(turretAttackSound);

        // Instantiate projectile
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

        // Get Rigidbody2D component
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        // Set velocity
        rb.velocity = direction.normalized * bulletSpeed;

        // Set projectile rotation to match movement direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(0, 0, angle);
    }


    public IEnumerator ShootContinuously()
    {
        while (isPlayerInVisionRange)
        {
            RotateTurret();
            if (!isCooldownActive)
            {
                Shoot();
                StartCoroutine(ShootCooldown());
            }
            yield return null; // Wait one frame before checking again
        }
        shootingCoroutine = null; // Reset coroutine reference when exiting loop
    }

    IEnumerator ShootCooldown()
    {
        isCooldownActive = true;
        yield return new WaitForSeconds(shootCooldown);
        isCooldownActive = false;
    }
}
