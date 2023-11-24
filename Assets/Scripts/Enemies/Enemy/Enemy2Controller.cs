using System.Collections.Generic;
using UnityEngine;

public class Enemy2Controller : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Rigidbody2D _enemyRigidBody;
    [SerializeField] Transform _enemyTransform;

    [Header("Projectiles")]
    private List<Quaternion> _enemyProjectileDirectionQuaternion;
    private EnemiesProjectilesStack _enemyProjectilesStack;
    private GameObject _enemyProjectileToLaunch;
    public int _enemyProjectilesperAttackCount;

    [Header("Enemies spawn timer")]
    public float _enemyProjectileSpawnTimer;
    public float _enemyProjectileSpawnTimerCounter;
    private bool _enemyProjectileAsSpawn;

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
            _enemyProjectileSpawnTimerCounter = 0f;
            _enemyProjectileAsSpawn = false;
        }
        else
            _enemyProjectileSpawnTimerCounter += Time.deltaTime;
    }

    private void EnemyProjectileSpawn()
    {
        for (int i = 0; i < _enemyProjectilesperAttackCount; i++)
        {
            _enemyProjectileToLaunch = _enemyProjectilesStack._enemyProjectilesStack.Pop();
            _enemyProjectileToLaunch.SetActive(true);
            _enemyProjectileToLaunch.transform.position = _enemyTransform.position;
            _enemyProjectileToLaunch.transform.rotation = _enemyProjectileDirectionQuaternion[i];

            if (_enemyProjectileToLaunch.GetComponent<EnemyProjectileController>() != null)
                _enemyProjectileToLaunch.GetComponent<EnemyProjectileController>().OnFireAction();
        }
        _enemyProjectileAsSpawn = true;
    }

    public void OnOutOfBoundAndPlayerCollision()
    {
        _enemyRigidBody.velocity = Vector2.zero;
        gameObject.SetActive(false);
        _enemyProjectilesStack._enemyProjectilesStack.Push(gameObject);
    }

    private void EnemyControllerInitialization()
    {
        _enemyProjectilesperAttackCount = 8;
        _enemyProjectileDirectionQuaternion = new List<Quaternion>();

        for (int i = 0; i < _enemyProjectilesperAttackCount; i++)
        {
            _enemyProjectileDirectionQuaternion.Add(Quaternion.Euler(0, 0, (360 / _enemyProjectilesperAttackCount) * i));
        }

        _enemyProjectilesStack = GameObject.Find("EnemiesProjectiles").GetComponent<EnemiesProjectilesStack>();
        _enemyProjectileToLaunch = null;
        _enemyProjectileSpawnTimer = 1.5f;
        _enemyProjectileSpawnTimerCounter = 0f;
        _enemyProjectileAsSpawn = false;
    }
}
