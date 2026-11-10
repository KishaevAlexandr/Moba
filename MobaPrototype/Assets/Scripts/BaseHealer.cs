using UnityEngine;

public class BaseHealer : MonoBehaviour
{
    public float healPerSecond = 10f;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Health h = other.GetComponent<Health>();
            if (h != null) h.Heal(healPerSecond * Time.deltaTime);
        }
    }
}