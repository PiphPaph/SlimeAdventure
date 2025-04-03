using UnityEngine;


public class ChangeSlimeForm : MonoBehaviour
{
    private BoxCollider2D _collider;
    private Rigidbody2D _rb2D;
    public PhysicsMaterial2D defaultSlimeMaterial;
    public PhysicsMaterial2D ironSlimeMaterial;
    
    // Добавляем публичное свойство для проверки
    public bool IsIronSlime => _collider.sharedMaterial == ironSlimeMaterial;
    public bool IsDefaultSlime => _collider.sharedMaterial == defaultSlimeMaterial;

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ApplyIronForm();
        }
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            ApplyDefaultForm();
        }
    }

    void ApplyIronForm()
    {
        _collider.sharedMaterial = ironSlimeMaterial;
        _rb2D.mass = 6;
        _rb2D.gravityScale = 1;
    }

    void ApplyDefaultForm()
    {
        _collider.sharedMaterial = defaultSlimeMaterial;
        _rb2D.mass = 1;
        _rb2D.gravityScale = 1;
    }
}
