using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Получение ввода с клавиатуры (WASD или стрелки)
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // Направление движения относительно камеры (здесь упрощённо — мировые координаты)
        Vector3 move = new Vector3(h, 0, v);
        move = Vector3.ClampMagnitude(move, 1f); // Ограничение диагональной скорости

        // Применение движения
        controller.Move(move * speed * Time.deltaTime);

        // Поворот персонажа в сторону движения (опционально)
        if (move != Vector3.zero)
        {
            transform.forward = move;
        }
    }
}