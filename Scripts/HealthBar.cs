using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    public float health = 1f; // 1 = 100%, 0.5 = 50% и т.д.
    public float maxHealth = 1f; // Можно задать через инспектор

    void Update()
    {
        healthBar.fillAmount = health;
    }

    public float GetHealth()
    {
        return health;
    }

    public void SetHealth(float value)
    {
        health = Mathf.Clamp(value, 0, maxHealth);
    }
}