using UnityEngine;

public class CreepAI : MonoBehaviour
{
    public float speed = 2f;
    public float attackRange = 1.5f;
    public float attackDamage = 10f;
    public float attackCooldown = 1.5f;

    private Transform target;
    private float lastAttackTime;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        // Ищем базу игрока (синюю) по тегу
        GameObject blueBase = GameObject.FindGameObjectWithTag("BlueBase");
        if (blueBase != null) target = blueBase.transform;
    }

    void Update()
    {
        // Если цель (база) уничтожена, крип останавливается
        if (target == null || !target.gameObject.activeSelf) return;

        // Ищем игрока, если он рядом (приоритетная цель)
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && Vector3.Distance(transform.position, player.transform.position) < 5f)
        {
            target = player.transform;
        }
        else
        {
            // Если игрока нет рядом, идем к базе
            GameObject blueBase = GameObject.FindGameObjectWithTag("BlueBase");
            if (blueBase != null) target = blueBase.transform;
        }

        if (target == null) return;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= attackRange)
        {
            // Атакуем
            if (Time.time - lastAttackTime > attackCooldown)
            {
                lastAttackTime = Time.time;
                Health health = target.GetComponent<Health>();
                if (health != null) health.TakeDamage(attackDamage);
            }
        }
        else
        {
            // Идем к цели
            Vector3 direction = (target.position - transform.position).normalized;
            direction.y = 0; // Чтобы не улетал вверх
            transform.forward = direction;

            if (controller != null)
                controller.Move(direction * speed * Time.deltaTime);
            else
                transform.position += direction * speed * Time.deltaTime;
        }
    }
}