using UnityEngine;
using UnityEngine.UI; // For Slider support

public class PlayerHealth : MonoBehaviour
{
    public Slider healthBar; // Reference to the health bar slider
    private int currentHealth = 10; // Default health

    void Start()
    {
        UpdateHealthUI();
    }

    // Method for taking damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, 10); // Ensure health doesn't go below 0
        UpdateHealthUI();
    }

    // Method for healing
    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, 10); // Ensure health doesn't exceed max
        UpdateHealthUI();
    }

    // Update the health bar UI
    private void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.value = currentHealth / 10f; // Assuming the health bar is set to range from 0 to 1
        }
    }

    // Expose health as a public property if you need it elsewhere
    public int CurrentHealth => currentHealth;
}