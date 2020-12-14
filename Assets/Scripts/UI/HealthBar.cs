using UnityEngine;
using UnityEngine.UI;

// Thanks Brakeys! --> https://www.youtube.com/watch?v=BLfNP4Sc_iA&ab_channel=Brackeys

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    Slider slider;

    public void OnEnable()
    {
        slider = GetComponent<Slider>();
    }
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
