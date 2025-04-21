using UnityEngine;

public class AddJumpForce : MonoBehaviour
{
  private BoxCollider2D _playerCollider;
  private BoxCollider2D _rabbitCollider;
  private GameObject _rabbit;
  private PlayerMove _playerMove;

  void Start()
  {
    GameObject player = GameObject.Find("Player");

    if (player != null)
    {
      _playerCollider = player.GetComponent<BoxCollider2D>();
      _playerMove = player.GetComponent<PlayerMove>();
    }

    _rabbitCollider = GetComponent<BoxCollider2D>();
    _rabbit = gameObject;
  }
  
  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.collider == _playerCollider && _playerMove != null)
    {
      _playerMove.maxForce += 1;
      Destroy(_rabbit);
    }
  }
  
}
