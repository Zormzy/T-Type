using System.Collections.Generic;
using UnityEngine;

public class Enemy2Controller : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Rigidbody2D _enemyRigidBody;
    [SerializeField] Transform _enemyTransform;
    [SerializeField] private AudioClip _enemyFireAudioClip;
    private AudioSource _audioSource;

    [Header("Projectiles")]
    private List<Quaternion> _enemyProjectileDirectionQuaternion;
    private EnemiesProjectilesStack _enemyProjectilesStack;
    private EnemiesStacks _enemyStacks;
    private GameObject _enemyProjectileToLaunch;
    public int _enemyProjectilesperAttackCount;

    [Header("Enemies spawn timer")]
    public float _enemyProjectileSpawnTimer;
    public float _enemyProjectileSpawnTimerCounter;

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
            EnemyProjectileSpawn();
            Invoke(nameof(EnemyProjectileSpawn), 0.5f);
            _enemyProjectileSpawnTimerCounter = 0f;
        }
        else
            _enemyProjectileSpawnTimerCounter += Time.deltaTime;
    }

    private void EnemyProjectileSpawn()
    {
        _audioSource.PlayOneShot(_enemyFireAudioClip);
        for (int i = 0; i < _enemyProjectilesperAttackCount; i++)
        {
            _enemyProjectileToLaunch = _enemyProjectilesStack._enemyProjectilesStack.Pop();
            _enemyProjectileToLaunch.SetActive(true);
            _enemyProjectileToLaunch.transform.position = _enemyTransform.position;
            _enemyProjectileToLaunch.transform.rotation = _enemyProjectileDirectionQuaternion[i];

            if (_enemyProjectileToLaunch.GetComponent<EnemyProjectileController>() != null)
                _enemyProjectileToLaunch.GetComponent<EnemyProjectileController>().OnFireAction();
        }
    }

    public void OnOutOfBoundAndPlayerCollision()
    {
        _enemyRigidBody.velocity = Vector2.zero;
        gameObject.SetActive(false);
        _enemyStacks._enemyNormal2Stack.Push(gameObject);
    }

    private void EnemyControllerInitialization()
    {
        _enemyProjectilesperAttackCount = 8;
        _enemyProjectileDirectionQuaternion = new List<Quaternion>();

        for (int i = 0; i < _enemyProjectilesperAttackCount; i++)
        {
            _enemyProjectileDirectionQuaternion.Add(Quaternion.Euler(0, 0, (360 / _enemyProjectilesperAttackCount) * i));
        }

        _audioSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();
        _enemyProjectilesStack = GameObject.Find("EnemiesProjectiles").GetComponent<EnemiesProjectilesStack>();
        _enemyStacks = GameObject.Find("Enemies").GetComponent<EnemiesStacks>();
        _enemyProjectileToLaunch = null;
        _enemyProjectileSpawnTimer = 2f;
        _enemyProjectileSpawnTimerCounter = 0f;
    }
}
