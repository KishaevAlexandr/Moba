using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float timeElapsed = 0f;
    private bool isRunning = true;

    void Update()
    {
        if (!isRunning) return;

        // Увеличиваем время
        timeElapsed += Time.deltaTime;

        // Переводим в минуты и секунды
        int minutes = Mathf.FloorToInt(timeElapsed / 60);
        int seconds = Mathf.FloorToInt(timeElapsed % 60);

        // Выводим на экран в формате 00:00
        if (timerText != null)
        {
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    // Метод, чтобы остановить таймер в конце игры
    public void StopTimer()
    {
        isRunning = false;
    }
}