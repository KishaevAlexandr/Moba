using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public float damage = 25f;

    void OnCollisionEnter(Collision collision)
    {
        Health h = collision.gameObject.GetComponent<Health>();
        if (h != null) h.TakeDamage(damage);
        Destroy(gameObject); // ”ничтожение снар€да при попадании
    }
}