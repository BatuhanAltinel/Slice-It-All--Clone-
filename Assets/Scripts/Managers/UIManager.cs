using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _inGamePanel;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _losePanel;
    
    [Header("Buttons")]
    [SerializeField] private Button[] _restartButtons;
    [SerializeField] private Button _audioToggleButton;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI _currentLevelTMP;
    [SerializeField] private TextMeshProUGUI _totalCurrencyTMP;
    [SerializeField] private TextMeshProUGUI _levelCurrencyTMP;

    [Header("Audio Sprites")]
    [SerializeField] private Sprite _audioSpriteOn;
    [SerializeField] private Sprite _audioSpriteOff;

    [Header("Combo")]
    [SerializeField] GameObject ComboFire;
    public TextMeshProUGUI _successWord;
    public string[] _succesWords;

    private LevelManager _levelManager;
    private SoundManager _soundManager;

    private void Awake()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        _soundManager = FindObjectOfType<SoundManager>();

        _menuPanel.SetActive(CloseOtherPanels());
        _currentLevelTMP.text = $"LEVEL {_levelManager.CurrentLevel + 1}";
        _totalCurrencyTMP.text = $"{0}";
    }

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += UpdateUI;

        _restartButtons.ToList().ForEach(x => x.onClick.AddListener(RestartLevel));
        _audioToggleButton.onClick.AddListener(AudioSwitch);

        EventManager.OnCombo += ActivateComboFire;
        EventManager.OnNotCombo += DeactivateComboFire;
        EventManager.OnCurrencyUpdated += UpdateCurrencyUI;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= UpdateUI;

        _restartButtons.ToList().ForEach(x => x.onClick.RemoveAllListeners());
        _audioToggleButton.onClick.AddListener(AudioSwitch);

        EventManager.OnCombo -= ActivateComboFire;
        EventManager.OnNotCombo -= DeactivateComboFire;
        EventManager.OnCurrencyUpdated -= UpdateCurrencyUI;
    }

    private void UpdateUI()
    {
        GameState state = GameManager.Instance.CurrentGameState;
        Debug.Log(state);

        switch (state)
        {
            case GameState.Menu:
                _menuPanel.SetActive(CloseOtherPanels());
                _currentLevelTMP.text = $"LEVEL {_levelManager.CurrentLevel + 1}";
                break;
            case GameState.InGame:
                _inGamePanel.SetActive(CloseOtherPanels());
                break;
            case GameState.Win:
                StartCoroutine(PrepareWinScreen());
                break;
            case GameState.AfterWin:
                break;
            case GameState.Lose:
                StartCoroutine(PrepareLoseScreen());
                break;
            case GameState.AfterLose:
                break;
            default:
                CloseOtherPanels();
                break;
        }
    }

    private void UpdateCurrencyUI(int totalCurrencyAmount, int levelCurrencyAmount)
    {
        _totalCurrencyTMP.text = $"{totalCurrencyAmount}";
        _levelCurrencyTMP.text = $"{levelCurrencyAmount}";
    }

    void ActivateComboFire()
    {
        ComboFire.SetActive(true);
        string word = _succesWords[Random.Range(0,_succesWords.Length)];
        _successWord.gameObject.SetActive(true);
        _successWord.text = word;
    }

    void DeactivateComboFire()
    {
        ComboFire.SetActive(false);
        _successWord.gameObject.SetActive(false);
    }

    private void NextLevel()
    {
        _levelManager.NextLevel();
    }

    private void RestartLevel()
    {
        _levelManager.Restart();
    }

    private void AudioSwitch()
    {
        _soundManager.IsAudioOn = !_soundManager.IsAudioOn;
        _audioToggleButton.image.sprite = (_soundManager.IsAudioOn) ? _audioSpriteOn : _audioSpriteOff;
    }

    private bool CloseOtherPanels()
    {
        _menuPanel.SetActive(false);
        _inGamePanel.SetActive(false);
        _winPanel.SetActive(false);
        _losePanel.SetActive(false);

        return true;
    }

    IEnumerator PrepareWinScreen()
    {
        yield return new WaitForSeconds(0.50f);
        _winPanel.SetActive(CloseOtherPanels());
    }

    IEnumerator PrepareLoseScreen()
    {
        yield return new WaitForSeconds(0.50f);
        _losePanel.SetActive(CloseOtherPanels());
        GameManager.Instance.SetGameState(GameState.AfterLose);
    }

}
