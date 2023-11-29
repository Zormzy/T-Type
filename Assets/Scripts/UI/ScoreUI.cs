using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI _scoreTextUI;

    [Header("Variables")]
    private string _scoreText;
    private string _scoreVictoryText;
    private string _scoreFinalVictoryText;
    public bool _playerIsAlive;
    public bool _gamePaused;
    public bool _victory;
    public float _score;
    public int _scoreMultiplier;
    public int _finalScore;

    private void Awake()
    {
        ScoreUIInitialization();
    }

    private void Update()
    {
        if (_playerIsAlive & !_gamePaused && !_victory)
        {
            _score += 1f * Time.deltaTime;
            _scoreText = "Score\n" + ((int)_score).ToString();
            _scoreTextUI.text = _scoreText;
        }
    }

    public void AddScoreMultipler()
    {
        _scoreMultiplier += 1;
    }

    public string GetScore()
    {
        if (_scoreMultiplier == 0)
            _scoreVictoryText = "Score\n" + (int)_score;
        else
            _scoreVictoryText = "Score\n" + (int)_score + " x " + _scoreMultiplier;
        return _scoreVictoryText;
    }

    public string GetFinalScore()
    {
        if (_scoreMultiplier == 0)
            _finalScore = (int)_score;
        else
            _finalScore = (int)_score * _scoreMultiplier;

        _scoreFinalVictoryText = "Final score\n" + _finalScore.ToString();
        return _scoreFinalVictoryText;
    }

    private void ScoreUIInitialization()
    {
        _scoreText = "";
        _scoreVictoryText = "";
        _scoreFinalVictoryText = "";
        _playerIsAlive = true;
        _gamePaused = false;
        _victory = false;
        _score = 0f;
        _scoreMultiplier = 0;
        _finalScore = 0;
    }
}
