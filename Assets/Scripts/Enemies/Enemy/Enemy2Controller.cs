using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy2Controller : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] GameObject _enemyProjectilePrefab;
    [SerializeField] Rigidbody2D _enemyRigidBody;
    [SerializeField] Transform _enemyTransform;

    [Header("Projectiles")]
    private List<Quaternion> _enemyProjectileDirectionQuaternion;
    public Stack<GameObject> _enemyProjectilesStack;
    private GameObject _enemyProjectileToLaunch;
    public int _enemyProjectilesCount;
    public int _enemyProjectilesperAttackCount;
    private string _playerProjectileName;

    [Header("Enemies spawn timer")]
    public float _enemyProjectileSpawnTimer;
    public float _enemyProjectileSpawnTimerCounter;
    private bool _enemyProjectileAsSpawn;
    private int _enemyProjectilesSpawnsCount;

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
            EnenmyProjectileSpawn();
            _enemyProjectileSpawnTimerCounter = 0f;
            _enemyProjectileAsSpawn = false;
        }
        else
            _enemyProjectileSpawnTimerCounter += Time.deltaTime;
    }

    private void EnenmyProjectileSpawn()
    {
        for(int i = 0; i < _enemyProjectilesperAttackCount; i++)
        {
            _enemyProjectileToLaunch = _enemyProjectilesStack.Last();
            _enemyProjectileToLaunch.SetActive(true);
            _enemyProjectileToLaunch.transform.position = _enemyTransform.position;
            _enemyProjectileToLaunch.transform.rotation = _enemyProjectileDirectionQuaternion[i];
            _enemyProjectileToLaunch.GetComponent<EnemyProjectileController>().OnFireAction();
            _enemyProjectilesStack.Pop();
        }
            _enemyProjectileAsSpawn = true;
    }

    public void OnOutOfBoundAndPlayerCollision()
    {
        _enemyRigidBody.velocity = Vector2.zero;
        gameObject.SetActive(false);
        _enemyProjectilesStack.Push(gameObject);
    }

    private void EnemyProjectilesListInitialization()
    {
        for (int i = 0; i < _enemyProjectilesCount; i++)
        {
            GameObject _projectile = Instantiate(_enemyProjectilePrefab);
            _projectile.SetActive(false);
            _projectile.transform.parent = _enemyTransform;
            _projectile.name = _playerProjectileName + i.ToString();
            _enemyProjectilesStack.Push(_projectile);
        }
    }

    private void EnemyControllerInitialization()
    {
        _enemyProjectilesCount = 64;
        _enemyProjectilesperAttackCount = 8;
        _enemyProjectilesSpawnsCount = 0;
        _enemyProjectileDirectionQuaternion = new List<Quaternion>();
        _playerProjectileName = "Enemy Projectile n°";

        for (int i = 0; i < _enemyProjectilesperAttackCount; i++)
        {
            _enemyProjectileDirectionQuaternion.Add(Quaternion.Euler(0, 0, (360 / _enemyProjectilesperAttackCount) * i));
        }

        _enemyProjectilesStack = new Stack<GameObject>();
        _enemyProjectileToLaunch = null;
        EnemyProjectilesListInitialization();
        _enemyProjectileSpawnTimer = 1.5f;
        _enemyProjectileSpawnTimerCounter = 0f;
        _enemyProjectileAsSpawn = false;
    }
}
