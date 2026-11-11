using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Движение")]
    public float speed = 5f;
    public float rotationSpeed = 10f; // Скорость поворота персонажа
    private CharacterController controller;

    [Header("Атака")]
    public float attackRange = 2f;
    public float attackDamage = 20f;
    public float attackCooldown = 1f;
    private float lastAttackTime;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleMovement();
        HandleAttack();
    }

    void HandleMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;

        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 move = (camForward * v + camRight * h).normalized;

        controller.Move(move * speed * Time.deltaTime);

        // ПЛАВНЫЙ ПОВОРОТ (вместо мгновенного transform.forward = move)
        if (move != Vector3.zero)
        {
            // Вычисляем нужный угол поворота
            Quaternion targetRotation = Quaternion.LookRotation(move);

            // Плавно поворачиваем от текущего положения к нужному
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0) && Time.time - lastAttackTime > attackCooldown)
        {
            lastAttackTime = Time.time;
            Attack();
        }
    }

    void Attack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);
        Debug.Log("Атака! Найдено объектов в радиусе: " + hitColliders.Length);

        foreach (var hit in hitColliders)
        {
            Health health = hit.GetComponent<Health>();
            if (health != null && hit.gameObject != gameObject)
            {
                health.TakeDamage(attackDamage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}