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
    [SerializeField] private ScoreUI _scoreUI;

    [Header("Varibales")]
    private float _victoryTimer;
    private float _victoryTimerCounter;
    private bool _victoryControlStatus;
    private float _victoryTimerRemaining;
    private float _victoryTimerMinutesRemaining;
    private float _victoryTimerSecondesRemaining;

    private void Awake()
    {
        VictoryControllerInitialization();
    }

    private void Update()
    {
        if (_victoryTimerCounter >= _victoryTimer && !_victoryControlStatus)
            OnPlayerVictory(true);
        else
            _victoryTimerCounter += Time.deltaTime;

        VictoryTimerDisplay();
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
        _scoreUI._victory = true;
        _victoryScoreText.text = _scoreUI.GetScore();
        _victoryFinalScoreText.text = _scoreUI.GetFinalScore();
        _victoryCanvas.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_victoryMainMenuBtn);
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
    }
}
