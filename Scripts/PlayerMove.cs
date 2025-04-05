using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxForce = 10f; // Максимальная сила прыжка
    public LineRenderer lineRenderer;
    public Gradient jumpPowerGradient; // Градиент для изменения цвета

    private Vector2 _startDragPos;
    private Vector2 _endDragPos;
    public Vector2 force;
    private Rigidbody2D _rb2D;
    public bool isDragging = false;
    public bool canJump = true; // Можно ли прыгать
    

    void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canJump)
        {
            _startDragPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            _endDragPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            UpdateLine();
        }
        else if (Input.GetMouseButtonUp(0) && isDragging)
        {
            Jump();
            isDragging = false;
            lineRenderer.enabled = false;
        }
    }

    void UpdateLine()
    {
        if (lineRenderer != null)
        {
            lineRenderer.enabled = true;
            Vector2 currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Ограничение расстояния
            Vector2 direction = (currentMousePos - _startDragPos).normalized;
            float distance = Mathf.Clamp((currentMousePos - _startDragPos).magnitude, 0, maxForce);
            _endDragPos = _startDragPos + direction * distance;

            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, _endDragPos);

            // Изменение цвета градиента
            float power = distance / maxForce;
            lineRenderer.startColor = lineRenderer.endColor = jumpPowerGradient.Evaluate(power);
        }
    }

    public void Jump()
    {
        /*Vector2 */ force = (_startDragPos - _endDragPos) * maxForce;
        _rb2D.velocity = Vector2.zero; 
        _rb2D.AddForce(force, ForceMode2D.Impulse);
        canJump = false; 
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        int layer = collision.gameObject.layer;
        
        if (layer == LayerMask.NameToLayer("GroundLayer") || layer == LayerMask.NameToLayer("WaterLineLayer"))
        {
            canJump = true;
        }

        if (canJump == false)
        {
            OnCollisionStay2D(collision);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        int layer = collision.gameObject.layer;
        
        if (layer == LayerMask.NameToLayer("GroundLayer") || layer == LayerMask.NameToLayer("WaterLineLayer"))
        {
            canJump = true;
        }
    }
    
}
