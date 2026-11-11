using UnityEngine;
using TMPro; // Подключаем библиотеку для работы с текстом

public class HealthTextUI : MonoBehaviour
{
    public Health targetHealth; // Объект, чье здоровье мы отслеживаем
    public TextMeshProUGUI healthText; // Сам текст на экране

    void Update()
    {
        if (targetHealth != null && healthText != null)
        {
            // Берем текущее здоровье и переводим в строку (текст)
            float hp = targetHealth.GetCurrentHealth();
            healthText.text = hp.ToString("0"); // "0" означает округление до целого числа
        }
    }
}