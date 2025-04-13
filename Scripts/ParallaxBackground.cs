using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private float lenghtX, lenghtY, startposX, startposY;
    public GameObject character;
    public float parallax;
    void Start() 
    {
        startposX = transform.position.x;
        startposY = transform.position.y;
        lenghtX = GetComponent<SpriteRenderer>().bounds.size.x;
        lenghtY = GetComponent<SpriteRenderer>().bounds.size.y;
    }
    void Update() 
    {
        float distX = (character.transform.position.x * parallax);
        float distY = (character.transform.position.y * parallax);
        transform.position = new Vector2(startposX + distX, startposY + distY);
    }
}