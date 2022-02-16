using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar:MonoBehaviour
{
    public Slider Slider;
    public Gradient Gradient;
    public Image Fill;
    private System.Action _minHealthAction;
    private int _minHealth;
    private bool _isMinHealth;
    public void SetMaxHealth(int max,int half, System.Action action)
    {
        Slider.minValue = 0;
        Slider.maxValue = max;
        Fill.color=Gradient.Evaluate(1f);
        _minHealthAction = action;
        _minHealth = max / half;
    }

    public void SetHealthValue(int health)
    {
        Slider.value = health;
        Fill.color = Gradient.Evaluate(Slider.normalizedValue);
        if (health <= _minHealth && _isMinHealth == false)
        {
            _isMinHealth = true;
            _minHealthAction?.Invoke();
        }

    }
    
}
