using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Example video is https://www.youtube.com/watch?v=BLfNP4Sc_iA
public class HealthBar : MonoBehaviour


{
    // Start is called before the first frame update
    public Gradient gradient;
    public Image fill;
    public Slider slider;
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        //fill.color = gradient.Evaluate();  
    }

    public void SetHealth(int health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);

    }

        

}
