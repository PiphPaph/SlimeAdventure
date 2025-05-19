using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector2 moveDirection = Vector2.right;
    public float moveDistance = 3f;
    public float moveSpeed = 6f;

    private Vector3 _startPos;
    private bool _forward = true;

    void Start()
    {
        _startPos = transform.position;
        moveDirection.Normalize();
    }

    void Update()
    {
        float moveStep = moveSpeed * Time.deltaTime;
        Vector3 target = _startPos + (Vector3)(moveDirection * moveDistance);
        Vector3 back = _startPos;

        if (_forward)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveStep);
            if (Vector3.Distance(transform.position, target) < 0.01f)
                _forward = false;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, back, moveStep);
            if (Vector3.Distance(transform.position, back) < 0.01f)
                _forward = true;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}