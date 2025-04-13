using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public static PlayerRespawn Instance;

    private Vector2 _currentCheckpoint;
    private float _savedHealth;
    private string _savedForm;

    private HealthBar _healthBar;
    private ChangeSlimeForm _slimeForm;
    
    private void Awake()
    {
        Instance = this;
        _healthBar = GetComponent<HealthBar>();
        _slimeForm = GetComponent<ChangeSlimeForm>();
    }
    
    private void Start()
    {
        _currentCheckpoint = transform.position;

        if (_healthBar != null) _savedHealth = _healthBar.GetHealth();
        if (_slimeForm != null) _savedForm = _slimeForm.GetCurrentForm();
    }

    public void SetCheckpoint(Vector2 newCheckpoint)
    {
        _currentCheckpoint = newCheckpoint;
        _savedHealth = _healthBar.GetHealth();
        _savedForm = _slimeForm.GetCurrentForm();
        Debug.Log("Checkpoint saved: " + _currentCheckpoint);
    }

    public void Respawn()
    {
        transform.position = _currentCheckpoint;
        _healthBar.SetHealth(_savedHealth);
        _slimeForm.LoadForm(_savedForm);
    }
}