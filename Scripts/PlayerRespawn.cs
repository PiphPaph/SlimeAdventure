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
        
        /*string[] allSlots = SaveSystem.GetAllSaveSlots();
        foreach (string slot in allSlots)
        {
            Debug.Log("Слот: " + slot);
        }*/
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
    
    public void SaveGameToSlot(string slotName)
    {
        SaveData data = new SaveData();
        data.playerHealth = _healthBar.GetHealth();
        data.slimeForm = _slimeForm.GetCurrentForm();
        data.checkpointX = _currentCheckpoint.x;
        data.checkpointY = _currentCheckpoint.y;
        data.saveName = slotName;
        data.saveTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm");

        SaveSystem.SaveToSlot(data, slotName);
    }

    public void LoadGameFromSlot(string slotName)
    {
        SaveData data = SaveSystem.LoadFromSlot(slotName);
        if (data == null) return;

        _currentCheckpoint = new Vector2(data.checkpointX, data.checkpointY);
        transform.position = _currentCheckpoint;
        _healthBar.SetHealth(data.playerHealth);
        _slimeForm.LoadForm(data.slimeForm);
    }

}