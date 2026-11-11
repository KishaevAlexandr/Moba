using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Health targetHealth;
    public Slider slider;

    void Start()
    {
        if (targetHealth != null && slider != null)
        {
            slider.maxValue = targetHealth.maxHealth; // Принудительно ставим максимум
            slider.value = targetHealth.maxHealth;
        }
    }

    void Update()
    {
        if (targetHealth != null && slider != null)
        {
            // Обновляем полоску текущим здоровьем
            slider.value = targetHealth.GetCurrentHealth();
        }
    }
}