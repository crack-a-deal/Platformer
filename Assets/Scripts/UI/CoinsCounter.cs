using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text coinLabel;
    public float coinAmount = 0;
    private void OnEnable()
    {
        Coin.OnCoinCollected += UpdateValue;
    }
    private void OnDisable()
    {
        Coin.OnCoinCollected -= UpdateValue;
    }
    private void UpdateValue()
    {
        coinAmount++;
        coinLabel.text=coinAmount.ToString();
    }
}
