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
    [SerializeField] private PauseController _pauseController;
    [SerializeField] private EnemiesGeneration _enemiesGeneration;
    [SerializeField] private GameObject _victoryMainMenuBtn;
    [SerializeField] private ScoreUI _scoreUI;

    [Header("Varibales")]
    private float _victoryTimer;
    private float _victoryTimerCounter;
    private bool _victoryControlStatus;

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

    private void VictoryControllerInitialization()
    {
        _victoryTimer = 150f;
        _victoryTimerCounter = 0f;
        _victoryControlStatus = false;
    }
}
