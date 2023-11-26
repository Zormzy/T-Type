using UnityEngine;

public class EnemyProjectileManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D _enemyProjectileRigidBody;
    [SerializeField] private EnemyProjectileController _enemyProjectileController;

    [Header("Variables")]
    private string _playerCollisionTag;
    private string _collisionTag;

    private void Awake()
    {
        EnemyProjectileManagerInitialization();
    }

    private void EnemyProjectileManagerInitialization()
    {
        _playerCollisionTag = "Player";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _collisionTag = collision.gameObject.tag;

        if (_collisionTag == _playerCollisionTag)
            _enemyProjectileController.OnOutOfBoundAndPlayerCollision();
    }
}
