using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{
    private int _totalCurrency = 0;
    private int _earnedCurrencyOnThisLevel = 0;
    private int _multiplier = 1;

    void OnEnable()
    {
        EventManager.OnObjectSliced += UpdateCurrency;
        GameManager.OnGameStateChanged += OnGameStateChangedHandle;
    }

    void OnDisable()
    {
        EventManager.OnObjectSliced -= UpdateCurrency;
        GameManager.OnGameStateChanged -= OnGameStateChangedHandle;
    }

    public int GetMultiplier()
    {
        return _multiplier;
    }

    public void SetMultiplier(int multiplier)
    {
        _multiplier = multiplier;
    }

    private void UpdateCurrency(int score)
    {
        _totalCurrency += score;
        _earnedCurrencyOnThisLevel += score;

        EventManager.OnCurrencyUpdated?.Invoke(_totalCurrency, _earnedCurrencyOnThisLevel);
    }

    private void OnGameStateChangedHandle()
    {
        GameState state = GameManager.Instance.CurrentGameState;

        if (state == GameState.Menu)
        {
            ResetCurrencyOnThisLevel();
        }

        if (state == GameState.Win)
        {
            StartCoroutine(AddLevelCurrencyToTotalCurrency(_multiplier));
        }
    }

    private void ResetCurrencyOnThisLevel()
    {
        _earnedCurrencyOnThisLevel = 0;
    }

    private IEnumerator AddLevelCurrencyToTotalCurrency(int multiplier)
    {
        _earnedCurrencyOnThisLevel *= multiplier;
        EventManager.OnCurrencyUpdated?.Invoke(_totalCurrency, _earnedCurrencyOnThisLevel);
        
        yield return new WaitForSeconds(2.00f);
        _totalCurrency += _earnedCurrencyOnThisLevel;
        _earnedCurrencyOnThisLevel = 0;
        EventManager.OnCurrencyUpdated?.Invoke(_totalCurrency, _earnedCurrencyOnThisLevel);

        yield return new WaitForSeconds(0.25f);
        GameManager.Instance.SetGameState(GameState.AfterWin);
    }

}
