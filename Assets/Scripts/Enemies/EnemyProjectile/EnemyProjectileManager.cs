using UnityEngine;

public class EnemyProjectileManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SpriteRenderer _enemyProjectileSpriteRenderer;
    [SerializeField] private Rigidbody2D _enemyProjectileRigidBody;
    [SerializeField] private EnemyProjectileController _enemyProjectileController;

    [Header("Variables")]
    private string _collisionTag;

    private void Awake()
    {
        EnemyManagerInitialization();
    }

    private void EnemyManagerInitialization()
    {
        _collisionTag = "Player";
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _collisionTag = collision.gameObject.tag;

        if (_enemyProjectileRigidBody.CompareTag(_collisionTag))
        {
            _enemyProjectileController.OnOutOfBoundAndPlayerCollision();
            // collision.gameObject.OnEnemyProjectileCollision();
        }
    }
}
