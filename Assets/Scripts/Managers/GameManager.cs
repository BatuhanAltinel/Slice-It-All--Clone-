using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static event Action OnGameStateChanged;
    public static GameManager Instance { get; private set; }
    public GameState CurrentGameState { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void SetGameState(GameState state)
    {
        if (state == CurrentGameState) return;

        CurrentGameState = state;
        OnGameStateChanged?.Invoke();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void SetMultiplier(int multiplier)
    {
        GetComponent<Currency>().SetMultiplier(multiplier);
    }

    public int GetMultiplier()
    {
        return GetComponent<Currency>().GetMultiplier();
    }
}
