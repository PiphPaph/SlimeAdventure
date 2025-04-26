using System;
using UnityEngine;

public class ChangeSlimeForm : MonoBehaviour
{
    // Физические материалы
    public PhysicsMaterial2D defaultSlimeMaterial;
    public PhysicsMaterial2D ironSlimeMaterial;
    public PhysicsMaterial2D fireSlimeMaterial;
    public PhysicsMaterial2D iceSlimeMaterial;

    // Настройки для форм
    [System.Serializable]
    public class SlimeForm
    {
        public string name;
        public PhysicsMaterial2D material;
        public float mass;
        public int requiredItems = 3;
        [HideInInspector] public int collectedItems;
    }

    public SlimeForm[] forms;
    private BoxCollider2D _collider;
    private Rigidbody2D _rb2D;
    public SlimeUIController uiController;

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _rb2D = GetComponent<Rigidbody2D>();
        InitializeForms();
    }

    void InitializeForms()
    {
        forms = new SlimeForm[]
        {
            new SlimeForm { name = "default", material = defaultSlimeMaterial, mass = 1f },
            new SlimeForm { name = "iron", material = ironSlimeMaterial, mass = 6f },
            new SlimeForm { name = "fire", material = fireSlimeMaterial, mass = 0.5f },
            new SlimeForm { name = "ice", material = iceSlimeMaterial, mass = 0.5f }
        };
    }

    public void AddItem(string formType)
    {
        foreach (var form in forms)
        {
            if (form.name == formType)
            {
                form.collectedItems++;
                uiController.UpdateProgress(formType, form.collectedItems, form.requiredItems);
                break;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            // Принудительно включаем дефолтную форму
            ApplyForm(forms[0]); // Прямой вызов, минуя проверки
            return;
        }
    
        // Остальные формы с проверкой
        if (Input.GetKeyDown(KeyCode.A)) TryChangeForm("iron");
        if (Input.GetKeyDown(KeyCode.S)) TryChangeForm("fire");
        if (Input.GetKeyDown(KeyCode.D)) TryChangeForm("ice");
    }

    void TryChangeForm(string formName)
    {
        // Дефолтная форма - особый случай
        if (formName == "default")
        {
            ApplyForm(forms[0]);
            return;
        }

        // Для остальных - проверка коллекции
        SlimeForm targetForm = Array.Find(forms, f => f.name == formName);
        if (targetForm != null && targetForm.collectedItems >= targetForm.requiredItems)
        {
            ApplyForm(targetForm);
        }
        else
        {
            Debug.Log($"Форма {formName} заблокирована!");
        }
    }

    void ApplyForm(SlimeForm form)
    {
        _collider.sharedMaterial = form.material;
        _rb2D.mass = form.mass;
        Debug.Log($"Активирована форма: {form.name}");
    }

    // Для сохранения/загрузки
    public string GetCurrentForm()
    {
        foreach (var form in forms)
            if (_collider.sharedMaterial == form.material)
                return form.name;
        return "default";
    }
    
    public void LoadForm(string formName)
    {
        foreach (var form in forms)
        {
            if (form.name == formName)
            {
                ApplyForm(form);
                return;
            }
        }
        Debug.LogError($"Форма {formName} не найдена!");
    }

// Геттеры для совместимости (оставляем публичными)
    public bool IsIronSlime => GetCurrentForm() == "iron";
    public bool IsFireSlime => GetCurrentForm() == "fire";
    public bool IsIceSlime => GetCurrentForm() == "ice";
    public bool IsDefaultSlime => GetCurrentForm() == "default";
}

/*
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
*/
