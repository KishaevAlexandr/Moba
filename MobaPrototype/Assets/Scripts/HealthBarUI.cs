using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Health targetHealth;
    public Slider slider;

    void Update()
    {
        if (targetHealth != null && slider != null)
            slider.value = targetHealth.GetCurrentHealth();
    }
}