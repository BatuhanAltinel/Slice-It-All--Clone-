using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinishPlatform : MonoBehaviour
{
    [SerializeField] private int _multiplier;
    [SerializeField] TextMeshProUGUI _multiplierText;
    
    void Start()
    {
        if(_multiplierText != null)
        _multiplierText.text = $"X{_multiplier}";
    }
    public int GetMultiplier()
    {
        return _multiplier;
    }
}
