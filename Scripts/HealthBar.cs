using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    public float health = 1f; // 1 = 100%, 0.5 = 50% и т.д.

    void Update()
    {
        healthBar.fillAmount = health;
    }
}
