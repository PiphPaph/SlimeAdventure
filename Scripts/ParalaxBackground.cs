using UnityEngine;

public class ParalaxBackground : MonoBehaviour
{
    private float _lenghtX, _lenghtY, _startPosX, _startPosY;
    public GameObject character;
    public float parallax;
    void Start() 
    {
        _startPosX = transform.position.x;
        _startPosY = transform.position.y;
        _lenghtX = GetComponent<SpriteRenderer>().bounds.size.x;
        _lenghtY = GetComponent<SpriteRenderer>().bounds.size.y;
    }
    void Update() 
    {
        float distX = (character.transform.position.x * parallax);
        float distY = (character.transform.position.y * parallax);
        transform.position = new Vector2(_startPosX + distX, _startPosY + distY);
    }
}
