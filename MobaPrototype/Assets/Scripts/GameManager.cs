using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int playerKills = 0;
    public int playerDeaths = 0;
    public int minionsKilled = 0;

    public GameObject endPanel;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI statsText;

    void Awake()
    {
        Instance = this;
        if (endPanel != null) endPanel.SetActive(false);
    }

    // Методы для добавления статистики
    public void AddKill() { playerKills++; }
    public void AddDeath() { playerDeaths++; }
    public void AddMinionKill() { minionsKilled++; }

    public void EndGame(bool playerWon)
    {
        endPanel.SetActive(true);
        resultText.text = playerWon ? "ПОБЕДА " : "ПОРАЖЕНИЕ";
        statsText.text = $"Убийств: {playerKills}\nСмертей: {playerDeaths}\nУбито миньонов: {minionsKilled}";

        // Останавливаем время в игре (опционально)
        // Time.timeScale = 0f; 
    }

    public void OnBaseDestroyed(bool isPlayerBase)
    {
        EndGame(!isPlayerBase);
    }
}