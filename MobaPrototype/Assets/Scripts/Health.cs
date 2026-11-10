using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public UnityEvent onDeath; // Событие смерти, для последующей статистики

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    void Die()
    {
        onDeath.Invoke();
        // Временно: просто отключаем объект (для игрока позже заменим на респавн)
        gameObject.SetActive(false);
    }

    public float GetCurrentHealth() => currentHealth;
}