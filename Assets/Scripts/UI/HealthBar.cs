using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Slider))]
    public class HealthBar : MonoBehaviour
    {
        public IntegerVariable healthVariable;
        private Slider _slider;

        public void Start()
        {
            _slider = GetComponent<Slider>();
            healthVariable.ValueChanged += UpdateHealth;
        }
    
        public void UpdateHealth(int value)
        {
            _slider.value = value;
        }
    }
}