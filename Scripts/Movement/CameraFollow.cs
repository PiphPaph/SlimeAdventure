using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Объект (слайм), за которым следует камера
    public Vector3 offset = new Vector3(0, 0, -10); // Смещение камеры

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset; // Следует за слаймом
            transform.rotation = Quaternion.Euler(0, 0, 0); // Убирает поворот камеры
        }
    }
}