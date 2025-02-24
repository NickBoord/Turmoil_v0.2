using UnityEngine;
using UnityEngine.UI;

public class EnemyDreamBar : MonoBehaviour
{
    public Slider dreamBar;
    public float currentValue = 0; // 0 = Neutral, -100 = Full Pure, 100 = Full Anger

    
    [SerializeField] private float minActionChance = 20f;
    [SerializeField] private float maxAttackChance = 80f;

    void Start()
    {
        if (dreamBar == null)
        {
            Debug.LogError("Dream Bar Slider is not assigned on " + gameObject.name);
            return;
        }

        UpdateDreamBarUI();
    }

    public void ApplyDreamValue(float value)
    {
        currentValue = Mathf.Clamp(currentValue + value, -100, 100);
        UpdateDreamBarUI();
    }

    private void UpdateDreamBarUI()
    {
        if (dreamBar != null)
        {
            dreamBar.value = currentValue;
        }
    }

    public bool ShouldAttack()
    {
        float attackChance = Mathf.Lerp(minActionChance, maxAttackChance, (currentValue + 100) / 200f);
        return Random.Range(0f, 100f) < attackChance;
    }

    public bool IsDefeated()
    {
        return currentValue == -100 || currentValue == 100;
    }
}
