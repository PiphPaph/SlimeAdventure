using UnityEngine;

public class SawTrap : MonoBehaviour
{
    public float damage = 0.2f;
    public float knockbackForce = 5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            var health = collision.collider.GetComponent<HealthBar>();
            if (health != null)
            {
                health.health -= damage;
            }

            Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
                rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            }
        }
    }
}
