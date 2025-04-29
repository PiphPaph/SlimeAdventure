using UnityEngine;

public class Door : MonoBehaviour
{
    private Vector3 _closedPosition;
    public Vector3 openOffset;
    private bool _isOpen;

    private void Start()
    {
        _closedPosition = transform.position;
    }

    public void Open()
    {
        if (_isOpen) return;
        transform.position = _closedPosition + openOffset;
        _isOpen = true;
    }

    public void Close()
    {
        if (!_isOpen) return;
        transform.position = _closedPosition;
        _isOpen = false;
    }
}