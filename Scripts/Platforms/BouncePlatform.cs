using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BouncePlatform : MonoBehaviour
{
    public Vector2 bounceDirection = Vector2.up;
    public float bounceForce = 10f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 force = bounceDirection.normalized * bounceForce;
                rb.velocity = Vector2.zero; // сброс перед толчком
                rb.AddForce(force, ForceMode2D.Impulse);
            }
        }
    }
}
