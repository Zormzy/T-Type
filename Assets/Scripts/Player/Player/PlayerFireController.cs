using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerFireController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject _playerProjectilePrefab;
    private Transform _playerTransform;

    [Header("FireRate")]
    public float _fireTimer;
    public float _fireTimerCounter;

    [Header("Projectiles")]
    public Stack<GameObject> _playerProjectileStack;
    private GameObject _playerProjectileToLaunch;
    private string _playerProjectileName;
    private bool _asPlayerFired;
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
            _asPlayerFired = false;
        }
        else
            _fireTimerCounter += Time.deltaTime;
    }

    private void PlayerFireGeneration()
    {
        _playerProjectileToLaunch = _playerProjectileStack.Last();
        _playerProjectileToLaunch.SetActive(true);
        _playerProjectileToLaunch.transform.position = _playerTransform.position;
        _playerProjectileToLaunch.GetComponent<PlayerProjectileController>().OnFireAction();
        _playerProjectileStack.Pop();
        _asPlayerFired = true;

        //foreach (GameObject _playerProjectile in _playerProjectileList)
        //{
        //    if (!_playerProjectile.activeSelf && !_asPlayerFired)
        //    {
        //        _playerProjectile.SetActive(true);
        //        _playerProjectile.transform.position = _playerTransform.position;
        //        _playerProjectile.GetComponent<PlayerProjectileController>().OnFireAction();
        //        _asPlayerFired = true;
        //    }
        //}
    }

    private void PlayerProjectilesListInitialization()
    {
        for (int i = 0; i < _playerProjectileCount; i++)
        {
            GameObject _projectile = Instantiate(_playerProjectilePrefab);
            _projectile.SetActive(false);
            _projectile.transform.parent = _playerTransform;
            _projectile.name = _playerProjectileName + i.ToString();
            _playerProjectileStack.Push(_projectile);
        }
    }

    private void PlayerFireControllerInitialization()
    {
        _playerTransform = transform;
        _playerProjectileName = "Player projectile n°";
        _playerProjectileCount = 20;
        _playerProjectileStack = new Stack<GameObject>();
        _playerProjectileToLaunch = null;
        PlayerProjectilesListInitialization();
        _asPlayerFired = false;
        _fireTimer = 0.5f;
        _fireTimerCounter = 0f;
    }
}
