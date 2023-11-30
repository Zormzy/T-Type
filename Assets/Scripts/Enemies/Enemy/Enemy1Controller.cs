using System.Collections.Generic;
using UnityEngine;

public class Enemy1Controller : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D _enemyRigidBody;
    [SerializeField] private Transform _enemyTransform;
    [SerializeField] private AudioClip _enemyFireAudioClip;
    private AudioSource _audioSource;

    [Header("Projectiles")]
    private Stack<Quaternion> _enemyProjectileDirectionQuaternionStack;
    private EnemiesProjectilesStack _enemyProjectilesStack;
    private EnemiesStacks _enemyStacks;
    private GameObject _enemyProjectileToLaunch;
    public int _enemyProjectilesperAttackCount;

    [Header("Enemies spawn timer")]
    public float _enemyProjectileSpawnTimer;
    public float _enemyProjectileSpawnTimerCounter;

    public float _enemyCrossRotation;

    private void Awake()
    {
        EnemyControllerInitialization();
    }

    private void Update()
    {
        EnemiyProjectilesSpawnTimer();
    }

    public void EnemiyProjectilesSpawnTimer()
    {
        if (_enemyProjectileSpawnTimerCounter >= _enemyProjectileSpawnTimer)
        {
            _enemyCrossRotation = this.GetComponent<EnemiesManager>()._enemyCrossRotation;

            if (Random.value <= 0.5f)
            {
                EnemyProjectilePattern1Spawn();
                Invoke(nameof(EnemyProjectilePattern1Spawn), 0.5f);
            }
            else
            {
                EnemyProjectilePattern2Spawn();
                Invoke(nameof(EnemyProjectilePattern2Spawn), 0.5f);
            }

            _enemyProjectileSpawnTimerCounter = 0f;
        }
        else
            _enemyProjectileSpawnTimerCounter += Time.deltaTime;
    }

    private void EnemyProjectilePattern1Spawn()
    {
        _enemyProjectilesperAttackCount = 5;
        for (int i = 0; i < _enemyProjectilesperAttackCount; i++)
        {
            _enemyProjectileDirectionQuaternionStack.Push(Quaternion.Euler(0f, 0f, 145f + _enemyCrossRotation + (90f / _enemyProjectilesperAttackCount) * i));
        }

        _audioSource.PlayOneShot(_enemyFireAudioClip, 0.5f);
        for (int i = 0; i < _enemyProjectilesperAttackCount; i++)
        {
            _enemyProjectileToLaunch = _enemyProjectilesStack._enemyProjectilesStack.Pop();
            _enemyProjectileToLaunch.SetActive(true);
            _enemyProjectileToLaunch.transform.position = _enemyTransform.position;
            _enemyProjectileToLaunch.transform.rotation = _enemyProjectileDirectionQuaternionStack.Pop();

            if (_enemyProjectileToLaunch.GetComponent<EnemyProjectileController>() != null)
                _enemyProjectileToLaunch.GetComponent<EnemyProjectileController>().OnFireAction();
        }
    }

    private void EnemyProjectilePattern2Spawn()
    {
        _enemyProjectilesperAttackCount = 10;
        for (int i = 0; i < _enemyProjectilesperAttackCount; i++)
        {
            _enemyProjectileDirectionQuaternionStack.Push(Quaternion.Euler(0f, 0f, 100f + _enemyCrossRotation + (180f / _enemyProjectilesperAttackCount) * i));
        }

        _audioSource.PlayOneShot(_enemyFireAudioClip, 0.5f);
        for (int i = 0; i < _enemyProjectilesperAttackCount; i++)
        {
            _enemyProjectileToLaunch = _enemyProjectilesStack._enemyProjectilesStack.Pop();
            _enemyProjectileToLaunch.SetActive(true);
            _enemyProjectileToLaunch.transform.position = _enemyTransform.position;
            _enemyProjectileToLaunch.transform.rotation = _enemyProjectileDirectionQuaternionStack.Pop();

            if (_enemyProjectileToLaunch.GetComponent<EnemyProjectileController>() != null)
                _enemyProjectileToLaunch.GetComponent<EnemyProjectileController>().OnFireAction();
        }
    }

    public void OnOutOfBoundAndPlayerCollision()
    {
        _enemyRigidBody.velocity = Vector2.zero;
        gameObject.SetActive(false);
        _enemyStacks._enemyNormal1Stack.Push(gameObject);
    }

    private void EnemyControllerInitialization()
    {
        _enemyProjectilesperAttackCount = 5;
        _enemyProjectileDirectionQuaternionStack = new Stack<Quaternion>();
        _audioSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();
        _enemyProjectilesStack = GameObject.Find("EnemiesProjectiles").GetComponent<EnemiesProjectilesStack>();
        _enemyStacks = GameObject.Find("Enemies").GetComponent<EnemiesStacks>();
        _enemyProjectileToLaunch = null;
        _enemyProjectileSpawnTimer = 2f;
        _enemyProjectileSpawnTimerCounter = 0f;
    }
}
