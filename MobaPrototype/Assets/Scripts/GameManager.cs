using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Если используете TextMeshPro

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
        endPanel.SetActive(false);
    }

    public void EndGame(bool playerWon)
    {
        endPanel.SetActive(true);
        resultText.text = playerWon ? "ПОБЕДА " : "ПОРАЖЕНИЕ";
        statsText.text = $"Убийств: {playerKills}\nСмертей: {playerDeaths}\nУбито миньонов: {minionsKilled}";
    }

    // Вызывается при смерти базы
    public void OnBaseDestroyed(bool isPlayerBase)
    {
        EndGame(!isPlayerBase); // Если разрушена база игрока, игрок проиграл
    }
}