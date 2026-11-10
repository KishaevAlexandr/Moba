using UnityEngine;

public class AbilitySystem : MonoBehaviour
{
    [Header("Настройки Кулдаунов")]
    public float qCooldown = 3f;  // Способность 1 (Снаряд)
    public float spaceCooldown = 5f; // Способность 2 (Рывок)
    public float eCooldown = 8f;  // Способность 3 (Лечение)

    private float qTimer, spaceTimer, eTimer;

    [Header("Ссылки и Параметры")]
    public GameObject projectilePrefab; // Сюда перетащить префаб снаряда
    public float dashDistance = 5f;     // Дистанция рывка
    public float healAmount = 30f;      // Количество лечения

    void Update()
    {
        // Уменьшаем таймеры кулдауна со временем
        qTimer -= Time.deltaTime;
        spaceTimer -= Time.deltaTime;
        eTimer -= Time.deltaTime;

        // 1. Способность на КЛАВИШУ Q (Запуск снаряда)
        if (Input.GetKeyDown(KeyCode.Q) && qTimer <= 0)
        {
            qTimer = qCooldown;
            CastProjectile();
        }

        // 2. Способность на ПРОБЕЛ / SPACE (Рывок)
        if (Input.GetKeyDown(KeyCode.Space) && spaceTimer <= 0)
        {
            spaceTimer = spaceCooldown;
            Dash();
        }

        // 3. Способность на КЛАВИШУ E (Лечение)
        if (Input.GetKeyDown(KeyCode.E) && eTimer <= 0)
        {
            eTimer = eCooldown;
            HealSelf();
        }
    }

    void CastProjectile()
    {
        if (projectilePrefab != null)
        {
            // Создаем снаряд чуть впереди игрока
            GameObject proj = Instantiate(projectilePrefab, transform.position + transform.forward * 1.5f, Quaternion.identity);

            // Задаем ему скорость вперед
            Rigidbody rb = proj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // В Unity 6 используется linearVelocity
                rb.linearVelocity = transform.forward * 15f;
            }
        }
    }

    void Dash()
    {
        // Получаем контроллер игрока и делаем рывок вперед
        CharacterController controller = GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.Move(transform.forward * dashDistance);
        }
    }

    void HealSelf()
    {
        // Ищем компонент здоровья и лечим
        Health h = GetComponent<Health>();
        if (h != null)
        {
            h.Heal(healAmount);
        }
    }
}