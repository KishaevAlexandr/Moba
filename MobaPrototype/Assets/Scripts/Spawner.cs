using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject creepPrefab; // Сюда перетащим префаб крипа
    public float spawnInterval = 5f; // Каждые 5 секунд
    private float timer;

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = spawnInterval;
            SpawnCreep();
        }
    }

    void SpawnCreep()
    {
        if (creepPrefab != null)
        {
            // Создаем крипа в позиции спавнера
            Instantiate(creepPrefab, transform.position, Quaternion.identity);
        }
    }
}