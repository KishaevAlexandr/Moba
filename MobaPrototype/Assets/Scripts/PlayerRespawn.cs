using System.Collections;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public float respawnTime = 3f;

    [Header("Точка возрождения")]
    public Transform respawnPoint; // Ссылка на объект SpawnPoint

    private PlayerController controllerScript;
    private CharacterController characterController;
    private Health healthScript;

    void Start()
    {
        controllerScript = GetComponent<PlayerController>();
        characterController = GetComponent<CharacterController>();
        healthScript = GetComponent<Health>();
    }

    public void StartRespawn()
    {
        StartCoroutine(RespawnRoutine());
    }

    IEnumerator RespawnRoutine()
    {
        // 1. Отключаем управление и физику
        if (controllerScript != null) controllerScript.enabled = false;
        if (characterController != null) characterController.enabled = false;

        // 2. Ждем указанное время
        yield return new WaitForSeconds(respawnTime);

        // 3. Телепортируем игрока в точку возрождения
        if (respawnPoint != null)
        {
            // Важно: перед сменой позиции отключаем CharacterController, 
            // чтобы Unity не сопротивлялась телепортации
            if (characterController != null) characterController.enabled = false;

            transform.position = respawnPoint.position;

            if (characterController != null) characterController.enabled = true;
        }

        // 4. Восстанавливаем здоровье и сбрасываем флаг смерти
        if (healthScript != null)
        {
            healthScript.currentHealth = healthScript.maxHealth;
            healthScript.isDead = false;
        }

        // 5. Включаем управление обратно
        if (controllerScript != null) controllerScript.enabled = true;
    }
}