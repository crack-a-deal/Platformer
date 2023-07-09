using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : ProgressBar
{
    private void Awake()
    {
        PlayerHealth.OnTakeDamage += UpdateValue;
    }
    private void OnDestroy()
    {
        PlayerHealth.OnTakeDamage-=UpdateValue;
    }
    private void UpdateValue(float health)
    {
        SetCurrent(health);
        SetCurrentFill();
    }
}
