using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform _enemyTransform;
    [SerializeField] private Rigidbody2D _enemyRigidBody;
    [SerializeField] private Enemy2Controller _enemy2Controller;
    [SerializeField] private AnimationCurve _enemyAnimationCurve;
    private Transform _enemySpawnPosition;

    [Header("Variables")]
    private string _playerProjectileCollisionTag;
    private string _collisionTag;
    private bool _enemyIsAlive;
    private int _hitPoints;
    private int _maxHitPoints;

    [Header("Movement")]
    public Vector2 _enemyMovementDirection;
    public float _movementTimer;
    public float _movementSpeed;

    private void Awake()
    {
        EnemyManagerInitialization();
    }

    private void Update()
    {
        EnemyMovement();
    }

    private void EnemyMovement()
    {
        _enemyMovementDirection.Set(_enemySpawnPosition.position.x + _enemyAnimationCurve.Evaluate(Time.time - _movementTimer), _enemyTransform.position.y - _movementSpeed * Time.deltaTime);
       _enemyTransform.position = _enemyMovementDirection;
    }

    public void OnHit(int damagePoints)
    {
        _hitPoints -= damagePoints;
    }

    public void EnemyReset()
    {
        _hitPoints = _maxHitPoints;
    }

    public void OnPlayerProjectileCollision()
    {
        _enemyRigidBody.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }

    private void EnemyManagerInitialization()
    {
        _enemySpawnPosition = GameObject.Find("EnemiesSpawnPoint").transform;
        _enemy2Controller = GetComponent<Enemy2Controller>();
        _playerProjectileCollisionTag = "PlayerProjectile";
        _enemyIsAlive = true;
        _movementTimer = 0f;
        _movementSpeed = 0.5f;
        _maxHitPoints = 10;
        _hitPoints = _maxHitPoints;
        _enemyMovementDirection = Vector2.down;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _collisionTag = collision.gameObject.tag;

        if (_collisionTag == _playerProjectileCollisionTag)
        {
            OnHit(collision.gameObject.GetComponent<PlayerProjectileManager>()._damage);
            if (_hitPoints <= 0)
            {
                _enemy2Controller.OnOutOfBoundAndPlayerCollision();
                _enemyIsAlive = false;
            }
        }
    }
}
