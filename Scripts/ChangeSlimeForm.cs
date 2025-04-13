using UnityEngine;


public class ChangeSlimeForm : MonoBehaviour
{
    private BoxCollider2D _collider;
    private Rigidbody2D _rb2D;
    public PhysicsMaterial2D defaultSlimeMaterial;
    public PhysicsMaterial2D ironSlimeMaterial;
    public PhysicsMaterial2D fireSlimeMaterial;
    public PhysicsMaterial2D iceSlimeMaterial;
    
    // Добавляем публичное свойство для проверки
    public bool IsIronSlime => _collider.sharedMaterial == ironSlimeMaterial;
    public bool IsDefaultSlime => _collider.sharedMaterial == defaultSlimeMaterial;
    public bool IsFireSlime => _collider.sharedMaterial == fireSlimeMaterial;
    public bool IsIceSlime => _collider.sharedMaterial == iceSlimeMaterial;

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
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            ApplyDefaultForm();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            ApplyFireForm();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            ApplyIceForm();
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

    void ApplyFireForm()
    {
        _collider.sharedMaterial = fireSlimeMaterial;
        _rb2D.mass = 0.5f;
        _rb2D.gravityScale = 1;
    }
    
    void ApplyIceForm()
    {
        _collider.sharedMaterial = iceSlimeMaterial;
        _rb2D.mass = 0.5f;
        _rb2D.gravityScale = 1;
    }
    
    public string GetCurrentForm()
    {
        if (IsIronSlime) return "iron";
        if (IsFireSlime) return "fire";
        if (IsIceSlime) return "ice";
        return "default";
    }

    public void LoadForm(string form)
    {
        switch (form)
        {
            case "iron":
                ApplyIronForm();
                break;
            case "fire":
                ApplyFireForm();
                break;
            case "ice":
                ApplyIceForm();
                break;
            default:
                ApplyDefaultForm();
                break;
        }
    }

}
