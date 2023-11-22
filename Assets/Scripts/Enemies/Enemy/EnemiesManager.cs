using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform _enemyTransform;
    [SerializeField] private Rigidbody2D _enemyRigidBody;
    [SerializeField] private AnimationCurve _enemyAnimationCurve;
    private Transform _enemySpawnPosition;

    [Header("Variables")]
    private string _playerCollisionTag;
    private string _playerProjectileCollisionTag;
    private bool _enemyIsAlive;

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

    private void EnemyManagerInitialization()
    {
        _enemySpawnPosition = GameObject.Find("EnemiesSpawnPoint").transform;
        _playerCollisionTag = "Player";
        _playerProjectileCollisionTag = "PlayerProjectile";
        _enemyIsAlive = true;
        _movementTimer = 0f;
        _movementSpeed = 0.5f;
        _enemyMovementDirection = Vector2.down;
    }

    public void OnPlayerProjectileCollision()
    {
        _enemyRigidBody.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _playerProjectileCollisionTag = collision.gameObject.tag;

        if (_enemyRigidBody.CompareTag(_playerCollisionTag) || _enemyRigidBody.CompareTag(_playerProjectileCollisionTag))
        {
            gameObject.SetActive(false);
            _enemyIsAlive = false;
        }
    }
}
