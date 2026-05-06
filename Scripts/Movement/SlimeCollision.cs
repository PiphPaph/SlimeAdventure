using UnityEngine;

public class SlimeCollision : MonoBehaviour
{
    public JellyDeformer jelly;
    public float impactMultiplier = 0.05f;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        /*Vector2 impact = rb.linearVelocity * impactMultiplier;

        Debug.Log("Impact: " + impact);

        jelly.ApplyImpact(impact);*/
        ContactPoint2D contact = collision.contacts[0];

        Vector2 worldNormal = contact.normal;
        Vector2 localNormal = transform.InverseTransformDirection(worldNormal);
        float speed = rb.linearVelocity.magnitude;

        Vector2 squash = -localNormal * speed * impactMultiplier;
        Vector2 stretch = new Vector2(localNormal.y, localNormal.x) * speed * impactMultiplier * 0.6f;

        jelly.ApplyImpact(squash + stretch);
    }
}
