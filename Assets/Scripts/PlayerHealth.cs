using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthBar; // Reference to the health bar slider
    private int currentHealth = 10; // Default health

    void Start()
    {
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, 10); // Ensure health doesn't go below 0
        UpdateHealthUI();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, 10); // Ensure health doesn't exceed max
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.value = currentHealth; // Assuming the health bar is set to range from 0 to 1
        }
    }
}
