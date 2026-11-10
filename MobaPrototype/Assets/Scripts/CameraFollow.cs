using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 5, -7); // Смещение: вверх и назад
    public float smoothSpeed = 5f; // Плавность движения камеры

    void LateUpdate()
    {
        if (target != null)
        {
            // Вычисляем точку, где должна быть камера (учитывая поворот игрока)
            Vector3 desiredPosition = target.position + target.rotation * offset;

            // Плавно перемещаем камеру к этой точке
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

            // Камера всегда смотрит на игрока (немного выше его центра)
            transform.LookAt(target.position + Vector3.up * 1.5f);
        }
    }
}