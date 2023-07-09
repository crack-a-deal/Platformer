using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image fillMask;
    [SerializeField] private TMP_Text healtPoint;
    [SerializeField] private float maximum;
    [SerializeField] private float current;
    public bool isFill;

    // Dispay current progress bar fill
    private void Start()
    {
        SetCurrentFill();
    }
    protected void SetCurrentFill()
    {
        healtPoint.text = string.Format("{0}/{1}",current,maximum);
        float fillAmount = current / maximum;
        isFill = fillAmount >= 1;
        fillMask.fillAmount = fillAmount;
    }
    public void Add(float exp)
    {
        current += exp;
    }
    public void SetCurrent(float value)
    {
        current = value;
    }
    public void SetMaximum(float value)
    {
        maximum = value;
    }
}
