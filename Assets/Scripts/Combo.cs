using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combo : MonoBehaviour
{
    public bool IsSlice { get; set; }
    [SerializeField] float _comboRate = 0.5f;
    [SerializeField] float _maxComboPoint = 10f;
    [SerializeField] float _pointPerCombo = 2.5f;
    // public float ComboDuration { get; set; }
    public float ComboDuration;
    public float ComboPoints { get; set; }
    public bool _isOnFire;

    public Slider _coomboSlider;

    void OnEnable()
    {
        EventManager.OnSlice += ComboCheck;
    }

    void OnDisable()
    {
        EventManager.OnSlice -= ComboCheck;
    }

    private void Awake()
    {
        ComboPoints = 0;
        // ComboDuration = 4f;
    }
    

    public void ComboCheck()
    {
        StartCoroutine(ComboCheckRoutine());
    }

    IEnumerator ComboCheckRoutine()
    {
        float t = _comboRate;

        while (t >= 0 && !_isOnFire)
        {
            t -= Time.deltaTime;
            yield return new WaitForEndOfFrame();

            if (IsSlice)
            {
                t = _comboRate;
                ComboPoints += _pointPerCombo;
                ComboSliderAction();

                if (ComboPoints >= _maxComboPoint)
                {
                    _isOnFire = true;
                    ComboPoints = _maxComboPoint;
                    EventManager.OnCombo.Invoke();
                    EventManager.OnKnifeStateChange(KnifeState.SUPER);
                    break;
                }

                Debug.Log($"ComboPoints : {ComboPoints}");
                IsSlice = false;
            }
        }
        if(!_isOnFire)
            EventManager.OnKnifeStateChange.Invoke(KnifeState.NORMAL);
        yield break;
    }

    public void ComboSliderAction()
    {
        _coomboSlider.value = ComboPoints / _maxComboPoint;
    }

    public void ResetCombo()
    {
        ComboPoints = 0;
        ComboSliderAction();
        EventManager.OnKnifeStateChange.Invoke(KnifeState.NORMAL);
    }
}
