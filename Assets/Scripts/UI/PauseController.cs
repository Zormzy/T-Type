using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject _pauseCanvas;
    [SerializeField] private GameObject _pauseMainMenuBtn;
    [SerializeField] private ScoreUI _scoreUI;
    [SerializeField] private AudioSource _levelAudioSource;
    [SerializeField] private AudioClip _levelMenuMusic;
    [SerializeField] private AudioClip _levelMusic;
    private Animator _playerAnimator;

    [Header("Varibales")]
    public float _musicVolume;
    public float _musicFadeOutTime;
    public float _musicFadeInTime;

    private void Awake()
    {
        PauseControllerInitializaztion();
    }

    public void PauseGame(bool _victoryDone, bool _victoryStatus)
    {
        if (_levelAudioSource.clip != _levelMenuMusic)
            StartCoroutine(FadeOut(_levelAudioSource, _levelMenuMusic, _musicFadeOutTime));

        if (!_victoryDone)
        {
            _pauseCanvas.SetActive(true);
            EventSystem.current.SetSelectedGameObject(_pauseMainMenuBtn);
            _playerAnimator.enabled = false;
            Time.timeScale = 0.0f;
        }
        else if (_victoryDone && _victoryStatus)
        {
            foreach (GameObject _gameObject in GameObject.FindGameObjectsWithTag("EnemyN1"))
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
            GameObject.Find("Player").SetActive(false);
        }
        else if (_victoryDone && !_victoryStatus)
        {
            foreach (GameObject _gameObject in GameObject.FindGameObjectsWithTag("PlayerProjectile"))
            {
                _gameObject.SetActive(false);
            }
        }
        _scoreUI._gamePaused = true;
    }

    public void UnPauseGame()
    {
        StartCoroutine(FadeOut(_levelAudioSource, _levelMusic, _musicFadeOutTime));
        _pauseCanvas.SetActive(false);
        _scoreUI._gamePaused = false;
        _playerAnimator.enabled = true;
        Time.timeScale = 1.0f;
    }

    IEnumerator FadeOut(AudioSource _audioSource, AudioClip _nextAudioClip, float _fadeOutTime)
    {
        while (_audioSource.volume > 0)
        {
            _audioSource.volume -= Time.unscaledDeltaTime / _fadeOutTime;

            yield return null;
        }
        _audioSource.volume = 0f;
        _audioSource.Stop();
        _audioSource.clip = _nextAudioClip;
        StartCoroutine(FadeIn(_levelAudioSource, _musicFadeInTime));
    }

    IEnumerator FadeIn(AudioSource _audioSource, float _fadeInTime)
    {
        _audioSource.Play();
        while (_audioSource.volume < _musicVolume)
        {
            _audioSource.volume += Time.unscaledDeltaTime / _fadeInTime;

            yield return null;
        }
        _audioSource.volume = _musicVolume;
    }

    private void PauseControllerInitializaztion()
    {
        _musicVolume = 0.12f;
        _musicFadeOutTime = 10f;
        _musicFadeInTime = 10f;
        _playerAnimator = GameObject.Find("Player").GetComponent<Animator>();
    }
}
