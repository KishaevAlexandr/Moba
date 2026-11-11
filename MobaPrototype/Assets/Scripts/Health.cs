using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth; // Сделали public, чтобы видеть в инспекторе, как падает здоровье

    public UnityEvent onDeath;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log(gameObject.name + " получил урон: " + amount + ". Осталось здоровья: " + currentHealth);

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
        Debug.Log(gameObject.name + " УНИЧТОЖЕН!");
        onDeath.Invoke();

        // Проверка на победу/поражение
        if (CompareTag("BlueBase"))
        {
            if (GameManager.Instance != null) GameManager.Instance.OnBaseDestroyed(true);
        }
        if (CompareTag("RedBase"))
        {
            if (GameManager.Instance != null) GameManager.Instance.OnBaseDestroyed(false);
        }

        gameObject.SetActive(false); // Скрываем объект
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}