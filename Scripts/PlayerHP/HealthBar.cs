using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    public float health = 1f;

    private PlayerRespawn respawn;
    private bool isDead;

    private void Start()
    {
        respawn = GetComponent<PlayerRespawn>();
    }

    void Update()
    {
        healthBar.fillAmount = health;

        if (health <= 0f && !isDead)
        {
            isDead = true;
            if (respawn != null)
                respawn.Respawn();
        }
    }

    public float GetHealth()
    {
        return health;
    }

    public void SetHealth(float value)
    {
        health = Mathf.Clamp01(value);
        isDead = false; // сброс флага после восстановления
    }
}