using UnityEngine;

public class PauseController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject _pauseCanvas;
    [SerializeField] private ScoreUI _scoreUI;

    [Header("Varibales")]
    private bool _isPaused;

    private void Awake()
    {
        PauseControllerInitializaztion();
    }

    public void PauseGame(bool _victoryDone, bool _victoryStatus)
    {
        if (!_victoryDone)
            _pauseCanvas.SetActive(true);
        else if (_victoryDone && !_victoryStatus)
        {
            foreach(GameObject _gameObject in GameObject.FindGameObjectsWithTag("EnemyN1"))
            {
                _gameObject.SetActive(false);
            }
            foreach (GameObject _gameObject in GameObject.FindGameObjectsWithTag("EnemyN2"))
            {
                _gameObject.SetActive(false);
            }
            foreach (GameObject _gameObject in GameObject.FindGameObjectsWithTag("EnemyProjectile"))
            {
                _gameObject.SetActive(false);
            }
            foreach (GameObject _gameObject in GameObject.FindGameObjectsWithTag("PlayerProjectile"))
            {
                _gameObject.SetActive(false);
            }
            Time.timeScale = 0.0f;
        }

        _scoreUI._gamePaused = true;
        _isPaused = true;
    }

    public void UnPauseGame()
    {
        _pauseCanvas.SetActive(false);
        _scoreUI._gamePaused = false;
        _isPaused = false;
    }

    private void PauseControllerInitializaztion()
    {
        _isPaused = false;
    }
}
