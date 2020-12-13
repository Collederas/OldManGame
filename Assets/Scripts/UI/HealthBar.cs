using UnityEngine;
using UnityEngine.UI;

// Thanks Brakeys! --> https://www.youtube.com/watch?v=BLfNP4Sc_iA&ab_channel=Brackeys
public class HealthBar : MonoBehaviour
{
    public Slider slider;

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
