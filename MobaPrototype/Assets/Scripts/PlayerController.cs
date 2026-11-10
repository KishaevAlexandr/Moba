using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Движение")]
    public float speed = 5f;
    private CharacterController controller;

    [Header("Атака")]
    public float attackRange = 2f;
    public float attackDamage = 20f;
    public float attackCooldown = 1f;
    private float lastAttackTime;

    void Start()
    {
        // Получаем компонент CharacterController при старте
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleMovement();
        HandleAttack();
    }

    void HandleMovement()
    {
        // Получение ввода с клавиатуры (WASD или стрелки)
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // Направление движения
        Vector3 move = new Vector3(h, 0, v);
        move = Vector3.ClampMagnitude(move, 1f); // Ограничение диагональной скорости

        // Применение движения
        controller.Move(move * speed * Time.deltaTime);

        // Поворот персонажа в сторону движения
        if (move != Vector3.zero)
        {
            transform.forward = move;
        }
    }

    void HandleAttack()
    {
        // Проверка нажатия ЛКМ и кулдауна
        if (Input.GetMouseButtonDown(0) && Time.time - lastAttackTime > attackCooldown)
        {
            lastAttackTime = Time.time;
            Attack();
        }
    }

    void Attack()
    {
        // Создаем невидимую сферу вокруг игрока и проверяем, что в нее попало
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);

        foreach (var hit in hitColliders)
        {
            Health health = hit.GetComponent<Health>();

            // Если у объекта есть скрипт Health и это не сам игрок — наносим урон
            if (health != null && hit.gameObject != gameObject)
            {
                health.TakeDamage(attackDamage);
            }
        }
    }

    // Опционально: чтобы видеть радиус атаки в редакторе Unity (в окне Scene)
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}