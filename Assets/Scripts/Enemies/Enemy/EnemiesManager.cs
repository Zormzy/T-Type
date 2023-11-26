using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform _enemyTransform;
    [SerializeField] private Rigidbody2D _enemyRigidBody;
    [SerializeField] private Enemy1Controller _enemy1Controller;
    [SerializeField] private Enemy2Controller _enemy2Controller;
    [SerializeField] private AnimationCurve _enemyAnimationCurve;
    private ScoreUI _scoreUI;
    private Transform _enemySpawnPosition;

    [Header("Variables")]
    private string _playerProjectileCollisionTag;
    private string _collisionTag;
    private bool _enemyIsAlive;
    private int _hitPoints;
    private int _maxHitPoints;
    private int _enemyControllerNumber;

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

        if (_hitPoints <= 0)
        {
            _scoreUI.AddScoreMultipler();
            switch (_enemyControllerNumber)
            {
                case 1:
                    _enemy1Controller.OnOutOfBoundAndPlayerCollision();
                    break;
                case 2:
                    _enemy2Controller.OnOutOfBoundAndPlayerCollision();
                    break;
                default:
                    break;
            }
            _enemyIsAlive = false;
        }
    }

    public void EnemyReset()
    {
        _enemyTransform.position = _enemySpawnPosition.position;
        if (_enemyControllerNumber == 1)
            _enemyTransform.rotation = Quaternion.Euler(0, 0, 45);
        _hitPoints = _maxHitPoints;
    }

    private void EnemyManagerInitialization()
    {
        if (TryGetComponent<Enemy1Controller>(out Enemy1Controller _enemy1ControllerOut))
        {
            _enemy1Controller = _enemy1ControllerOut;
            _enemyControllerNumber = 1;
        }
        else if (TryGetComponent<Enemy2Controller>(out Enemy2Controller _enemy2ControllerOut))
        {
            _enemy2Controller = _enemy2ControllerOut;
            _enemyControllerNumber = 2;
        }

        _enemySpawnPosition = GameObject.Find("EnemiesSpawnPoint").transform;
        _scoreUI = GameObject.Find("UIManager").GetComponent<ScoreUI>();
        _playerProjectileCollisionTag = "PlayerProjectile";
        _enemyIsAlive = true;
        _movementTimer = 0f;
        _movementSpeed = 0.2f;
        _maxHitPoints = 10;
        _hitPoints = _maxHitPoints;
        _enemyMovementDirection = Vector2.down;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _collisionTag = collision.gameObject.tag;

        if (_collisionTag == _playerProjectileCollisionTag)
            OnHit(collision.gameObject.GetComponent<PlayerProjectileManager>()._damage);
    }
}
