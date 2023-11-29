using System.Collections.Generic;
using UnityEngine;

public class PlayerFireController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject _playerProjectilePrefab;
    [SerializeField] private AudioClip _playerFireSFX;
    private AudioSource _audioSource;
    private Transform _playerTransform;

    [Header("FireRate")]
    public float _fireTimer;
    public float _fireTimerCounter;

    [Header("Projectiles")]
    public Stack<GameObject> _playerProjectileStack;
    private GameObject _playerProjectileToLaunch;
    private Transform _playerProjectilesTransformParent;
    private string _playerProjectileName;
    public int _playerProjectileCount;

    private void Awake()
    {
        PlayerFireControllerInitialization();
    }

    private void Update()
    {
        PlayerFire();
    }

    private void PlayerFire()
    {
        if (_fireTimerCounter >= _fireTimer)
        {
            PlayerFireGeneration();
            _fireTimerCounter = 0f;
        }
        else
            _fireTimerCounter += Time.deltaTime;
    }

    private void PlayerFireGeneration()
    {
        _audioSource.PlayOneShot(_playerFireSFX, 1f);
        _playerProjectileToLaunch = _playerProjectileStack.Pop();
        _playerProjectileToLaunch.SetActive(true);
        _playerProjectileToLaunch.transform.position = _playerTransform.position;

        if (_playerProjectileToLaunch.GetComponent<PlayerProjectileController>() != null)
            _playerProjectileToLaunch.GetComponent<PlayerProjectileController>().OnFireAction();
    }

    private void PlayerProjectilesStackInitialization()
    {
        for (int i = 0; i < _playerProjectileCount; i++)
        {
            GameObject _projectile = Instantiate(_playerProjectilePrefab);
            _projectile.SetActive(false);
            _projectile.transform.parent = _playerProjectilesTransformParent;
            _projectile.name = _playerProjectileName + i.ToString();
            _projectile.GetComponent<PlayerProjectileController>()._playerFireController = this;
            _playerProjectileStack.Push(_projectile);
        }
    }

    private void PlayerFireControllerInitialization()
    {
        _audioSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();
        _playerProjectilesTransformParent = GameObject.Find("PlayerProjectiles").transform;
        _playerTransform = transform;
        _playerProjectileName = "Player projectile n°";
        _playerProjectileCount = 20;
        _playerProjectileStack = new Stack<GameObject>();
        _playerProjectileToLaunch = null;
        PlayerProjectilesStackInitialization();
        _fireTimer = 0.2f;
        _fireTimerCounter = 0f;
    }
}
