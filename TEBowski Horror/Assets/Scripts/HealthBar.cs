using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
                  
    public Slider slider;
    public Gradient gradient;
    public Image Health;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;   //Ustawia maksymalną wartość Health Bara
        slider.value = health;

        Health.color = gradient.Evaluate(1f);   //Ustawia gradient dla paska HP
    }

    public void SetHealth(int health)
    {
        slider.value = health;

        Health.color = gradient.Evaluate(slider.normalizedValue);   //Aktualizuje kolor hp z utratą i jego gradientem
    }
}
