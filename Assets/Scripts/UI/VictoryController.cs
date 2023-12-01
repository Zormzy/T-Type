using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class VictoryController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject _victoryCanvas;
    [SerializeField] private TextMeshProUGUI _victoryTitleText;
    [SerializeField] private TextMeshProUGUI _victoryScoreText;
    [SerializeField] private TextMeshProUGUI _victoryFinalScoreText;
    [SerializeField] private TextMeshProUGUI _victoryTimerText;
    [SerializeField] private PauseController _pauseController;
    [SerializeField] private EnemiesGeneration _enemiesGeneration;
    [SerializeField] private GameObject _victoryMainMenuBtn;
    [SerializeField] private GameObject _victoryRetryBtn;
    [SerializeField] private GameObject _victoryInputFieldPseudo;
    [SerializeField] private GameObject _victoryTimerTxtGO;
    [SerializeField] private GameObject _scoreTxtGO;
    [SerializeField] private GameObject _playerLifeTxtGO;
    [SerializeField] private GameObject _virtualKeyboardGO;
    [SerializeField] private GameObject _virtualKeyboardQLetterGO;
    [SerializeField] private ScoreUI _scoreUI;

    [Header("Varibales")]
    private float _victoryTimer;
    private float _victoryTimerCounter;
    private bool _victoryControlStatus;
    private float _victoryTimerRemaining;
    private float _victoryTimerMinutesRemaining;
    private float _victoryTimerSecondesRemaining;
    public bool _isGamepadControl;

    private void Awake()
    {
        VictoryControllerInitialization();
    }

    private void Update()
    {
        if (_victoryTimerCounter >= _victoryTimer && !_victoryControlStatus)
            OnPlayerVictory(true);
        else
        {
            VictoryTimerDisplay();
            _victoryTimerCounter += Time.deltaTime;
        }
    }

    public void OnPlayerPseudoEntered()
    {
        _victoryInputFieldPseudo.SetActive(false);
        _virtualKeyboardGO.SetActive(false);
        _victoryMainMenuBtn.SetActive(true);
        _victoryRetryBtn.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_victoryMainMenuBtn);
    }

    public void OnPlayerVictory(bool _asWon)
    {
        if (_asWon)
        {
            _pauseController.PauseGame(true, _asWon);
            _victoryTitleText.text = "Victory";
            _enemiesGeneration._playerAsWon = true;
        }
        else
        {
            _pauseController.PauseGame(true, _asWon);
            _victoryTitleText.text = "Failure";
        }

        if (_isGamepadControl)
        {
            _virtualKeyboardGO.SetActive(true);
            EventSystem.current.SetSelectedGameObject(_virtualKeyboardQLetterGO);
        }

        _victoryTimerTxtGO.SetActive(false);
        _scoreTxtGO.SetActive(false);
        _playerLifeTxtGO.SetActive(false);
        _scoreUI._victory = true;
        _victoryScoreText.text = _scoreUI.GetScore();
        _victoryFinalScoreText.text = _scoreUI.GetFinalScore();
        _victoryCanvas.SetActive(true);
        _victoryControlStatus = true;
    }

    private void VictoryTimerDisplay()
    {
        _victoryTimerRemaining = _victoryTimer - _victoryTimerCounter;
        _victoryTimerMinutesRemaining = Mathf.FloorToInt(_victoryTimerRemaining / 60);
        _victoryTimerSecondesRemaining = Mathf.FloorToInt(_victoryTimerRemaining % 60);
        _victoryTimerText.text = string.Format("{0:00}:{1:00}", _victoryTimerMinutesRemaining, _victoryTimerSecondesRemaining);
    }

    private void VictoryControllerInitialization()
    {
        _victoryTimer = 150f;
        _victoryTimerCounter = 0f;
        _victoryTimerRemaining = _victoryTimer;
        _victoryTimerMinutesRemaining = Mathf.FloorToInt(_victoryTimerRemaining / 60);
        _victoryTimerSecondesRemaining = Mathf.FloorToInt(_victoryTimerRemaining % 60);
        _victoryControlStatus = false;
        _isGamepadControl = false;
    }
}
