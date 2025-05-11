using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Collider2D))]
public class SpikeTrap : MonoBehaviour
{
    public Sprite raisedSprite;
    public Sprite loweredSprite;
    public float raiseInterval = 2f;
    public float damage = 0.2f;
    public float knockbackForce = 5f;

    private SpriteRenderer spriteRenderer;
    private Collider2D trapCollider;
    private bool isDangerous;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        trapCollider = GetComponent<Collider2D>();
        StartCoroutine(SpikeCycle());
    }

    private System.Collections.IEnumerator SpikeCycle()
    {
        while (true)
        {
            isDangerous = true;
            spriteRenderer.sprite = raisedSprite;

            // Прямо сейчас проверяем, кто стоит на ловушке
            ApplyDamageToOverlappingObjects();

            yield return new WaitForSeconds(raiseInterval);

            isDangerous = false;
            spriteRenderer.sprite = loweredSprite;
            yield return new WaitForSeconds(raiseInterval);
        }
    }

    private void ApplyDamageToOverlappingObjects()
    {
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        filter.useTriggers = false;

        List<Collider2D> results = new List<Collider2D>();
        trapCollider.OverlapCollider(filter, results);

        foreach (Collider2D col in results)
        {
            if (!col.CompareTag("Player")) continue;

            GameObject player = col.gameObject;

            var health = player.GetComponent<HealthBar>();
            if (health != null)
            {
                health.health -= damage;
            }

            var rb = player.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 knockbackDir = (player.transform.position - transform.position).normalized;
                rb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Дополнительно: если игрок запрыгивает на уже активные шипы
        if (isDangerous && collision.collider.CompareTag("Player"))
        {
            ApplyDamageToSingle(collision.collider.gameObject);
        }
    }

    private void ApplyDamageToSingle(GameObject player)
    {
        var health = player.GetComponent<HealthBar>();
        if (health != null)
        {
            health.health -= damage;
        }

        var rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 knockbackDir = (player.transform.position - transform.position).normalized;
            rb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);
        }
    }
}
