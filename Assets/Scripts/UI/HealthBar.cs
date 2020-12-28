using UnityEngine;
using UnityEngine.UI;

// Thanks Brakeys! --> https://www.youtube.com/watch?v=BLfNP4Sc_iA&ab_channel=Brackeys

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    private Slider _slider;

    public void OnEnable()
    {
        _slider = GetComponent<Slider>();
    }

    public void SetMaxHealth(int health)
    {
        _slider.maxValue = health;
        _slider.value = health;
    }

    public void SetHealth(int health)
    {
        _slider.value = health;
    }
}