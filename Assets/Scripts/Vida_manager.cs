using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Vida_manager : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("UI References")]
    public RawImage healthBarGreen;
    public RawImage healthBarRed;

    [Header("Animator and Death Settings")]
    public Animator animator;
    public AudioClip deathSound;
    private AudioSource audioSource;

    [Header("Delay Settings")]
    public float delayTime = 2f;
    public float smoothSpeed = 0.5f;

    private RectTransform greenBarRect;
    private RectTransform redBarRect;
    private float initialWidth;

    [Header("Player Identification")]
    public string playerIdentifier; // Pode ser "Player1" ou "Player2" para distinguir os jogadores

    void Start()
    {
        currentHealth = maxHealth;
        greenBarRect = healthBarGreen.GetComponent<RectTransform>();
        redBarRect = healthBarRed.GetComponent<RectTransform>();
        initialWidth = greenBarRect.sizeDelta.x;

        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(float amount)
    {
        if (currentHealth <= 0) return;  // Já está morto, ignora mais dano

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        float newWidth = (currentHealth / maxHealth) * initialWidth;
        greenBarRect.sizeDelta = new Vector2(newWidth, greenBarRect.sizeDelta.y);

        StartCoroutine(UpdateRedBar(newWidth));

        if (currentHealth <= 0)
        {
            TriggerDeath();
        }
    }

    private IEnumerator UpdateRedBar(float targetWidth)
    {
        yield return new WaitForSeconds(delayTime);

        while (redBarRect.sizeDelta.x > targetWidth)
        {
            float newWidth = Mathf.Lerp(redBarRect.sizeDelta.x, targetWidth, smoothSpeed * Time.deltaTime);
            redBarRect.sizeDelta = new Vector2(newWidth, redBarRect.sizeDelta.y);
            yield return null;
        }
    }

    private void TriggerDeath()
    {
        if (animator != null)
        {
            animator.SetTrigger("Morte");
        }

        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        Destroy(gameObject, 2f);
    }
}
