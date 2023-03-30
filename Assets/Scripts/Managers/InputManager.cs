using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    private LevelManager _levelManager;
    [SerializeField] private GameObject _audioToggleButton;
    [SerializeField] private GameObject _restartButton;

    private void Awake()
    {
        _levelManager = FindObjectOfType<LevelManager>();
    }

    private void Update()
    {
        GameObject selectedGO = EventSystem.current.currentSelectedGameObject;
        if (selectedGO != null && (selectedGO == _audioToggleButton || selectedGO == _restartButton))
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            InputHandle();
        }
    }

    private void InputHandle()
    {
        GameState state = GameManager.Instance.CurrentGameState;

        switch (state)
        {
            case GameState.Menu:
                GameManager.Instance.GetComponent<Combo>().ResetCombo();
                GameManager.Instance.SetGameState(GameState.InGame);
                EventManager.OnTap?.Invoke();
                break;
            case GameState.InGame:
                EventManager.OnTap?.Invoke();
                break;
            //case GameState.Win:
            //    _levelManager.NextLevel();
            //    break;
            case GameState.AfterWin:
                _levelManager.NextLevel();
                break;
            //case GameState.Lose:
            //    _levelManager.Restart();
            //    break;
            case GameState.AfterLose:
                _levelManager.Restart();
                break;
            default:
                break;
        }
    }
}
