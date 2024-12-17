using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 100f;
    public float damage = 20f; // Damage the player deals on collision
    public float armor = 5f; // Armor value (mitigates damage taken)

    public float pushBackForce = 10f;

    public AudioClip bubleSound;
    private Animator animator;
    private PlayerMovement player;
    public AudioClip attackSound; // Assign your attack sound effect in the Inspector
    public ParticleSystem attackParticle; // Assign your attack particle effect in the Inspector
    private AudioSource audioSource; // Reference to the AudioSource component
    private ParticleSystem particleSystem;
    public ParticleSystem deathBubbles;
    private bool hasPlayedSound = false; // To ensure sound plays only once per burst
    public bool isDead = false;

    public GameObject healthBar;  // Reference to the health bar fill image
    public Image healthBarFill;  // Reference to the health bar fill image
    public GameObject playerVisual;

    private void Awake()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
        player = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // Add an AudioSource component if it doesn't exist
        }

        isDead = false;
    }

    private void Update()
    {
        BubbleSound();

        //if (Input.GetKeyDown(KeyCode.K)) // Press C to clear saved data
        //{
        //    PlayerPrefs.DeleteAll();
        //    PlayerPrefs.Save();
        //}
    }

    private void BubbleSound()
    {
        // Check if there are active particles
        if (particleSystem.particleCount > 0 && !hasPlayedSound)
        {
            AudioManager.instance.PlaySoundSFX(bubleSound);
            hasPlayedSound = true; // Mark sound as played for this burst
        }
        else if (particleSystem.particleCount == 0)
        {
            // Reset the flag when there are no active particles
            hasPlayedSound = false;
        }
    }

    public void TakeDamage(float amount)
    {
        // Calculate the effective damage after armor mitigation
        float effectiveDamage = amount - armor;

        // Ensure the player takes at least 1 damage if effective damage is greater than 0
        effectiveDamage = effectiveDamage > 0 ? effectiveDamage : 3f;

        currentHealth -= effectiveDamage;
        UpdateHealthBar();

        if (currentHealth <= 0f)
        {
            Die();
        }
    }
    public void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = currentHealth / maxHealth;
        }
    }

    void Die()
    {
        Debug.Log("Player Died");
        StartCoroutine(DeathTime());
    }

    private IEnumerator DeathTime()
    {
        isDead = true;

        float elapsed = 0f;
        float duration = 2f; // Duration of the death animation

        Vector3 initialScale = playerVisual.transform.localScale; // Original scale
        Vector3 targetScale = Vector3.zero; // Scale to shrink to

        if (animator != null)
        {
            animator.enabled = false;
        }

        deathBubbles.Play();
        AudioManager.instance.PlaySoundSFX(bubleSound);

        yield return new WaitForSeconds(0.25f);

        healthBar.SetActive(false);

        while (elapsed < duration)
        {
            // Lerp the scale over time
            playerVisual.transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsed / duration);

            // Rotate the visual as it scales
            playerVisual.transform.Rotate(0, 0, 300 * Time.deltaTime);

            elapsed += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure it is fully scaled to zero at the end
        playerVisual.transform.localScale = targetScale;


        // Reload the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);

                // Play the attack sound effect
                audioSource.PlayOneShot(attackSound);

                // Play the attack particle effect
                //attackParticle.transform.position = collision.contacts[0].point;
                //attackParticle.Play();

                // Start the attack state
                GetComponent<PlayerMovement>().StartAttack();

                // Apply a force to the player towards the opposite side of the enemy

                //Rigidbody2D playerRigidbody = GetComponent<Rigidbody2D>();
                //Vector2 forceDirection = (transform.position + enemy.transform.position).normalized;
                //playerRigidbody.AddForce(-forceDirection * pushBackForce, ForceMode2D.Impulse);

                // Apply a force to the player away from the enemy
                Rigidbody2D playerRigidbody = GetComponent<Rigidbody2D>();
                Vector2 forceDirection = (enemy.transform.position - transform.position).normalized;
                playerRigidbody.AddForce(-forceDirection * pushBackForce, ForceMode2D.Impulse);

            }
        }
    }
}