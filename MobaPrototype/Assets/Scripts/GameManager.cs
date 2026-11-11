using UnityEngine;
using UnityEngine.SceneManagement; 
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
        
        // На всякий случай возвращаем нормальное время при старте сцены
        Time.timeScale = 1f; 
    }

    public void AddKill() { playerKills++; }
    public void AddDeath() { playerDeaths++; }
    public void AddMinionKill() { minionsKilled++; }

    public void EndGame(bool playerWon)
    {
        if (endPanel != null) endPanel.SetActive(true);
        if (resultText != null) resultText.text = playerWon ? "ПОБЕДА " : "ПОРАЖЕНИЕ";
        if (statsText != null) statsText.text = $"Убийств: {playerKills}\nСмертей: {playerDeaths}\nУбито миньонов: {minionsKilled}";
        
        // --- ОСТАНАВЛИВАЕМ ИГРУ ---
        // Время в игре замирает. Крипы и базы перестают стрелять и двигаться.
        Time.timeScale = 0f; 
    }

    public void OnBaseDestroyed(bool isPlayerBase)
    {
        EndGame(!isPlayerBase); 
    }

    public void RestartGame()
    {
        // Возвращаем время в норму перед перезагрузкой сцены, иначе игра запустится "замороженной"
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}