using TMPro;
using UnityEngine;

public class PlayerLifeController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI _playerLifeTextUI;
    [SerializeField] private VictoryController _victoryController;
    [SerializeField] private PauseController _pauseController;
    [SerializeField] private ScoreUI _scoreUI;
    private PlayerController _playerController;

    [Header("Variables")]
    private string _playerLifeText;
    private int _playerLifeCounter;

    private void Start()
    {
        PlayerLifeControllerInitialization();
    }

    public void PlayerLoseLife()
    {
        if (_playerLifeCounter - 1 > 0)
        {
            _playerLifeCounter -= 1;
            _playerLifeTextUI.text = _playerLifeText + _playerLifeCounter.ToString();
            _playerController.EnemyCollision();
        }
        else
        {
            _playerController.PlayerDeath();
            _scoreUI._playerIsAlive = false;
            _victoryController.OnPlayerVictory(false);
        }
    }

    private void PlayerLifeControllerInitialization()
    {
        _playerLifeText = "Life : ";
        _playerLifeCounter = 3;
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }
}
