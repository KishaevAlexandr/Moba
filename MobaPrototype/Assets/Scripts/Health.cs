using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public UnityEvent onDeath;

    // Флаг, чтобы смерть срабатывала только один раз
    public bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        isDead = false;
    }

    public void TakeDamage(float amount)
    {
        // Если объект уже мертв, урон не проходит
        if (isDead) return;

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    public void Heal(float amount)
    {
        // Мертвых лечить нельзя
        if (isDead) return;
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    void Die()
    {
        // Двойная проверка на всякий случай
        if (isDead) return;

        // Ставим флаг, что мы умерли
        isDead = true;

        onDeath.Invoke();

        // 1. Если умер Игрок
        if (CompareTag("Player"))
        {
            if (GameManager.Instance != null)
                GameManager.Instance.AddDeath();

            PlayerRespawn respawn = GetComponent<PlayerRespawn>();
            if (respawn != null)
            {
                respawn.StartRespawn();
                return; // Выходим, чтобы не отключать объект игрока (gameObject.SetActive(false))
            }
        }

        // 2. Если умер Крип (миньон)
        if (CompareTag("Creep"))
        {
            if (GameManager.Instance != null)
                GameManager.Instance.AddMinionKill();
        }

        // 3. Если умерли базы
        if (CompareTag("BlueBase"))
        {
            if (GameManager.Instance != null)
                GameManager.Instance.OnBaseDestroyed(true);
        }
        if (CompareTag("RedBase"))
        {
            if (GameManager.Instance != null)
                GameManager.Instance.OnBaseDestroyed(false);
        }

        // Отключаем объект (для баз и крипов)
        gameObject.SetActive(false);
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}