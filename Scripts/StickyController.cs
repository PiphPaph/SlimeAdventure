using UnityEngine;

public class StickyController : MonoBehaviour
{
    [Header("Настройки")]
    [Range(0.1f, 1f)] public float stickyFriction = 0.95f;
    public float positionCorrection = 25f;
    public float surfaceOffset = 0.03f;
    public float breakVelocity = 3f;

    private Rigidbody2D _rb;
    private Collider2D _col;
    private Vector2 _stickyNormal;
    private Vector2 _contactPoint;
    private bool _isSticky;
    private PhysicsMaterial2D _stickyMaterial;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<Collider2D>();
        
        // Создаем материал один раз
        _stickyMaterial = new PhysicsMaterial2D
        {
            friction = stickyFriction,
            bounciness = 0
        };
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("StickySurface") || _isSticky) 
            return;

        // Фиксируем параметры контакта
        ContactPoint2D contact = col.GetContact(0);
        _contactPoint = contact.point;
        _stickyNormal = contact.normal;
        
        // Применяем липкий материал
        _col.sharedMaterial = _stickyMaterial;
        _isSticky = true;
        
        // Небольшая коррекция позиции
        Vector2 targetPos = _contactPoint + _stickyNormal * (GetColliderRadius() + surfaceOffset);
        _rb.MovePosition(targetPos);
    }

    private void FixedUpdate()
    {
        if (!_isSticky) return;

        // 1. Постоянная коррекция позиции
        Vector2 targetPos = _contactPoint + _stickyNormal * (GetColliderRadius() + surfaceOffset);
        Vector2 correctionForce = (targetPos - _rb.position) * positionCorrection;
        _rb.AddForce(correctionForce);

        // 2. Проверка на отрыв
        if (_rb.velocity.magnitude > breakVelocity)
        {
            Unstick();
        }
    }

    private void Update()
    {
        if (_isSticky && Input.GetMouseButtonDown(0))
        {
            // Отталкивание с учетом поверхности
            Vector2 jumpForce = (_stickyNormal + Vector2.up).normalized * breakVelocity;
            _rb.AddForce(jumpForce, ForceMode2D.Impulse);
            Unstick();
        }
    }

    private void Unstick()
    {
        _isSticky = false;
        _col.sharedMaterial = null; // Возвращаем стандартный материал
    }

    private float GetColliderRadius()
    {
        if (_col is CircleCollider2D circle) 
            return circle.radius * transform.localScale.y;
        
        return _col.bounds.extents.y;
    }
}